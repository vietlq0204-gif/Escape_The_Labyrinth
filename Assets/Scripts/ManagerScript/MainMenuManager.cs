using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] public GameObject SettingUI;
    public void StartGame(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void Setting()
    {
        SettingUI.SetActive(true);
    }
    public void Quit()
    {
        SettingUI.SetActive(false);
    }
}
