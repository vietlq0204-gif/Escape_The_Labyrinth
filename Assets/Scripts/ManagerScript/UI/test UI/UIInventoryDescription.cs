using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
// cái này gắn cho UIInventoryDescription tổng để quản lí thông tin Item trong Slot
public class UIInventoryDescription : MonoBehaviour
{
    [SerializeField] private Image imageItem;
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text description;

    public void Awake()
    {
        RessetDescription();
    }
    public void RessetDescription() // trả gía trị của bảng thông tin về rổng
    {
        this.imageItem.gameObject.SetActive(false);
        this.title.text = "";
        this.description.text = "";
    }
    public void SetDescription(Sprite sprite, string itemName, string itemDescription)
    {
        this.imageItem.gameObject.SetActive(true);
        this.imageItem.sprite = sprite;
        this.title.text = itemName;
        this.description.text = itemDescription;
    }

}
