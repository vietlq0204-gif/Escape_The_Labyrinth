using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
// cs trên gán vào UI Slot. để quản lí các sự kiện tương tác của Item trong Slot
public class InventorySlot : MonoBehaviour, IDropHandler, IPointerClickHandler
{   
    public Image Border; 
    public Color defaultColor = new Color(0.41f, 0.41f, 0.41f); // color Slot image default
    public Color selectedColor = new Color(0.95f, 0.47f, 0f); // color Slot image select
    private InventoryPase inventoryPase;

    [SerializeField] public Image ImageItemSlot; // Avata of Item save in SlotUI
    [SerializeField] public string NameItemSlot; //name of Item save in SlotUI
    [SerializeField] public int QuantityItemSlot; //Quantity of Item save in SlotUI
    [SerializeField] public string descriptionTxtItemSlot;// Description of Item save in SlotUI

    private void Awake()
    {
        inventoryPase = GetComponentInParent<InventoryPase>();

        Border = GetComponent<Image>();
        if (Border != null)
        {
            Border.color = defaultColor; // Đặt màu mặc định (trắng) khi khởi tạo
        }
       
    }
    private void Update()
    {
       
    }   
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;

        if (dropped == null)
        {
            Debug.LogWarning("Dropped object is null.");
            return;
        }

        DragItem dragItem = dropped.GetComponent<DragItem>();

        if (dragItem == null)
        {
            Debug.LogWarning("DragItem component not found on dropped object.");
            return;
        }

        Transform parentAfterDrag = dragItem.parenAfterDrag;

        #region // Kiểm tra xem slot hiện tại có chứa item khác với Layer "Item" không
        bool hasItemInSlot = false;
        Transform currentSlotItem = null;
        Vector3 currentSlotItemPosition = Vector3.zero; //lưu vị trí của currentSlotItem

        foreach (Transform child in transform)
        {
            if (child.gameObject.layer == LayerMask.NameToLayer("Item"))
            {
                hasItemInSlot = true;
                currentSlotItem = child; // Lưu Item B
                currentSlotItemPosition = child.localPosition; // Lưu vị trí của item B
                break;
            }
        }

        if (hasItemInSlot && currentSlotItem != null)
        {
            currentSlotItem.SetParent(parentAfterDrag); // Chuyển item hiện tại trong slot đích về slot ban đầu của item đang được kéo (dragItem)
            currentSlotItem.localPosition = Vector3.zero;
        }
        dragItem.parenAfterDrag = transform; // Đặt Parent mới cho Item A
        dragItem.transform.SetParent(transform); // Đặt Item A (đang kéo) vào vị trí của Item B đã được lưu trước đó trong vòng lặp kiểm tra
        dragItem.transform.localPosition = hasItemInSlot ? currentSlotItemPosition : dragItem.originalPosition; // Đặt dragItem vào vị trí của item hiện tại hoặc vị trí ban đầu nếu không có item nào khác

        #endregion

        #region slot đích có chứa Item khác không (chưa dùng đến)
        // Slot đích có chứa Item khác không
        // if (transform.childCount > 0)
        // {
        //     Transform currentSlotItem = transform.GetChild(0);
        //     currentSlotItem.SetParent(parentAfterDrag);
        //     currentSlotItem.localPosition = Vector3.zero;
        // }

        // dragItem.parenAfterDrag = transform;
        // dragItem.transform.SetParent(transform);
        // dragItem.transform.localPosition = Vector3.zero;
        #endregion

    }
    public void SetItemInfo(Item_Core item) // input data in Item to Slot
    {
        if (item == null)
        {
            Debug.Log("Item is null");
            return;
        }
        item.GetItemInfo(out string name, out string description);
        ImageItemSlot = GetComponentInChildren<Item_Core>().ImageItem;
        //ImageItemSlot.sprite = image.sprite; // Gán sprite từ Image vào ImageItemSlot
        NameItemSlot = name;
        descriptionTxtItemSlot = description;

        Debug.Log($"ITEM NAME: {NameItemSlot}\tITEM DESCRIPTION: {descriptionTxtItemSlot}");
    }
    public void ClearSlotInfo()
    {
        ImageItemSlot = null;
        NameItemSlot = "";
        descriptionTxtItemSlot = "";

        Debug.Log("Info Item cleared.");
    }
    public void CheckAndSetItemInfo()
    {
        bool foundItemUI = false;

        // Lấy tất cả các Transform con
        Transform[] children = GetComponentsInChildren<Transform>(true);

        foreach (Transform child in children)
        {
            if (child.gameObject.CompareTag("coin")) // Check Coin ItemUI
            {
                Debug.Log("Found Item: " + child.name + ", Tag: " + child.tag);
                Item_Core item = GetComponentInChildren<Item_Core>();

                SetItemInfo(item);
                foundItemUI = true;
                break;
            }
            if (child.gameObject.CompareTag("heathPotion"))// Check HeathPotion ItemUI
            {
                Debug.Log("Found Item: " + child.name + ", Tag: " + child.tag);
                Item_Core item = GetComponentInChildren<Item_Core>();

                SetItemInfo(item);
                foundItemUI = true;
                break;
            }
        }
        if (foundItemUI == false)
        {
            Debug.Log("Don't found Item In Slot.");
            ClearSlotInfo();
        }


    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            //Debug.Log("Mouse Left true");

            if (Border != null)
            {
                inventoryPase.SelectSlot(this);
                //Debug.Log("Call change Border true");
            }
        }
    }


    public void SetBorderToDefault()// Đặt màu của Border về màu mặc định

    {
        if (Border != null)
        {
            Border.color = defaultColor;
        }
    }

    public void SetBorderToSelected()// Đặt màu của Border khi Slot được chọn

    {
        if (Border != null)
        {
            Border.color = selectedColor;
            CheckAndSetItemInfo();
        }
    }
    // Đặt Slot là không chứa item đang kéo

}
