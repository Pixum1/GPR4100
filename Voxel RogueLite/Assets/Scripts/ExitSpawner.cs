using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform[] exitLocations;
    [SerializeField]
    private GameObject exitPrefab;

    private bool exitSpawned = false;

    private GameManager gm;
    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
    }
    private void Update()
    {
        if (gm.Collectibles.Count <= 0 && !exitSpawned)
            SpawnExit();
    }

    public void SpawnExit()
    {
        int rnd = Random.Range(0, exitLocations.Length);
        Instantiate(exitPrefab, exitLocations[rnd].position, Quaternion.identity);
        exitSpawned = true;
    }
}
