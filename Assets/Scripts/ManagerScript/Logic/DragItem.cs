using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
// cs trên gán vào GameOject Item. để sử lí việc kéo thả Item trong hệ thống UI (Slot)
public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;

    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector2 offset;
    public Vector3 originalPosition;

    [HideInInspector] public Transform parenAfterDrag;
    private InventorySlot inventorySlot;
    private void Start()
    {
        inventorySlot = transform.parent.GetComponent<InventorySlot>();
    }
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = rectTransform.localPosition; // Lưu vị trí ban đầu

        // Chuyển đổi vị trí chuột từ screen space sang canvas space
        Vector2 localPointerPosition;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            canvas.worldCamera,
            out localPointerPosition))
        {
            parenAfterDrag = transform.parent; // Lưu lại parent ban đầu của đối tượng
            transform.SetParent(transform.root); // Đặt parent của đối tượng lên trên cùng
            transform.SetAsLastSibling();
            offset = rectTransform.localPosition - (Vector3)localPointerPosition; // Tính toán sự chênh lệch giữa vị trí chuột và vị trí của vật phẩm

            image.raycastTarget = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Chuyển đổi vị trí chuột từ screen space sang canvas space
        Vector2 localPointerPosition;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            canvas.worldCamera,
            out localPointerPosition))
        {
            rectTransform.localPosition = localPointerPosition + offset; // Cập nhật vị trí của vật phẩm dựa trên vị trí chuột và sự chênh lệch ban đầu
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parenAfterDrag); // Đặt lại parent của đối tượng về parent ban đầu
        rectTransform.localPosition = originalPosition; // Khôi phục vị trí ban đầu

        image.raycastTarget = true;
    }
}
