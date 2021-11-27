using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsLeftText : MonoBehaviour
{
    private GameManager gm;
    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
    }
    private void Update()
    {
        GetComponent<Text>().text = $"{gm.Collectibles.Count}";
    }
}
