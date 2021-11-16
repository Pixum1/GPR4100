using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuideUI : MonoBehaviour
{
    [SerializeField]
    private Text text;
    [SerializeField]
    private Animator textAnim;

    private GameManager gm;
    private void Awake()
    {
        gm = Object.FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        ShowObjective("COLLECT ALL COINS!");
    }

    private void Update()
    {
        if(gm.Collectibles.Count <= 0)
        {
            ShowObjective("FIND AND ENTER EXIT!");
        }
    }

    private void ShowObjective(string _sentence)
    {
        text.text = _sentence;
        textAnim.StartPlayback();
    }
}
