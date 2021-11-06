using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    [SerializeField]
    private GameManager m_Gm;
    [SerializeField]
    private UIManager uiManager;

    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
        m_Gm = FindObjectOfType<GameManager>();
    }

    private void OnTriggerStay(Collider _other)
    {
        if (_other.CompareTag("Player"))
        {
            //if player wants to exit && can exit level
            if (Input.GetKeyDown(KeyCode.E))
            {
                Destroy(_other.gameObject);
                uiManager.GameWonScreen();
            }
                //m_Gm.LoadRandomLevel(); //activate next Level Load Function in GameManager
        }
    }
}
