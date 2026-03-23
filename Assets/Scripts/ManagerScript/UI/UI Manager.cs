using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
// cs trên gán vào ... để quản lí tất cả các UI (UI Cha)
public class UIManager : MonoBehaviour
{
    //public Animator UiAnimatorUi;
    public Canvas canvas;
    public GameObject player;
    public GameObject GameOverUI;
    public GameObject PauseGameUI;
    public GameObject ScreenUI;
    public GameObject UIBOXBAG;
    public GameObject WinGameUI;

    //public Animator ani;
    //public Teleporter teleporter;
    public PlayerManager playerManager;
    public DamgeManager damageManager;
    public HeathManager heathManager;
    public ScoreManager scoreManager;
    public GamePlay gamePlay;
    public InventoryPase inventoryPase; // đang lam chinh thuc
    //public Door door;
    //public Chest chest;
    public OncoliderPlayerManager oncoliderPlayerManager;

    private bool isPaused = false;

    [SerializeField] public Text ScoreTextScreen;
    [SerializeField] public Text HeathTextScreen;
    [SerializeField] public Text ScoreTextGOver;

    public int inventorySize = 10;
    void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        scoreManager = GetComponent<ScoreManager>();

        // truyền tham số InventorySize vào InitializeInventoryUI trong inventoryPase
        inventoryPase.InitializeInventoryUI(inventorySize); 
    }
    private void Awake()
    {
        //UiAnimatorUi = GetComponent<Animator>();
        if (UIBOXBAG.activeSelf || PauseGameUI.activeSelf) // nếu UIBOXBAG đang true
        {
            UIBOXBAG.SetActive(false);
            PauseGameUI.SetActive(false);
        }
        player = GameObject.FindGameObjectWithTag("Player");
       
    }
    private void Update()
    {
        //RunWhenStopTime();
        StopingGame();
        ESC();
        addListScoreToTextUi();

        gameOver();
        WinGame();
        OpenInventory();
    }
    public void StopingGame()
    {
        if (PauseGameUI.activeSelf || player == null || UIBOXBAG.activeSelf || GameOverUI.activeSelf || WinGameUI.activeSelf)
        {
            ScreenUI.SetActive(false);
            //Time.timeScale = 0f; // Tạm dừng thời gian trò chơi
        }
        else
        {
            ScreenUI.SetActive(true);
            //Time.timeScale = 1f; // Tiếp tục thời gian trò chơi
        }
    }
    public void ESC()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //InventoryUI đang mở và nhấn phím Esc thì đóng InventoryUI
        {
            PauseGame();
            if (UIBOXBAG.activeSelf)
            {
                player.SetActive(true);
                UIBOXBAG.SetActive(false);
            }
            
        }
    }
    #region UI Manager
    public void addListScoreToTextUi()
    {
        ScoreTextScreen.text = scoreManager.listCoinInGame[0].CoinInGame.ToString();
        ScoreTextGOver.text = ScoreTextScreen.text.ToString();
        HeathTextScreen.text = heathManager.listHeathInGame[0].heathInGame.ToString();
    }
    //public void RunWhenStopTime() // vô hiệu hóa ảnh hưởng của timeScale
    //{
    //    UiAnimatorUi.updateMode = AnimatorUpdateMode.UnscaledTime; // vô hiệu hóa ảnh hưởng của timeScale đối vơi UiAnimation
    //}
    public void gameOver()
    {
        if (player == null)
        {
            WaitForSeconds waitForSeconds = new WaitForSeconds(1);

            GameOverUI.SetActive(true);
        }
    }
    public void WinGame()
    {
        if (gamePlay.isWinGame == true)
        {
            WinGameUI.SetActive(true);
            //player.SetActive(false);
            ScreenUI.SetActive(false);
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        isPaused = false;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    #region Pause game system
    public void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                Pause();     
            }
        }
    }
    public void Pause()
    {
        isPaused = true;
        bool isActive = !PauseGameUI.activeSelf;
        PauseGameUI.SetActive(isActive);
        player.SetActive(!isActive);
        if (!UIBOXBAG.activeSelf)
        {
            UIBOXBAG.SetActive(false);
        }
    }
    public void ResumeGame()
    {
        isPaused = false;
        ScreenUI.SetActive(true);
        PauseGameUI.SetActive(false);
        UIBOXBAG.SetActive(false);
        player.SetActive(true);
    }
    #endregion
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cup"))
        {
            gamePlay.isWinGame = true;
        }
    }
    #endregion

    public void OpenInventory()
    {
        // Kiểm tra nếu PauseGameUI đang mở thì không cho phép mở inventory
        if (PauseGameUI.activeSelf || GameOverUI.activeSelf || WinGameUI.activeSelf)
        {
            return;
        }
        // Kiểm tra null trước khi gọi SetActive
        if (UIBOXBAG != null && player != null)
        {
            if (Input.GetKeyDown(KeyCode.I) || (oncoliderPlayerManager.IsChestGuide && Input.GetKeyDown(KeyCode.F)))
            {
                bool isActive = !UIBOXBAG.activeSelf;
                UIBOXBAG.SetActive(isActive);
                player.SetActive(!isActive);
            }
            else
            {
                //Debug.LogWarning("InventoryUI hoặc player không được khởi tạo đúng cách.");
            }
        }
    }
    //public void OpenInventory_2() // dang test
    //{
    //    if (Input.GetKeyDown(KeyCode.L))
    //    {
    //        if (InventoryUI_2.isActiveAndEnabled == false)
    //        {
    //            InventoryUI_2.Show();
    //        }
    //        else
    //        {
    //            InventoryUI_2.Hide();
    //        }
    //    }
    //}
}
