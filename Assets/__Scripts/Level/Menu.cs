using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [Header("Menus")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject leaderboard;

    [Header("Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button leaderboardButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button returnToMenuButton;

    [Header("Leaderboard")]
    [SerializeField] private Text firstPlaceText;
    [SerializeField] private Text lastPlaceText;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        mainMenu.SetActive(true);
        leaderboard.SetActive(false);

        // setup button listeners
        playButton.onClick.AddListener(PlayGame);
        leaderboardButton.onClick.AddListener(OpenLeaderboard);
        quitButton.onClick.AddListener(QuitGame);
        returnToMenuButton.onClick.AddListener(ReturnToMenu);
    }

    void PlayGame()
    {
        SceneManager.LoadScene("Play");
    }

    void OpenLeaderboard()
    {
        firstPlaceText.color = Color.green;
        lastPlaceText.color = Color.red;

        if (Config.totalPlayerItTime <= Config.totalEnemyItTime)
        {
            firstPlaceText.text = "Player - " + Config.totalPlayerItTime + "s";
            lastPlaceText.text = "Enemy - " + Config.totalEnemyItTime + "s";
        }
        else
        {
            firstPlaceText.text = "Enemy - " + Config.totalEnemyItTime.ToString("#.#") + "s";
            lastPlaceText.text = "Player - " + Config.totalPlayerItTime.ToString("#.#") + "s";
        }

        mainMenu.SetActive(false);
        leaderboard.SetActive(true);
    }

    void ReturnToMenu()
    {
        mainMenu.SetActive(true);
        leaderboard.SetActive(false);
    }

    void QuitGame()
    {
        Application.Quit();
    }
}
