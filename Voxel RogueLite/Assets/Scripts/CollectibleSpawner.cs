using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> locations;
    [SerializeField]
    private GameObject collectiblePrefab;
    
    [SerializeField] [Range(0,100)]
    private int spawnRate;
    [SerializeField]
    private int minimumCoinsInLevel = 3;

    private void Awake()
    {
        SpawnMinimumAmount();
        
        SpawnRandomCoins();

        locations.Clear();
    }

    private void SpawnMinimumAmount()
    {
        for (int i = 0; i < minimumCoinsInLevel; i++)
        {
            int rnd = Random.Range(0, locations.Count - 1);
            //Debug.Log($"Added necessary coin at {locations[rnd].name}");
            Instantiate(collectiblePrefab, locations[rnd].transform.position, Quaternion.identity);
            locations.Remove(locations[rnd]);
        }
    }

    private void SpawnRandomCoins()
    {
        foreach(GameObject t in locations)
        {
            int rnd = Random.Range(0, 100);
            if (rnd <= spawnRate)
            {
                //Debug.Log($"Added random coin at {t.name}");
                Instantiate(collectiblePrefab, t.transform.position, Quaternion.identity);
            }
        }
    }
}
