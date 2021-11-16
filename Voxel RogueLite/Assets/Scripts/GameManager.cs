using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject player;

    private List<int> lastLevels; //a list of all levels that have previously been loaded
    private List<Collectible> collectibles; //a list of all coins in the level
    public List<Collectible> Collectibles { get { return collectibles; } }
    public List<GuardBehaviour> Guards { get { return guards; } }
    private List<GuardBehaviour> guards;
    [SerializeField]
    private AudioClip[] soundtrack;
    [SerializeField]
    private AudioSource audioSource;
    private List<AudioClip> alreadyPlayed = new List<AudioClip>();

    private void Awake()
    {    
        /**
         * this Object will not be destroyed on new Scene Load in order to prevent the game from loading previously completed Levels
         */
        DontDestroyOnLoad(gameObject.transform);
        lastLevels = new List<int>();
        collectibles = new List<Collectible>();
        guards = new List<GuardBehaviour>();
    }
    private void Start()
    {
        SwitchSoundtrack();
    }

    #region Remove/Add Objects
    public void AddGuard(GuardBehaviour _guard)
    {
        guards.Add(_guard);
    }
    public void RemoveGuard(GuardBehaviour _guard)
    {
        guards.Remove(_guard);
    }
    public void AddCollectible(Collectible _collectible)
    {
        collectibles.Add(_collectible);
    }
    public void RemoveCollectible(Collectible _collectible)
    {
        collectibles.Remove(_collectible);
    }
    #endregion

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
        SwitchSoundtrack();
    }

    private void SwitchSoundtrack()
    {
        int rnd = Random.Range(0, soundtrack.Length);

        if(alreadyPlayed.Contains(soundtrack[rnd]))
            SwitchSoundtrack();

        else
        {
            audioSource.clip = soundtrack[rnd];
            audioSource.Play();
            alreadyPlayed.Add(soundtrack[rnd]);
            StartCoroutine(WaitForSong(soundtrack[rnd]));
        }
        if (alreadyPlayed.Count >= soundtrack.Length)
            foreach (var s in alreadyPlayed)
                alreadyPlayed.Remove(s);
    }
    private IEnumerator WaitForSong(AudioClip _audio)
    {
        yield return new WaitForSeconds(_audio.length);
        SwitchSoundtrack();
    }
}
