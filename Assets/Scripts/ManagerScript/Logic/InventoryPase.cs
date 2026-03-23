using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;
// cs trên gán vào UI Inventory (UI Cha). để quản lí các Slot trong Inventory
public class InventoryPase : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private ItemManager itemManager;
    [SerializeField] private OncoliderPlayerManager oncoliderPlayerManager;
    [SerializeField] private InventorySlot SlotPrefabs;
    [SerializeField] private RectTransform BoxInventory;

    public List<InventorySlot> ListUISlot = new List<InventorySlot>();
    public List<string> ListINFOItem = new List<string>();
    public List<int> listHeathPotionItem = new List<int>();

    public InventorySlot selectedSlot = null;
    public bool IsSlotSelected = false;
    public int heathPotion = 0;

    public GameObject UICoin;
    public GameObject UIHeathPotionItem;

    [SerializeField] public Image ImageItemPase; // Avata of Item save in Pase
    [SerializeField] public string NameItemPase; //name of Item save in pase
    [SerializeField] public int QuantityItemPase; //Quantity of Item save in Pase
    [SerializeField] public string descriptionTxtItemPase;// Description of Item save in Pase
    private void Awake()
    {
        // Khởi tạo danh sách các InventorySlot
        ListUISlot = new List<InventorySlot>(GetComponentsInChildren<InventorySlot>());
        ListINFOItem = new List<string>();
        listHeathPotionItem = new List<int>();
    }
    private void Update()
    {
        HaveCoin();
        HaveHeathPotion();
        
    }
    public void InitializeInventoryUI(int inventorySize) // tạo các SlotInventory dựa trên kích thước Inventory
    {
        for (int i = 0; i < inventorySize; i++)
        {
            InventorySlot uiSlot = Instantiate(SlotPrefabs, Vector3.zero, Quaternion.identity);
            uiSlot.transform.SetParent(BoxInventory);
            uiSlot.transform.localScale = Vector3.one; // Đặt scale của item về 1
            ListUISlot.Add(uiSlot);
        }
    }
    //public void SetItemInfo(Core item) // input data in Item to Slot
    //{
    //    item.GetItemInfo(out Sprite image, out string name, out string description);

    //    ImageItemSlot.sprite = image;
    //    NameItemSlot = name;
    //    descriptionTxtItemSlot = description;

    //    // In ra console
    //    Debug.Log("Item Name: " + NameItemSlot);
    //    Debug.Log("Item Description: " + descriptionTxtItemSlot);
    //}
    //public void ClearSlotInfo()
    //{
    //    if (ImageItemSlot != null)
    //    {
    //        ImageItemSlot.sprite = null;
    //    }

    //    NameItemSlot = "";
    //    descriptionTxtItemSlot = "";

    //    // In ra console
    //    Debug.Log("Info Item cleared.");
    //}

    //public void CheckAndSetItemInfo()
    //{
    //    bool foundCoin = false;

    //    // Lấy tất cả các Transform con
    //    Transform[] children = GetComponentsInChildren<Transform>(true);

    //    foreach (Transform child in children)
    //    {
    //        if (child.CompareTag("coin"))
    //        {
    //            Debug.Log("Checking child: " + child.name + ", Tag: " + child.tag);
    //            Core item = selectedSlot.GetComponentInChildren<Core>();
    //            if (item != null)
    //            {
    //                Debug.Log("found  info");
    //                SetItemInfo(item);
    //                foundCoin = true;
    //                break;
    //            }
    //        }else if (foundCoin == false)
    //        {
    //            Debug.Log("delete Item info");
    //            //ClearSlotInfo();
    //        }
    //    }

        
    //}
    public void SelectSlot(InventorySlot activeSlot)
    {
        if (activeSlot == null)
        {
            Debug.LogError("Active slot is null.");
            return;
        }
        if (selectedSlot != null)
        {
            // Nếu slot đang được chọn là slot đang active, bỏ chọn nó
            if (selectedSlot == activeSlot)
            {
                selectedSlot.SetBorderToDefault();
                selectedSlot = null;
                IsSlotSelected = false;
                return;
            }

            // Nếu không, đặt lại màu mặc định cho slot đang được chọn trước đó
            selectedSlot.SetBorderToDefault();
        }

        // Đánh dấu slot hiện tại là slot được chọn
        selectedSlot = activeSlot;
        selectedSlot.SetBorderToSelected();
        IsSlotSelected = true;
        Debug.Log($"is {selectedSlot.name} Selected");

        GetSelectedSlotInfo();// Lấy thông tin của slot được chọn
    }
    public void GetSelectedSlotInfo()
    {
        if (selectedSlot != null)
        {
            this.ImageItemPase = selectedSlot.ImageItemSlot;
            this.NameItemPase = selectedSlot.NameItemSlot;
            this.descriptionTxtItemPase = selectedSlot.descriptionTxtItemSlot;
            // In ra console để kiểm tra
            Debug.Log($"Selected Slot Info - Name: {NameItemPase}, Description: {descriptionTxtItemPase}");
        }
        else
        {
            Debug.LogWarning("No slot is selected.");
        }
    }
    public void HaveCoin()
    {
        if (scoreManager.listCoinInGame != null && scoreManager.listCoinInGame.Count > 0 && scoreManager.listCoinInGame[0].CoinInGame > 0)
        {
            UICoin.SetActive(true);
        }
        else
        {
            UICoin.SetActive(false);
        }
    }

    public void HaveHeathPotion()
    {
        if (itemManager.listHeathPotion != null && itemManager.listHeathPotion.Count > 0)
        {
            UIHeathPotionItem.SetActive(true);
        }
        else
        {
            UIHeathPotionItem.SetActive(false);
        }
    }


}
