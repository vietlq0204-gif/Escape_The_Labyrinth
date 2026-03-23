using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.AdaptivePerformance.Provider.AdaptivePerformanceSubsystemDescriptor;

public class UIInventoryManager : MonoBehaviour
{
    [SerializeField] private UIManager uIManager;
    public GameObject UIListItemInBodyScreen;
    public GameObject UIListItemInBodyBag;
    public GameObject infoPlayerScreen;
    public GameObject infoPlayerBag;

    public GameObject player;
    public GameObject BoxInventory;
    public GameObject BoxChest;
    public GameObject BoxCraft;

    private bool hasSwapped = false; // trạng thái để kiểm tra việc hoán đổi
    private bool hasSwappedp = false; // trạng thái để kiểm tra việc hoán đổi

    #region data info List body bar
    // lưu trữ thông tin parent, vị trí và tỉ lệ List body bar
    private Transform originalParentScreen;
    private Vector3 originalPositionScreen;
    private Vector3 originalScaleScreen;

    private Transform originalParentBag;
    private Vector3 originalPositionBag;
    private Vector3 originalScaleBag;
    #endregion
    #region data info Player
    // lưu trữ thông tin parent, vị trí và tỉ lệ List body
    private Transform originalParentScreenPlayer;
    private Vector3 originalPositionScreenPlayer;
    private Vector3 originalScaleScreenPlayer;

    private Transform originalParentBagPlayer;
    private Vector3 originalPositionBagPlayer;
    private Vector3 originalScaleBagPlayer;
    #endregion

    private void Start()
    {
        #region data info List body bar
        // Lưu thông tin parent, vị trí và tỉ lệ ban đầu List body bar
        originalParentScreen = UIListItemInBodyScreen.transform.parent;
        originalPositionScreen = UIListItemInBodyScreen.transform.localPosition;
        originalScaleScreen = UIListItemInBodyScreen.transform.localScale;

        originalParentBag = UIListItemInBodyBag.transform.parent;
        originalPositionBag = UIListItemInBodyBag.transform.localPosition;
        originalScaleBag = UIListItemInBodyBag.transform.localScale;
        #endregion
        #region data info List player info bar
        // Lưu thông tin parent, vị trí và tỉ lệ ban đầu List info player ui
        originalParentScreenPlayer = infoPlayerScreen.transform.parent;
        originalPositionScreenPlayer = infoPlayerScreen.transform.localPosition;
        originalScaleScreenPlayer = infoPlayerScreen.transform.localScale;

        originalParentBagPlayer = infoPlayerBag.transform.parent;
        originalPositionBagPlayer = infoPlayerBag.transform.localPosition;
        originalScaleBagPlayer = infoPlayerBag.transform.localScale;
        #endregion

    }

    private void Update()
    {
        UIListItemInBody();
        UIinfoPlayer();
    }

    public void UIListItemInBody()
    {
        if (uIManager.UIBOXBAG.activeSelf && !hasSwapped)
        {
            // Hoán đổi các parent
            UIListItemInBodyScreen.transform.SetParent(originalParentBag);
            UIListItemInBodyBag.transform.SetParent(originalParentScreen);

            // Đặt lại vị trí và tỉ lệ
            UIListItemInBodyScreen.transform.localPosition = originalPositionBag;
            UIListItemInBodyScreen.transform.localScale = originalScaleBag;

            UIListItemInBodyBag.transform.localPosition = originalPositionScreen;
            UIListItemInBodyBag.transform.localScale = originalScaleScreen;

            // Đánh dấu rằng việc hoán đổi đã diễn ra
            hasSwapped = true;
        }
        else if (!uIManager.UIBOXBAG.activeSelf && hasSwapped)
        {
            // Hoán đổi các parent trở lại
            UIListItemInBodyScreen.transform.SetParent(originalParentScreen);
            UIListItemInBodyBag.transform.SetParent(originalParentBag);

            // Đặt lại vị trí và tỉ lệ
            UIListItemInBodyScreen.transform.localPosition = originalPositionScreen;
            UIListItemInBodyScreen.transform.localScale = originalScaleScreen;

            UIListItemInBodyBag.transform.localPosition = originalPositionBag;
            UIListItemInBodyBag.transform.localScale = originalScaleBag;

            // Đánh dấu rằng việc hoán đổi ngược lại đã diễn ra
            hasSwapped = false;
        }
    }
    public void UIinfoPlayer()
    {
        if (uIManager.UIBOXBAG.activeSelf && !hasSwappedp)
        {
            // Hoán đổi các parent
            infoPlayerScreen.transform.SetParent(originalParentBagPlayer);
            infoPlayerBag.transform.SetParent(originalParentScreenPlayer);

            // Đặt lại vị trí và tỉ lệ
            infoPlayerScreen.transform.localPosition = originalPositionBagPlayer;
            infoPlayerScreen.transform.localScale = originalScaleBagPlayer;

            infoPlayerBag.transform.localPosition = originalPositionScreenPlayer;
            infoPlayerBag.transform.localScale = originalScaleScreenPlayer;

            // Đánh dấu rằng việc hoán đổi đã diễn ra
            hasSwappedp = true;
        }
        else if (!uIManager.UIBOXBAG.activeSelf && hasSwappedp)
        {
            // Hoán đổi các parent trở lại
            infoPlayerScreen.transform.SetParent(originalParentScreenPlayer);
            infoPlayerBag.transform.SetParent(originalParentBagPlayer);

            // Đặt lại vị trí và tỉ lệ
            infoPlayerScreen.transform.localPosition = originalPositionScreenPlayer;
            infoPlayerScreen.transform.localScale = originalScaleScreenPlayer;

            infoPlayerBag.transform.localPosition = originalPositionBagPlayer;
            infoPlayerBag.transform.localScale = originalScaleBagPlayer;

            // Đánh dấu rằng việc hoán đổi ngược lại đã diễn ra
            hasSwappedp = false;
        }
    }
    public void BtInventory()
    {
        BoxInventory.SetActive(true);
        BoxChest.SetActive(false);
        BoxCraft.SetActive(false);
    }

    public void BtChest()
    {
        BoxInventory.SetActive(false);
        BoxChest.SetActive(true);
        BoxCraft.SetActive(false);
    }

    public void BtCraft()
    {
        BoxInventory.SetActive(false);
        BoxChest.SetActive(false);
        BoxCraft.SetActive(true);
    }
}
