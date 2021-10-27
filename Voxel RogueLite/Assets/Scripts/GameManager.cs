using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject player;

    private List<int> lastLevels; //a list of all levels that have previously been loaded
    private List<Coin> coins; //a list of all coins in the level
    public List<Coin> Coins { get { return coins; } }

    private void Awake()
    {    
        /**
         * this Object will not be destroyed on new Scene Load in order to prevent the game from loading previously completed Levels
         */
        DontDestroyOnLoad(gameObject.transform);
        lastLevels = new List<int>();
        coins = new List<Coin>();
    }

    private void LateUpdate()
    {
        //if the current scene index is not equal to the main menu's index
        if(SceneManager.GetActiveScene().buildIndex != 0)
        {
            //if no player was found
            if (GameObject.FindGameObjectWithTag("Player") == null)
            {
                SceneManager.LoadScene(0); //load Main Menu
                Destroy(gameObject); //destroy the game manager
            }
        }
    }
    public void AddCoin(Coin _coin)
    {
        coins.Add(_coin);
    }
    public void RemoveCoin(Coin _coin)
    {
        coins.Remove(_coin);
    }
    /// <summary>
    /// A new random scene from the build that hasn't been loaded before is taken and loaded
    /// </summary>
    public void LoadRandomLevel()
    {
        int numberOfScenes = SceneManager.sceneCountInBuildSettings; //get amount of all scenes in build
        int randomLevel = Random.Range(1, numberOfScenes); //get random int between 1 and all scenes amount

        if(SceneManager.GetActiveScene().buildIndex != 0)
        { 
            //if lastLevels list has not already added the current Scene index to it
            if (!lastLevels.Contains(SceneManager.GetActiveScene().buildIndex))
            {
                Debug.Log("Level added");
                lastLevels.Add(SceneManager.GetActiveScene().buildIndex); //it will be added
            }
        }
        //if every scene has been loaded once (except the main menu)
        if (lastLevels.Count >= numberOfScenes - 1)
        {
            Debug.Log("Load Main");
            SceneManager.LoadScene(0); //load main menu
            Destroy(gameObject);
        }
        //if the chosen (randomLevel) index has already been loaded once
        if (lastLevels.Contains(randomLevel) == true)
        {
            return; //return function => choose another randomLevel
        }
        Debug.Log("Load Level");
        SceneManager.LoadScene(randomLevel); //load the random scene
    }
}
