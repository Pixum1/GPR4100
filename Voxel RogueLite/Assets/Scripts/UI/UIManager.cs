using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject inGameUI;
    [SerializeField]
    private GameObject gameOverUI;
    [SerializeField]
    private GameObject gameWonUI;
    [SerializeField]
    private GameObject pauseUI;

    private GameManager gm;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByBuildIndex(0)) {
            if (Input.GetKey(KeyCode.Escape) && !gameWonUI.activeInHierarchy && !gameOverUI.activeInHierarchy) {
                pauseUI.SetActive(true);
                inGameUI.SetActive(false);
                Time.timeScale = 0;
            }else if(!pauseUI.activeInHierarchy)
                inGameUI.SetActive(true);
        }
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0))
            inGameUI.SetActive(false);
    }

    public void LoadMainMenu()
    {
        Destroy(FindObjectOfType<GameManager>().gameObject);
        SceneManager.LoadScene(0);
    }
    public void LoadNextLevel()
    {
        gm.LoadRandomLevel();
    }
    public void GameOverScreen()
    {
        gameOverUI.SetActive(true);
        inGameUI.SetActive(false);
    }
    public void GameWonScreen()
    {
        //Time.timeScale = 0;
        gameWonUI.SetActive(true);
        inGameUI.SetActive(false);
    }
    public void UnPauseGame()
    {
        Time.timeScale = 1;
        pauseUI.SetActive(false);
        inGameUI.SetActive(true);
    }
}
