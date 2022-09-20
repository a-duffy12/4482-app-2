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
    [SerializeField] private GameObject crosshair;

    [Header("Buttons")]
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button returnToMenuButton;

    [Header("Text")]
    [SerializeField] private Text endText;
    [SerializeField] private Text timerText;
    [SerializeField] private Text playerText;
    [SerializeField] private Text playerScoreText;
    [SerializeField] private Text enemyText;
    [SerializeField] private Text enemyScoreText;

    private float timer;
    private float playerItTime;
    private float enemyItTime;
    private bool playing;
    private bool scoreUpdated = false;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;

        pauseMenu.SetActive(false);
        hud.SetActive(true);
        
        endText.text = "";

        // setup button listeners
        resumeButton.onClick.AddListener(ClosePauseMenu);
        restartButton.onClick.AddListener(RestartGame);
        returnToMenuButton.onClick.AddListener(ReturnToMenu);

        timer = 180;
        playerItTime = 0.0f;
        enemyItTime = 0.0f;
        Config.playerIt = true;

        timerText.text = timer.ToString("#");
        playerScoreText.text = playerItTime.ToString("#");
        playerText.color = Color.red;
        playerScoreText.color = Color.red;
        enemyScoreText.text = enemyItTime.ToString("#");
        enemyText.color = Color.black;
        enemyScoreText.color = Color.black;

        playing = true;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        timerText.text = timer.ToString("#");

        if (playing)
        {
            if (Config.playerIt)
            {
                playerItTime += Time.deltaTime;
                playerScoreText.text = playerItTime.ToString("#.#");
                playerText.color = Color.red;
                playerScoreText.color = Color.red;
                enemyText.color = Color.black;
                enemyScoreText.color = Color.black;
            }
            else
            {
                enemyItTime += Time.deltaTime;
                enemyScoreText.text = enemyItTime.ToString("#.#");
                enemyText.color = Color.red;
                enemyScoreText.color = Color.red;
                playerText.color = Color.black;
                playerScoreText.color = Color.black;
            }
        }

        if (timer <= 0)
        {
            playing = false;
            Time.timeScale = 0.00001f;
            crosshair.SetActive(false);
            
            if (playerItTime <= enemyItTime)
            {
                endText.text = "WINNER";
                endText.color = Color.green;
            }
            else
            {
                endText.text = "LOSER";
                endText.color = Color.red;
            }

            if (!scoreUpdated)
            {
                Config.totalPlayerItTime += playerItTime;
                Config.totalEnemyItTime += enemyItTime;
                scoreUpdated = true;
            }

            StartCoroutine(RestartPlay(0.0001f)); // waits for equivalent of 10s before restarting play
        }
    }

    void ReturnToMenu()
    {
        //Config.totalPlayerItTime += playerItTime;
        //Config.totalEnemyItTime += enemyItTime;

        SceneManager.LoadScene("Menu");
    }
 

    void OpenPauseMenu()
    {
        Time.timeScale = 0f;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        endText.gameObject.SetActive(false);
        crosshair.SetActive(false);
        pauseMenu.SetActive(true);
    }

    void ClosePauseMenu()
    {
        pauseMenu.SetActive(false);
        crosshair.SetActive(true);
        endText.gameObject.SetActive(true);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        Time.timeScale = 1f;
    }

    void RestartGame()
    {   
        Debug.Log("????");
        //Config.totalPlayerItTime += playerItTime;
        //Config.totalEnemyItTime += enemyItTime;

        SceneManager.LoadScene("Play");
    }

    IEnumerator RestartPlay(float delay)
    {
        yield return new WaitForSeconds(delay);
        
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
