using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    [SerializeField]
    private GameManager m_Gm;

    private void Awake()
    {
        m_Gm = FindObjectOfType<GameManager>();
    }

    private void OnTriggerStay(Collider _other)
    {
        if (_other.CompareTag("Player"))
        {
            //if player wants to exit && can exit level
            if (Input.GetKeyDown(KeyCode.E))
                m_Gm.LoadRandomLevel(); //activate next Level Load Function in GameManager
        }
    }
}
