using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.AdaptivePerformance.Provider.AdaptivePerformanceSubsystemDescriptor;

public class UiBoxBag : MonoBehaviour
{
    [SerializeField] private OncoliderPlayerManager OnColiderplayer;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private InventorySlot SlotPrefabs;
    [SerializeField] private InventoryPase inventoryPase;
    [SerializeField] private UIGameObjectDescription uIGameObjectDescription;

    public GameObject UIImgInfoGameObject;
    public GameObject UITextInfoGameObject;

    [SerializeField] public List<Items> ListINFOItem = new List<Items>();

    [SerializeField] public Image ImageItemSlot; // Avata of Item save in SlotUI
    [SerializeField] public string NameItemSlot; //name of Item save in SlotUI
    [SerializeField] public int QuantityItemSlot; //Quantity of Item save in SlotUI
    [SerializeField] public string descriptionTxtItemSlot;// Description of Item save in SlotUI


    private void Start()
    {
    }
    private void Update()
    {
        SetActiveUIInfo();
        //TakeDataFromItem();
        UpdateDecriptionItem();
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void SetActiveUIInfo()
    {
        if (inventoryPase.IsSlotSelected == true)
        {
            UIImgInfoGameObject.SetActive(true);
            UITextInfoGameObject.SetActive(true);
            //Debug.Log($" open ui");
        }
        else
        {
            UIImgInfoGameObject.SetActive(false);
            UITextInfoGameObject.SetActive(false);
            //Debug.Log($" close ui");
        }
    }

    //public void TakeDataFromItem()
    //{
    //    if (ListINFOItem.Count != scoreManager.listScore.Count)
    //    {
    //        Items Coin = new Items("coin", scoreManager.listScore.Count);
    //        ListINFOItem.Add(Coin);
    //        Debug.Log($"trong ListINFOItem có: {ListINFOItem.Count} Item");
    //        foreach (Items firstItem in ListINFOItem)  
    //        {
    //            Debug.Log("Item Name: " + firstItem.ItemName + ", Item ID: " + firstItem.ItemID);
    //        }
    //    }
    //}
    public void UpdateDecriptionItem()
    {
        foreach (Items firstItem in ListINFOItem)
        {
            NameItemSlot = firstItem.ItemName;
            QuantityItemSlot = ListINFOItem.Count;
        }
        

    }
}
public class Items
{
    public string ItemName;
    public int ItemID;

    public Items(string itemName, int itemID)
    {
        ItemName = itemName;
        ItemID = itemID;
    }
}