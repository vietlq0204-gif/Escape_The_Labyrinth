using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGameObjectDescription : MonoBehaviour
{
    [SerializeField] private UiBoxBag uiBoxBag;
    [SerializeField] private InventoryPase inventoryPase;
    
    [SerializeField] private Image ImageItemINFO;
    [SerializeField] private TMP_Text NameItemINFO;
    [SerializeField] private TMP_Text QuantityItemINFO;
    [SerializeField] private TMP_Text descriptionTxtItemINFO;

    public void Awake()
    {
      
    }
    private void Update()
    {
        CheckActiveUI();
    }
    public void CheckActiveUI()
    {
        if (inventoryPase.IsSlotSelected == true)
        {
            SetDescription();
        }
        else
        {
            RessetDescription();
            //Debug.Log($" close SetDescription");
        }
    }
    public void RessetDescription() // trả gía trị của bảng thông tin về rổng
    {
        this.ImageItemINFO.sprite = default;
        this.NameItemINFO.text = "";
        this.descriptionTxtItemINFO.text = "";
        //this.QuantityItemINFO.text = "";
    }
    public void SetDescription() // lấy thông tin của Item trong Slot được chọn
    {
        if (inventoryPase.ImageItemPase == null)
        {
            return;
        }
        this.ImageItemINFO.sprite = inventoryPase.ImageItemPase.sprite;
        this.NameItemINFO.text = inventoryPase.NameItemPase;
        this.descriptionTxtItemINFO.text = inventoryPase.descriptionTxtItemPase;
        //this.QuantityItemINFO.text = itemQuantity;
    }
}
