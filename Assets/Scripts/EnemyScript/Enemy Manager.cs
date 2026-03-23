using Autodesk.Fbx;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private int EnemyCount = 0; // tổng số lượng Enemy 
    [SerializeField] private int aliveEnemyCount = 0; //số lượng Enemy đang sống
    [SerializeField] private int deadEnemyCount = 0; //số lượng Enemy đang chết
    public List<GameObject> ListDeadEnemy = new List<GameObject>(); // Danh sách lưu trữ các Enemy đang chết
    public List<GameObject> ListEnemiesWillRevive = new List<GameObject>();  // lưu trữ các Enemy sẳn sàng hồi sinh

    public float timeNewDead = 24f; // Thời gian chờ trước khi ẩn Enemy (THAY DOI DUOC)
    public float setTimeOldDead = 48f; // Thời gian (giờ) chờ cho đến khi Enemy hồi sinh (THAY DOI DUOC)
    public float timeOldDead = 0f; // Thời gian (giờ) đếm cho quá trình hồi sinh
    private bool isTimeCounting = false; // Cờ để kiểm soát đếm thời gian hồi sinh

    public float TimelineAllEnemiesRevive = 0.2f; // mốc % thời gian của [setTimeOldDead] để % số kẻ địch đc hồi sinh (THAY DOI DUOC)
    private float NumberEnemiesRevived_20 = 0.2f; // số % Enemy được phép hồi sinh sau số 20% [TimelineEnemiesRevive] trôi qua 
    private float NumberEnemiesRevived_50 = 0.5f; // số % Enemy được phép hồi sinh sau số 50% [TimelineEnemiesRevive] trôi qua 
    private bool isTimed_20 = false;
    private bool isTimed_50 = false;

    private void Start()
    {
        InvokeRepeating(nameof(CountEnemies), 0f, 0.5f);
    }
    void Update()
    {
        TimerRespawn(); 
        CheckEnemiesLife();
    }
    private void CountEnemies() // kiểm tra số lượng Enemy
    {
        int aliveEnemy = 0;
        int deadEnemy = 0;
        foreach (Transform enemy in transform)
        {   
            EnemyCount = transform.childCount;
            HeathManager enemyHeathManager = enemy.GetComponent<HeathManager>();
            if (enemyHeathManager != null)
            {
                if (enemyHeathManager.isLife)
                {
                    aliveEnemy++;
                    
                    if (ListDeadEnemy.Contains(enemy.gameObject)) // Nếu Enemy đang sống, chắc chắn nó không có trong Death Note
                    {
                        ListDeadEnemy.Remove(enemy.gameObject);
                        ListEnemiesWillRevive.Remove(enemy.gameObject);
                    }
                }
                else
                {
                    deadEnemy++;
                    if (!ListDeadEnemy.Contains(enemy.gameObject)) // Thêm Enemy vào death note
                    {
                        ListDeadEnemy.Add(enemy.gameObject);
                    }
                }
            }
        }
        //Debug.Log($"Số lượng Enemy đang sống trong 'map_4': {aliveEnemy}");
        aliveEnemyCount = aliveEnemy;
        deadEnemyCount = deadEnemy;
    }
    private void TimerRespawn()// Đếm thời gian hồi sinh
    {
        if (isTimeCounting && timeOldDead < setTimeOldDead)
        {
            timeOldDead += Time.deltaTime;
        }

        if (timeOldDead >= setTimeOldDead || ListDeadEnemy.Count == 0)
        {
            //Debug.Log("hết thời gian befor All quái hôì sinh, dừng đếm.");
            timeOldDead = 0f;
            isTimeCounting = false; // Dừng đếm thời gian
        }
    }
    private void CheckEnemiesLife() // Kiểm tra trạng thái sống/chết của các enemy
    {
        foreach (Transform enemy in transform)
        {
            HeathManager enemyHeathManager = enemy.GetComponent<HeathManager>();
            if (enemyHeathManager == null) continue;
            if (enemyHeathManager.isLife)// loại bỏ ra khoi dead list nếu Enemy đang sống
            {
                if (ListDeadEnemy.Contains(enemy.gameObject))
                {
                    ListDeadEnemy.Remove(enemy.gameObject);
                }
                continue;
            }
            if (!enemyHeathManager.isLife && !isTimeCounting) // Enemy chết và chưa đếm thời gian hồi sinh
            {
                StartCoroutine(WaitAndDisableEnemy(enemy.gameObject, enemyHeathManager));
            }
            else
            {
                RespawnEnemies();
            }
        }
    }
    private void RespawnEnemies()// Hồi sinh enemy dựa trên % thời gian trôi qua
    {
        foreach (Transform enemy in transform)
        {
            HeathManager enemyHeathManager = enemy.GetComponent<HeathManager>();
            if (enemyHeathManager == null) continue;

            float epsilon = 0.01f; // sai số cho phép
            float halfTime_1 = setTimeOldDead * 0.2f; // 20% số thời gian để quái hồi sinh
            float halfTime_2 = setTimeOldDead * 0.5f; // 50% số thời gian để quái hồi sinh
            float halfTime_3 = setTimeOldDead * TimelineAllEnemiesRevive; // % số thời gian để toàn bộ quái hồi sinh

            if (Mathf.Abs(timeOldDead - halfTime_1) < epsilon && !enemyHeathManager.isLife)
            {
                isTimed_20 = true;
                Debug.Log($"trôi qua {0.2f * 100}% thời gian");
                enemyHeathManager.Revival = true; // Đặt cờ hồi sinh
                NumberEnemiesWillRevived();
            }
            if (Mathf.Abs(timeOldDead - halfTime_2) < epsilon && !enemyHeathManager.isLife)
            {
                isTimed_50 = true;
                Debug.Log($"trôi qua {0.5f * 100}% thời gian");
                enemyHeathManager.Revival = true;
                NumberEnemiesWillRevived();
            }
            if (Mathf.Abs(timeOldDead - halfTime_3) < epsilon && !enemyHeathManager.isLife)
            {
                Debug.Log($"trôi qua {TimelineAllEnemiesRevive * 100}% thời gian");
                enemyHeathManager.Revival = true;
                foreach (var enemys in ListDeadEnemy) // hồi sinh tất cả Enemy
                {
                    StartCoroutine(WaitAndEnableEnemy(enemys, enemyHeathManager));
                    break;
                }
            }
        }
    }
    private void NumberEnemiesWillRevived() // số lượng Enemy sẽ được hồi sinh
    {
        int totalDeadEnemies = ListDeadEnemy.Count;
        if (ListDeadEnemy.Count == 1) // nếu chỉ còn 1 Enemy trong ListDeadEnemy
        {
            GameObject singleEnemyToRevive = ListDeadEnemy[0]; // Lấy Enemy duy nhất đó
            Debug.Log($" Only 1 Enemy Need Revive: {singleEnemyToRevive.name}");
            ListEnemiesWillRevive.Add(singleEnemyToRevive);

            HeathManager enemyHeathManager = singleEnemyToRevive.GetComponent<HeathManager>();
            StartCoroutine(WaitAndEnableEnemy(singleEnemyToRevive, enemyHeathManager));

            ListDeadEnemy.Remove(singleEnemyToRevive);
            ListEnemiesWillRevive.Remove(singleEnemyToRevive);
            return;
        }
        else
        {
            Debug.Log($"có trên 1 Enemy");
            int numberToRevive = 0; // Lấy số lượng Enemy đã chết để hồi sinh dựa trên % yêu cầu

            if (isTimed_20 == true)
            {
                numberToRevive = Mathf.FloorToInt(totalDeadEnemies * NumberEnemiesRevived_20);
            }
            else if (isTimed_50 == true)
            {
                numberToRevive = Mathf.FloorToInt(totalDeadEnemies * NumberEnemiesRevived_50);
            }
            if (numberToRevive == 0) return;

            if (totalDeadEnemies % 2 != 0) // nếu totalDeadEnemies là số lẻ (hoạt động độc lập)
            {
                GameObject enemy = ListDeadEnemy[ 0/*totalDeadEnemies - 1*/];

                if (!ListEnemiesWillRevive.Contains(enemy))
                {
                    ListEnemiesWillRevive.Add(enemy);// thêm Enemy đầu tiên vào ListEnemiesWillRevive
                    HeathManager enemyHeathManager = enemy.GetComponent<HeathManager>();
                    StartCoroutine(WaitAndEnableEnemy(enemy, enemyHeathManager));

                    ListDeadEnemy.Remove(enemy); // Xóa khỏi ListDeadEnemy
                    ListEnemiesWillRevive.Clear();
                }

                numberToRevive -= 1;  // Giảm `numberToRevive` vì đã thêm một Enemy
            }
            if (totalDeadEnemies % 2 == 0)
            {
                HashSet<int> selectedIndexes = new HashSet<int>(); //  lưu các chỉ số đã chọn ngẫu nhiên
                System.Random random = new System.Random();

                while (selectedIndexes.Count < numberToRevive) // Lặp cho đến khi đã chọn đủ số lượng Enemy cần hồi sinh
                {
                    int randomIndex = random.Next(numberToRevive); // Lấy ngẫu nhiên một chỉ số từ 0 đến numberToRevive - 1
                    if (selectedIndexes.Add(randomIndex)) // Chọn chỉ số mới nếu nó chưa được chọn
                    {
                        GameObject enemy = ListDeadEnemy[randomIndex]; // Lấy Enemy từ ListDeadEnemy theo chỉ số randomIndex
                        if (!ListEnemiesWillRevive.Contains(enemy)) 
                        {
                            ListEnemiesWillRevive.Add(enemy); 
                            HeathManager enemyHeathManager = enemy.GetComponent<HeathManager>(); 
                            StartCoroutine(WaitAndEnableEnemy(enemy, enemyHeathManager)); 
                            // Nếu số lượng trong ListEnemiesWillRevive đã đạt số lượng cần hồi sinh, thoát khỏi vòng lặp
                            if (ListEnemiesWillRevive.Count >= numberToRevive)
                            {
                                break;
                            }
                        }
                    }
                }
            }
        }
        foreach (Transform enemy in transform)
        {
            HeathManager enemyHeathManager = enemy.GetComponent<HeathManager>();
            if (enemyHeathManager.isLife)
            {
                ListEnemiesWillRevive.Clear();
            }
        }  // Xóa các Enemy còn sống khỏi ListEnemiesWillRevive (gở lỗi)(chưa tối ưu)
    }

    private IEnumerator WaitAndDisableEnemy(GameObject enemy, HeathManager enemyHeathManager) // ẩn Enemy
    {
        yield return new WaitForSeconds(timeNewDead);
        if (!enemyHeathManager.isLife)
        {
            enemy.SetActive(false);
            //Debug.Log($"Enemy bị tắt: {enemy.name}");
            isTimeCounting = true; // Bắt đầu đếm thời gian hồi sinh
        }
    }
    private IEnumerator WaitAndEnableEnemy(GameObject enemy, HeathManager enemyHeathManager) // hiện Enemy
    {
        yield return new WaitForSeconds(0.01f);
        //Debug.Log($"Enemy được hồi sinh: {enemy.name}");
        enemy.SetActive(true);
        enemy.GetComponent<DamgeManager>().ResetDamageState();  // Thiết lập lại trạng thái sát thương// (còn lỗi, chưa tối ưu)
    }
}
