using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class UIGuideScreenManager : MonoBehaviour
{
    public GameObject player;
    public OncoliderPlayerManager oncoliderPlayerManager;

    public GameObject F;
    public GameObject E;
    public GameObject Q;
    private void Update()
    {
        ShowUiGuideScreen();
    }
    public void ShowUiGuideScreen()
    {
        if (player != null)
        {
            if (oncoliderPlayerManager.IsChestGuide)
            {
                F.SetActive(true);

            }
            else if (oncoliderPlayerManager.IsPuzzle)
            {
                Q.SetActive(true);
            }
            else if (oncoliderPlayerManager.IsDoorGuide || oncoliderPlayerManager.IsTeleportGuide)
            {
                E.SetActive(true);
            }
            else
            {
                F.SetActive(false);
                E.SetActive(false);
                Q.SetActive(false);
            }
        }
    }


}
