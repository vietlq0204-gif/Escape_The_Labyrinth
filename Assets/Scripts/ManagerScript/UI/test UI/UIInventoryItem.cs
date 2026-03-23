using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
// Script này gắn cho SlotUI (quản lí các sự kiện của SlotUI và Item UI)
public class UIInventoryItem : MonoBehaviour, IPointerClickHandler, IBeginDragHandler
            , IEndDragHandler, IDropHandler, IDragHandler
{
    [SerializeField] private Image itemImage; // ảnh đại diện cho Item trong SlotUI
    [SerializeField] private TMP_Text quantityTxt;// Số lượng Item trong SlotUI
    [SerializeField] private Image boderImage;// ảnh viền item, đánh dấu khi item được chọn.

    public event Action<UIInventoryItem> OnItemClicked, OnItemDroppedOn, 
        OnItemBeginDrag, OnItemEndDrag, OnRightMouseBtClick; // khởi tạo các sự kiện tương tác

    private bool empty = true; // kiểm tra xem item có rỗng (không có dữ liệu) hay không.

    public void Awake() // gọi khi đối tượng được tạo
    {
        ResetData();
        Deselect();
    }
    public void ResetData() // Đặt lại dữ liệu của item, ẩn hình ảnh của item và đánh dấu item là rỗng.
    {
        this.itemImage.gameObject.SetActive(false);
        empty = true;
    }
    public void Deselect() // Tắt viền của item, dùng khi item không được chọn.
    {
        boderImage.enabled = false;
    }
    public void SetData(Sprite sprite, int quantity) //  Đặt dữ liệu cho item, bao gồm hình ảnh
                                                    //  và số lượng, và đánh dấu item không còn rỗng.
    {
        this.itemImage.gameObject.SetActive(true);
        this.itemImage.sprite = sprite;
        this.quantityTxt.text = quantity + "";
        empty = false;
    }
    public void Select() // Hiển thị viền của item, dùng khi item được chọn.
    {
        boderImage.enabled = true;
    } 

    //public void OnBeginDrag() //Được gọi khi bắt đầu kéo item. Nếu item rỗng, không làm gì cả.
    //                          //Nếu không, kích hoạt sự kiện OnItemBeginDrag.
    //{
    //    if (empty) 
    //        return;
    //        OnItemBeginDrag?.Invoke(this); 
    //}
    //public void OnDrop() // Được gọi khi item được thả. Kích hoạt sự kiện OnItemDroppedOn.
    //{
    //    OnItemDroppedOn?.Invoke(this);
    //}
    //public void OnEndDrag() // Được gọi khi kết thúc kéo item. Kích hoạt sự kiện OnItemEndDrag.
    //{
    //    OnItemEndDrag?.Invoke(this);
    //}
    //public void OnPointerClick( BaseEventData Data) // sự kiện nhấn chuột
    //{
    //    if (empty)
    //        return;
    //    PointerEventData pointerData = (PointerEventData)Data;
    //    if (pointerData.button== PointerEventData.InputButton.Right) // BtmouseRight true -> OnRightMouseBtClick = true
    //    {
    //        OnRightMouseBtClick?.Invoke(this);
    //    }
    //    else // OtherButton -> OnItemClicked  == true
    //    {
    //        OnItemClicked?.Invoke(this);
    //    }
    //}

    public void OnPointerClick(PointerEventData pointerData)
    {
        if (empty)
            return;
        if (pointerData.button == PointerEventData.InputButton.Right) // BtmouseRight true -> OnRightMouseBtClick = true
        {
            OnRightMouseBtClick?.Invoke(this);
        }
        else // OtherButton -> OnItemClicked  == true
        {
            OnItemClicked?.Invoke(this);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (empty)
            return;
        OnItemBeginDrag?.Invoke(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnItemEndDrag?.Invoke(this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        OnItemDroppedOn?.Invoke(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }



    // Các thuộc tính và phương thức khác

    public (Sprite sprite, int quantity) GetData() // test 
    {
        return (this.itemImage.sprite, int.Parse(this.quantityTxt.text));
    }
}
