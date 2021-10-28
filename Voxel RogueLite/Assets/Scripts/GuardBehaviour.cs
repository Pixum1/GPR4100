using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardBehaviour : MonoBehaviour
{
    private GameManager gm;
    private GuardVision gVision;
    public int CurrentBehaviour { get { return currentBehaviour; } set { currentBehaviour = value; } }
    public enum EBehaviour { patrolling, chasing }
    private int currentBehaviour;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        gVision = GetComponentInChildren<GuardVision>();
    }
    private void Start()
    {
        gm.AddGuard(this);
    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    AlarmGuards();
        //}
    }
    public void AlarmGuards()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        currentBehaviour = 1;
        gVision.LastKnownPlayerPos = player.transform.position;
        Debug.Log("Switch");
    }
    private void OnDestroy()
    {
        gm.RemoveGuard(this);
    }
}
