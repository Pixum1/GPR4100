using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exit : MonoBehaviour
{
    [SerializeField]
    private UIManager uiManager;
    private Text interactionText;

    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
        interactionText = GameObject.Find("Press E").GetComponent<Text>();
    }

    private void OnTriggerStay(Collider _other)
    {
        if (_other.CompareTag("Player"))
        {
            interactionText.enabled = true;
            //if player wants to exit && can exit level
            if (Input.GetKeyDown(KeyCode.E))
            {
                //Destroy(_other.gameObject);
                interactionText.enabled = false;
                uiManager.GameWonScreen();
            }
                //m_Gm.LoadRandomLevel(); //activate next Level Load Function in GameManager
        }
    }
    private void OnTriggerExit(Collider _other) {
        if (_other.CompareTag("Player")) {
            interactionText.enabled = false;
        }
    }
}
