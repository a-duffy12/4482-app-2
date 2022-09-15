using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerUI : MonoBehaviour
{
    [Header("Menus")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject leaderboard;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject hud;

    [Header("Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button leaderboardButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button returnToMenuButton1;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button returnToMenuButton2;

    private float timer;
    private float playerItTime;
    private float enemyItTime;
    public bool playerIt;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        mainMenu.SetActive(true);
        leaderboard.SetActive(false);
        pauseMenu.SetActive(false);
        hud.SetActive(false);

        // setup button listeners
        playButton.onClick.AddListener(PlayGame);
        leaderboardButton.onClick.AddListener(OpenLeaderboard);
        quitButton.onClick.AddListener(QuitGame);
        returnToMenuButton1.onClick.AddListener(ReturnToMenu);
        resumeButton.onClick.AddListener(ClosePauseMenu);
        restartButton.onClick.AddListener(RestartGame);
        returnToMenuButton2.onClick.AddListener(ReturnToMenu);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIt)
        {
            playerItTime += Time.deltaTime;
        }
        else
        {
            enemyItTime += Time.deltaTime;
        }
    }

    void PlayGame()
    {
        timer = 180;
        playerItTime = 0;
        enemyItTime = 0;
        playerIt = true;

        // set up hud

        mainMenu.SetActive(false);
        hud.SetActive(true);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        Time.timeScale = 1.0f;
    }

    void ReturnToMenu()
    {
        mainMenu.SetActive(true);
        leaderboard.SetActive(false);
        pauseMenu.SetActive(false);
        hud.SetActive(false);

        Config.totalPlayerItTime += playerItTime;
        Config.totalEnemyItTime += enemyItTime;
    }

    void OpenLeaderboard()
    {
        mainMenu.SetActive(false);
        leaderboard.SetActive(true);
    }

    void OpenPauseMenu()
    {
        Time.timeScale = 0f;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        pauseMenu.SetActive(true);
    }

    void ClosePauseMenu()
    {
        pauseMenu.SetActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        Time.timeScale = 1f;
    }

    void RestartGame()
    {
        Config.totalPlayerItTime += playerItTime;
        Config.totalEnemyItTime += enemyItTime;

        SceneManager.LoadScene("Main");
    }

    void QuitGame()
    {
        Config.totalPlayerItTime += playerItTime;
        Config.totalEnemyItTime += enemyItTime;

        Application.Quit();
    }

    #region input functions

    public void TogglePauseMenu(InputAction.CallbackContext con)
    {
        if (con.performed && !pauseMenu.activeInHierarchy) // not paused
        {
            OpenPauseMenu();
        }
        else if (con.performed && pauseMenu.activeInHierarchy) // paused
        {
            ClosePauseMenu();
        }
    }

    #endregion input functions
}
