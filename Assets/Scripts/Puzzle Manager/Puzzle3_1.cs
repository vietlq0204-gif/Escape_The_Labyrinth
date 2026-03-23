using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class Puzzle3_1 : MonoBehaviour
{
    public OncoliderPlayerManager oncoliderPlayerManager;
    [SerializeField] private GameObject cell1;
    [SerializeField] private GameObject cell2;
    [SerializeField] private GameObject cell3;
    [SerializeField] private GameObject cell4;
    [SerializeField] private GameObject cell1_1;
    [SerializeField] private GameObject cell2_1;
    [SerializeField] private GameObject cell3_1;
    [SerializeField] private GameObject cell4_1;
    [SerializeField] private GameObject StartMap3;
    [SerializeField] private GameObject NextMap3_4;
    public bool cell = false;
    public bool cell01 = false;
    public bool cell02 = false;
    public bool cell03 = false;
    public bool cell04 = false;

    [SerializeField] private List<Transform> ListCellActive = new List<Transform>();
    [SerializeField] private List<Transform> ListCellTrue = new List<Transform>();

    private void Start()
    {
    }

    private void Update()
    {
        Logic();
        Logic2();
    }

    public void Logic()
    {
       
        if (Input.GetKeyDown(KeyCode.Q))
        {
            CheckChild();
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            if (ListCellActive.Count == 0)
            {
                cell = false; // Nếu không có va chạm với bất kỳ GameObject con nào, cell == false
                cell01 = false;
                cell02 = false;
                cell03 = false;
                cell04 = false;
                UpdateCellSprites();
            }
        }
        SetActiveGameObject();
        // Hiển thị tên của hai phần tử đầu tiên trong ListCellActive trước khi thực hiện kiểm tra
        if (ListCellActive.Count >= 2 && ListCellActive[0].name == ListCellActive[1].name)
        {
            Debug.Log($"LIST cell name: {ListCellActive[0].name} AND {ListCellActive[1].name}");
            ListCellTrue.AddRange(ListCellActive);
        }

        // Kiểm tra nếu ListCellActive có ít nhất 2 phần tử và tên của phần tử thứ 2 khác với phần tử thứ 1
        if (ListCellActive.Count >= 2 || ListCellActive.Count >= 2 && ListCellActive[0].name != ListCellActive[1].name)
        {
            ListCellActive.Clear();
        }
       
    }
    private void Logic2()
    {
        if (NextMap3_4!= null)
        {
            if (ListCellTrue.Count < 8)
            {
                NextMap3_4.SetActive(false);
            }
            else
            {
                NextMap3_4.SetActive(true);
            }
        }
        else
        {
            Debug.Log("next map 3_4 = is nulling");
        }
     
    }
    private void CheckChild()
    {
        // Duyệt qua tất cả các GameObject con bên trong Parent Puzzle3_1
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeInHierarchy)
            {
                SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer>();
                if (IsPlayerCollidingWithChild(child.gameObject))
                {
                    cell = true;
                    //// Kiểm tra nếu tag của child là "Puzzle3_1CellFalse"
                    //if (child.CompareTag("Puzzle3_1CellFalse") && ListCellActive.Count == 1) // lỏ
                    //{
                    //    ListCellActive.Clear();
                    //    Debug.Log("Found a child with tag 'Puzzle3_1CellFalse'. ListCellActive cleared.");
                    //}
                    if (child.name == "cell01")
                    {
                        cell01 = true;
                        spriteRenderer.enabled = true;
                        ListCellActive.Add(child);
                        Debug.Log("name cell: " + child.name);
                    }
                    if (child.name == "cell02")
                    {
                        cell02 = true;
                        spriteRenderer.enabled = true;
                        ListCellActive.Add(child);
                        Debug.Log("name cell: " + child.name);
                    }
                    if (child.name == "cell03")
                    {
                        cell03 = true;
                        spriteRenderer.enabled = true;
                        ListCellActive.Add(child);
                        Debug.Log("name cell: " + child.name);
                    }
                    if (child.name == "cell04")
                    {
                        cell04 = true;
                        spriteRenderer.enabled = true;
                        ListCellActive.Add(child);
                        Debug.Log("name cell: " + child.name);
                    }
                    return; // Nếu có va chạm với bất kỳ GameObject con nào, ta đánh dấu và kết thúc vòng lặp
                }
            }
        }
        
    }
    private void UpdateCellSprites()
    {
        // Cập nhật spriteRenderer.enabled của tất cả các cell
        foreach (Transform child in transform)
        {
            SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                if (child.name == "cell01")
                {
                    spriteRenderer.enabled = cell01;
                }
                if (child.name == "cell02")
                {
                    spriteRenderer.enabled = cell02;
                }
                if (child.name == "cell03")
                {
                    spriteRenderer.enabled = cell03;
                }
                if (child.name == "cell04")
                {
                    spriteRenderer.enabled = cell04;
                }
            }
        }
    }
    private bool IsPlayerCollidingWithChild(GameObject child)
    {
        // Kiểm tra va chạm của Player với GameObject con
        Collider2D childCollider = child.GetComponent<Collider2D>();

        if (childCollider != null)
        {
            // Tìm tất cả các Collider2D của GameObject có tag "Player"
            Collider2D[] playerColliders = GameObject.FindGameObjectWithTag("Player").GetComponents<Collider2D>();

            foreach (Collider2D playerCollider in playerColliders)
            {
                if (childCollider.IsTouching(playerCollider))
                {
                    return true;
                }
            }
        }

        return false;
    }
    private void SetActiveGameObject()
    {

        if (cell01)
        {
            Debug.Log("bbbbbb");
            cell1.SetActive(false);
        }
        // Kiểm tra nếu ListCellTrue có hai phần tử trùng tên
        if (ListCellTrue.Count(t => t.name == "cell01") >= 2)
        {
            Debug.Log("C1");
                cell1.SetActive(false);
                cell1_1.SetActive(false);
        }
        if (cell02)
        {
            cell2.SetActive(false);
        }
        // Kiểm tra nếu ListCellTrue có hai phần tử trùng tên
        if (ListCellTrue.Count(t => t.name == "cell02") >= 2)
        {
            Debug.Log("C2");
            if (cell2_1 != null)
            {
                cell2.SetActive(false);
                cell2_1.SetActive(false);
            }
        }
        if (cell03)
        {
            cell3.SetActive(false);
        }
        // Kiểm tra nếu ListCellTrue có hai phần tử trùng tên
        if (ListCellTrue.Count(t => t.name == "cell03") >= 2)
        {
            Debug.Log("C3");
            if (cell3_1 != null)
            {
                cell3.SetActive(false);
                cell3_1.SetActive(false);
            }
        }
        if (cell04)
        {
            cell4.SetActive(false);
        }
        // Kiểm tra nếu ListCellTrue có hai phần tử trùng tên
        if (ListCellTrue.Count(t => t.name == "cell04") >= 2)
        {
            Debug.Log("C4");
            if (cell4_1 != null)
            {
                cell4.SetActive(false);
                cell4_1.SetActive(false);
            }
        }

        //else if (cell01 && cell1_1.CompareTag("Puzzle3_1CellFalse"))
        //{
        //    cell1_1.SetActive(false);
        //    Debug.Log("RESET CELL 1");
        //}
        //else if (cell02 && gameObject.CompareTag("Puzzle3_1CellFalse"))
        //{
        //    cell2_1.SetActive(false);
        //    Debug.Log("RESET CELL 2");
        //}
        //else if (cell03 && gameObject.CompareTag("Puzzle3_1CellFalse"))
        //{
        //    cell3_1.SetActive(false);
        //    Debug.Log("RESET CELL 3");
        //}
        //else if (cell04 && gameObject.CompareTag("Puzzle3_1CellFalse"))
        //{
        //    cell4_1.SetActive(false);
        //    Debug.Log("RESET CELL 4");
        //}
        else
        {
            cell1.SetActive(true);
            cell2.SetActive(true);
            cell3.SetActive(true);
            cell4.SetActive(true);
            cell1_1.SetActive(true);
            cell2_1.SetActive(true);
            cell3_1.SetActive(true);
            cell4_1.SetActive(true);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Thêm logic xử lý khi có va chạm
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Thêm logic xử lý khi rời khỏi va chạm
    }
}
