using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Script này gắn cho UI INVENTORY tổng
public class UIInventoryPage : MonoBehaviour
{
    [SerializeField] private UIInventoryItem itemPrefabs;
    [SerializeField] private RectTransform contentPanel;
    [SerializeField] private UIInventoryDescription itemDescription;
    [SerializeField] private MouseFollower mouseFollower;

    private List<UIInventoryItem> ListUIItem = new List<UIInventoryItem>();

    public Sprite image, image2;
    public int quantity;
    public string title, description;

    private int currentlyDraggedItemIndex = -1;

    private void Awake()
    {
        Hide();
        mouseFollower.Toggle(false);
        itemDescription.RessetDescription();
    }

    public void InitializeInventoryUI(int inventorySize) // tạo các SlotInventory dựa trên kích thước Inventory
    {
        for (int i = 0; i < inventorySize; i++)
        {
            UIInventoryItem uiItem = Instantiate(itemPrefabs, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel);
            uiItem.transform.localScale = Vector3.one; // Đặt scale của item về 1 (dùng đỡ)

            ListUIItem.Add(uiItem);

            uiItem.OnItemClicked += HandleItemSelection;
            uiItem.OnItemBeginDrag += HandleBeginDrag;
            uiItem.OnItemDroppedOn += HandleSwap;
            uiItem.OnItemEndDrag += HandleEndDrag;
            uiItem.OnRightMouseBtClick += HandleShowItemActions;
        }
    }

    private void HandleShowItemActions(UIInventoryItem inventoryItemUI)
    {
        // Xử lý hành động chuột phải vào item
    }

    private void HandleEndDrag(UIInventoryItem inventoryItemUI)
    {
        mouseFollower.Toggle(false);
        currentlyDraggedItemIndex = -1; // Reset chỉ số mục đang kéo
    }

    private void HandleSwap(UIInventoryItem inventoryItemUI)
    {
        int index = ListUIItem.IndexOf(inventoryItemUI); // Tìm chỉ số của InventoryItemUI trong danh sách
        if (index == -1) // Nếu không tìm thấy InventoryItemUI trong danh sách
        {
            mouseFollower.Toggle(false);
            currentlyDraggedItemIndex = -1;
            return; // Ngắt
        }

        var draggedItemData = ListUIItem[currentlyDraggedItemIndex].GetData();
        var targetItemData = ListUIItem[index].GetData();

        ListUIItem[currentlyDraggedItemIndex].SetData(targetItemData.sprite, targetItemData.quantity);
        ListUIItem[index].SetData(draggedItemData.sprite, draggedItemData.quantity);

        mouseFollower.Toggle(false);
        currentlyDraggedItemIndex = -1; // Reset chỉ số mục đang kéo
    }

    private void HandleBeginDrag(UIInventoryItem inventoryItemUI)
    {
        int index = ListUIItem.IndexOf(inventoryItemUI); // Tìm chỉ số của InventoryItemUI trong danh sách
        if (index == -1) // Nếu không tìm thấy InventoryItemUI trong danh sách
            return; // Ngắt

        currentlyDraggedItemIndex = index;
        mouseFollower.Toggle(true);
        var itemData = ListUIItem[index].GetData();
        mouseFollower.SetData(itemData.sprite, itemData.quantity); // Thiết lập dữ liệu cho Item khi bắt đầu kéo mục
    }

    private void HandleItemSelection(UIInventoryItem inventoryItemUI)
    {
        itemDescription.SetDescription(image, title, description);
        inventoryItemUI.Select(); // Gọi select để xử lý các hiệu ứng khi chọn vào Item trong SlotInventory
    }

    public void Show() // Hiện Inventory
    {
        gameObject.SetActive(true);
        itemDescription.RessetDescription();
        // Cập nhật dữ liệu cho slot đầu tiên và thứ hai trong danh sách.
        ListUIItem[0].SetData(image, quantity);
        ListUIItem[1].SetData(image2, quantity);
    }

    public void Hide() // Ẩn Inventory
    {
        gameObject.SetActive(false);
    }
}
