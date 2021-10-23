using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    PlayerController PlayerCtrl;

    List<int> lastLevels; //a list of all levels that have previously been loaded

    private int numberOfCoins; //amount of coins in the scene
    private bool canExitLevel; //true if all coins have been collected
    public bool GetCanExitLevel() { return canExitLevel; }

    private void Awake()
    {      
        /**
         * this Object will not be destroyed on new Scene Load in order to prevent the game from loading previously completed Levels
         */
        DontDestroyOnLoad(gameObject.transform);
        lastLevels = new List<int>();
    }
    private void Update()
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
            else
            {
                PlayerCtrl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
                numberOfCoins = GameObject.FindGameObjectsWithTag("Coin").Length; //get amount of coins in scene left

                //if there are no more coins in the scene
                if (numberOfCoins == 0)
                {
                    //if lastLevels list has not already added the current Scene index to it
                    if (!lastLevels.Contains(SceneManager.GetActiveScene().buildIndex))
                    {
                        lastLevels.Add(SceneManager.GetActiveScene().buildIndex); //it will be added
                    }

                    canExitLevel = true; //player is able to exit the level

                    //if the player is leaving the level
                    if (PlayerCtrl.GetIsLeaving() == true)
                        LoadRandomLevel(); //load a random level
                }
                else
                    canExitLevel = false;
            }   
        }       
    }
    /// <summary>
    /// A new random scene from the build that hasn't been loaded before is taken and loaded
    /// </summary>
    private void LoadRandomLevel()
    {
        int numberOfScenes = SceneManager.sceneCountInBuildSettings; //get amount of all scenes in build
        int randomLevel = Random.Range(1, numberOfScenes); //get random int between 1 and all scenes amount
        
        //if every scene has been loaded once (except the main menu)
        if (lastLevels.Count >= numberOfScenes - 1)
        {
            SceneManager.LoadScene(0); //load main menu
            Destroy(gameObject); //destroy game manager
        } 
        //if the chosen (randomLevel) index has already been loaded once
        if (lastLevels.Contains(randomLevel) == true)
        {
            return; //return function => choose another randomLevel
        }
        SceneManager.LoadScene(randomLevel); //load the random scene
    }
}
