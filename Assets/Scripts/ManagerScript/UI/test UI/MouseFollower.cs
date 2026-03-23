using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// cái này gắn cho MouseFollower
public class MouseFollower : MonoBehaviour
{
    [SerializeField] Canvas canvas;       // Tham chiếu đến canvas chính
    [SerializeField] Camera mainCamera;   // Tham chiếu đến camera chính
    [SerializeField] UIInventoryItem item;// Tham chiếu đến UIInventoryItem con

    public void Awake()
    {
        canvas = transform.root.GetComponent<Canvas>(); 
        mainCamera = Camera.main;                       
        item = GetComponentInChildren<UIInventoryItem>();// Lấy UIInventoryItem từ các thành phần con
    }
    public void SetData(Sprite Sprite, int quantity)
    {
        item.SetData(Sprite, quantity);// Thiết lập dữ liệu cho UIInventoryItem con dựa trên các
                                       // tham số đầu vào Sprite và quantity.
    }
    void Update()
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle( //Chuyển đổi vị trí chuột từ
                                                                 //không gian màn hình
                                                                 //(Screen space) sang không gian
                                                                 //local của RectTransform của canvas
                                                                 //(cái nay xịn vl, déo can fix nx)
            (RectTransform)canvas.transform, 
            Input.mousePosition,
            canvas.worldCamera, out position); //lấy Camera của canvas để chuyển đổi và gắn
                                               // kết quả chuyển đổi cho position

        transform.position = canvas.transform.TransformPoint(position); //Chuyển đổi vị trí local
                  //trong canvas thành vị trí Scene và gán giá trị đó cho transform.position,
                  //để đối tượng MouseFollower theo dõi vị trí chuột.
    }
    public void Toggle(bool val)
    {
        Debug.Log($"Item Toggle {val}");
        gameObject.SetActive(val);
    } // Bật hoặc tắt đối tượng MouseFollower
}
