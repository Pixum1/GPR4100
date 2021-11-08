using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    [SerializeField]
    private GameObject[] objects;

    [SerializeField]
    private List<GameObject> spawnPoints = new List<GameObject>();

    [SerializeField] [Range(0, 100)]
    private int spawnRate;

    [SerializeField]
    private bool allowRotation;

    [SerializeField]
    private bool InstantiateOnStart;

    private void Start()
    {
        if(InstantiateOnStart)
        {
            GenerateRandomObjects();
        }
    }
    public void AddSpawnpoint()
    {
        GameObject sp = new GameObject("Spawnpoint");
        sp.transform.SetParent(this.transform);
        sp.transform.localPosition = Vector3.zero;
        spawnPoints.Add(sp);
    }

    public void GenerateRandomObjects()
    {
        ClearObjects();

        foreach(GameObject sp in spawnPoints)
        {
            int rnd = Random.Range(0, 101);
            if(rnd <= spawnRate)
            {
                int rndObj = Random.Range(0, objects.Length);
                GameObject obj = Instantiate(objects[rndObj], sp.transform.position, gameObject.transform.rotation);
                obj.transform.SetParent(sp.transform);

                if(allowRotation)
                    obj.transform.localRotation = Quaternion.Euler(new Vector3(objects[rndObj].transform.eulerAngles.x, Random.Range(0f, 360f), objects[rndObj].transform.eulerAngles.z));
            }
        }
    }

    public void ClearObjects()
    {
        foreach(GameObject spawnPoint in spawnPoints)
        {
            foreach(Transform child in spawnPoint.transform)
            {
                DestroyImmediate(child.gameObject);
            }
        }
    }

    public void ClearSpawnPoints()
    {
        foreach(GameObject spawnPoint in spawnPoints)
        {
            DestroyImmediate(spawnPoint);
        }

        spawnPoints.Clear();
    }

    public void DeleteLastSpawnPoint()
    {
        if(spawnPoints.Count > 0)
        {
            DestroyImmediate(spawnPoints[spawnPoints.Count - 1].gameObject);
            spawnPoints.RemoveAt(spawnPoints.Count - 1);
        }
    }

    private void OnDrawGizmos()
    {
        BoxCollider colOfObj = objects[0].GetComponent<BoxCollider>();
        Gizmos.color = Color.green;
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            if (spawnPoints[i] != null)
                if (allowRotation)
                    Gizmos.DrawWireSphere(spawnPoints[i].transform.position, (Mathf.Max(colOfObj.size.x, colOfObj.size.z) / 2) + 1);
                else
                    Gizmos.DrawWireCube(spawnPoints[i].transform.position, colOfObj.size);

        }
    }
}
