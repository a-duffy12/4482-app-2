using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerUI : MonoBehaviour
{
    [Header("Menus")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject hud;

    [Header("Buttons")]
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button returnToMenuButton;

    private float timer;
    private float playerItTime;
    private float enemyItTime;
    public bool playerIt;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;

        pauseMenu.SetActive(false);
        hud.SetActive(true);

        // setup button listeners
        resumeButton.onClick.AddListener(ClosePauseMenu);
        restartButton.onClick.AddListener(RestartGame);
        returnToMenuButton.onClick.AddListener(ReturnToMenu);

        timer = 180;
        playerItTime = 0;
        enemyItTime = 0;
        playerIt = true;

        // set up hud ui
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

    void ReturnToMenu()
    {
        Config.totalPlayerItTime += playerItTime;
        Config.totalEnemyItTime += enemyItTime;

        SceneManager.LoadScene("Menu");
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

        SceneManager.LoadScene("Play");
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
