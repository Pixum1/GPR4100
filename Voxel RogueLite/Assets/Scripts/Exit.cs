using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    [SerializeField]
    private GameManager m_Gm;
    private bool canExit = false;

    private void Awake()
    {
        m_Gm = FindObjectOfType<GameManager>();
    }
    private void Update()
    {
        if (m_Gm.Coins.Count == 0)
            canExit = true;
    }

    private void OnTriggerStay(Collider _other)
    {
        if (_other.CompareTag("Player"))
        {
            //if player wants to exit && can exit level
            if (Input.GetKeyDown(KeyCode.E) && canExit == true)
            {
                m_Gm.LoadRandomLevel(); //activate next Level Load Function in GameManager
            }
        }
    }
}
