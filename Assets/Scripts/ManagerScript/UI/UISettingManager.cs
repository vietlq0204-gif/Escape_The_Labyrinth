using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UISettingManager : MonoBehaviour
{
    public GameObject player;
    public GameObject LightList;
    public GameObject AudioList;
    public GameObject Controllist;

    public GameObject ListLightControl;
    public GameObject ListAudioControl;
    public GameObject ListControl;
    void Start()
    {
        
    }

    void Update()
    {
        //Graphics();
        //Audio();
    }
    public void Graphics()
    {
        LightList.SetActive(true);
        ListLightControl.SetActive(true);

        AudioList.SetActive(false);
        ListAudioControl.SetActive(false);
        Controllist.SetActive(false);
        ListControl.SetActive(false);
    }
    public void Audio()
    {
        AudioList.SetActive(true);
        ListAudioControl.SetActive(true);

        LightList.SetActive(false);
        ListLightControl.SetActive(false);
        Controllist.SetActive(false);
        ListControl.SetActive(false);
    }
    public void Control()
    {
        Controllist.SetActive(true);
        ListControl.SetActive(true);

        LightList.SetActive(false);
        ListLightControl.SetActive(false);
        AudioList.SetActive(false);
        ListAudioControl.SetActive(false);
    }
}
