using Autodesk.Fbx;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 
/// </summary>
public class AIEnemy : MonoBehaviour
{
    public Rigidbody2D rbEnemy;
    private GameObject player;
    public HeathManager heathManager;
    public Checkzone checkzone;
    public AttackEnemy attackEnemy;
    public Vector2 Movement;
    [SerializeField] private LayerMask includeLayers; // Layer để Enemy va chạm và thay đổi điểm đến
    [SerializeField] private LayerMask excludeLayers; // Layer mask để bỏ qua các layer không cần thiết
    [SerializeField] public float speedEnemy = 3f; //(THAY DOI DUOC)
    private bool movingRight = true; // Hướng di chuyển của Enemy
    [SerializeField] private float visionRadius = 3f; // bán kính tầm nhìn của Enemy

    [SerializeField] private bool seeTheThreat = false; // thấy mối de dọa
    [SerializeField] private bool panic = false; // hoảng sợ

    private bool isWaitingGenerateRandomDestination = false; // Trạng thái chờ của Enemy
    private Vector2 randomDestination; // Vị trí ngẫu nhiên mà Enemy sẽ di chuyển tới khi không thấy player
    public float wanderRadius = 2f; // Bán kính di chuyển ngẫu nhiên (THAY ĐỔI ĐƯỢC)
    private float distanceThreshold = 0.2f; // Ngưỡng để xác định Enemy đã đến đích
    [SerializeField] private float Timer; // Đếm thời gian từ khi điểm đến cuối cùng được tạo
    [SerializeField] private float waitTimeGenerateRandomDestination = 2f; // Thời gian tối đa Enemy trước khi thay đổi hướng
    private float timeLimitMove = 4f; // Thời gian tối đa trước khi thay đổi hướng

    public float attackRadius = 1f; //bán kính phạm vi tấn công (THAY DOI DUOC)

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        GenerateRandomDestination(); // Khởi tạo điểm đích ngẫu nhiên của Enemy
        Timer = 0f; // Khởi tạo bộ đếm thời gian
    }
    void Update()
    {
        if (heathManager.isLife)
        {
            SaveYourself();
            if (player != null)
            {
                if (seeTheThreat)
                {
                    if (!panic)
                    {
                        MoveFollowGameObject();
                    }
                }
                else
                {
                    MoveRandomly();
                }
                if (Vector2.Distance(rbEnemy.position, player.transform.position) < attackRadius)  // nếu người chơi nằm trong vùng tấn công
                {
                    attackEnemy.Attack();
                }
            }
            else
            {
                Debug.LogWarning("Player không tồn tại.");
            }
            ColiderWithTrap();
            
        }
        else
        {
            //Debug.LogWarning($"Enemy {name} không tồn tại.");
        }
    }
    private void FixedUpdate()
    {
        if (player != null)
        {
            CheckRaycast();
        }
        else
        {
            Debug.LogWarning("Player không tồn tại.");
        }
    }
    public void CheckRaycast() // Kiểm tra khoảng cách giữa địch và người chơi
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= visionRadius) // Nếu người chơi nằm trong tầm nhìn, tiến hành kiểm tra raycast
        {
            RaycastHit2D ray = Physics2D.Raycast(transform.position, player.transform.position - transform.position,
                Mathf.Infinity, ~excludeLayers); // Raycast với layer mask để bỏ qua các layer cụ thể
            if (ray.collider != null)
            {
                //Debug.Log("Raycast hit: " + ray.collider.gameObject.name);
                seeTheThreat = ray.collider.CompareTag("Player");
                if (seeTheThreat)
                {
                    Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.green);
                }
                else
                {
                    Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.red);
                }
            }
        }
        else
        {
            seeTheThreat = false; // Nếu người chơi không nằm trong tầm nhìn
        }
    }
    #region logic sử lí điểm đích ngẫu nhiên để Enemy di chuyển đến nếu không bị đe dọa
    private void GenerateRandomDestination() // tạo điểm đích ngẫu nhiên mới
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized; // Tạo hướng ngẫu nhiên
        randomDestination = (Vector2)transform.position + randomDirection * wanderRadius;
        Timer = 0f;
        //Debug.Log("New random destination: " + randomDestination);
    }
    private void GenerateReverseRandomDestination() // Tạo điểm đích ngẫu nhiên theo hướng ngược lại
    {
        Vector2 reverseDirection = -(randomDestination - (Vector2)transform.position).normalized; // Hướng ngược lại
        randomDestination = (Vector2)transform.position + reverseDirection * wanderRadius;
        Timer = 0f;
        //Debug.Log("New reverse destination: " + randomDestination);
    }
    private void ColiderWithTrap() // kiểm ra có chạm với bẩy không
    {
        if (checkzone.dangerousZone)
        {
            if (checkzone.smallDmgZone)
            {
                //Debug.LogWarning(" Enemy hét.");
                checkzone.smallDmgZone = false;
                GenerateReverseRandomDestination();
            }
        }
    }
    private IEnumerator WaitAndGenerateRandomDestination()
    {
        yield return new WaitForSeconds(waitTimeGenerateRandomDestination);
        GenerateRandomDestination();
        isWaitingGenerateRandomDestination = false; // Đặt lại trạng thái chờ tạo điểm đến mới
    }
    #endregion
    public void MoveFollowGameObject() // Di chuyển theo người chơi khi có tầm nhìn
    {
        if (seeTheThreat)
        {
            randomDestination = transform.position;  // Xóa điểm đang đến khi thấy mối đe dọa
            rbEnemy.position = Vector2.MoveTowards(rbEnemy.position, player.transform.position, speedEnemy * Time.deltaTime); // đi theo player
                                                                                                                                  // Quay về hướng người chơi
            //if (player.transform.position.x > transform.position.x && !movingRight)
            //{
            //    Flip();
            //}
            //else if (player.transform.position.x < transform.position.x && movingRight)
            //{
            //    Flip();
            //}
        }
        else
        {
            return;
        }
    }
    public void MoveRandomly() // di chuyển ngẫu nhiên
    {
        if (heathManager.isLife == true)
        {
            if (!seeTheThreat)
            {
                Timer += Time.deltaTime; // Tăng thời gian kể từ khi điểm đích cuối cùng được tạo

                rbEnemy.position = Vector2.MoveTowards(rbEnemy.position, randomDestination, speedEnemy * Time.deltaTime);// Di chuyển đến điểm ngẫu nhiên

                //if (randomDestination.x > transform.position.x && !movingRight)// Kiểm tra hướng và lật hướng nếu cần
                //{
                //    Flip();
                //}
                //else if (randomDestination.x < transform.position.x && movingRight)
                //{
                //    Flip();
                //}
                // Nếu đã đến gần điểm ngẫu nhiên và không đang chờ
                if (Vector2.Distance(transform.position, randomDestination) < distanceThreshold && !isWaitingGenerateRandomDestination)
                {
                    isWaitingGenerateRandomDestination = true; // Đặt trạng thái chờ
                    StartCoroutine(WaitAndGenerateRandomDestination());
                }
                else if (Timer >= timeLimitMove && !isWaitingGenerateRandomDestination)// Nếu không đến được điểm ngẫu nhiên trong 4 giây
                {
                    GenerateReverseRandomDestination();
                }
            }
           
        }
        else
        {
            Debug.LogWarning($"Enemy {name} không sống.");
        }
    }
    public void SaveYourself() // tự cứu lấy bản thân
    {
        if (heathManager.listHeathInGame[0].heathInGame <= heathManager.startHeath / 3 )
        {
            panic = true;
            Debug.Log("Enemy bo cuoc");
        }
        else
        {
            panic = false;
        }
    }
    void Flip() // lỗi
    {
        // Đổi hướng di chuyển
        movingRight = !movingRight;

        // Lật hình của Skeleton
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnCollisionEnter2D(Collision2D collision) //xử lý va chạm Collision với các đối tượng trong Layer "Include Layer"
    {
        // nếu va chạm có Layer nằm trong "includeLayers" và không phải là Player
        if (((1 << collision.gameObject.layer) & includeLayers) != 0 && !collision.gameObject.CompareTag("Player"))
        {
            GenerateRandomDestination();
        }
    }
    private void OnTriggerEnter2D(Collider2D Trigger) //xử lý va chạm Trigger với các đối tượng trong Layer "Include Layer"
    {
        // nếu đối tượng va chạm có Layer nằm trong "includeLayers" và không phải là Player
        if (((1 << Trigger.gameObject.layer) & includeLayers) != 0 && !Trigger.gameObject.CompareTag("Player"))
        {
            GenerateRandomDestination();
        }
    }
    private void OnTriggerExit2D(Collider2D Trigger)
    {
        // chưa có gì đặc thù để kiểm tra
    }
    private void OnDrawGizmos() // Vẽ vùng bán kính khả quan của Enemy (test game)
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRadius);// Vẽ vùng bán kính tầm nhìn của địch
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(randomDestination, wanderRadius); // Vẽ bán kính điểm đến ngẫu nhiên
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius); // Vẽ bán kính tấn công của địch
    }
}
