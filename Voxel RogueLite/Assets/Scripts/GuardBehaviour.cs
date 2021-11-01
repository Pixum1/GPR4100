using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardBehaviour : MonoBehaviour
{
    private GameManager gm;
    private GuardVision gVision;
    public bool Alarmed { get { return alarmed; } set { alarmed = value; } }
    private bool alarmed = false;
    public EBehaviour CurrentBehaviour { get { return currentBehaviour; } set { currentBehaviour = value; } }
    public enum EBehaviour { patrolling, chasing }
    private EBehaviour currentBehaviour;

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
        alarmed = true;
        PlayerController player = FindObjectOfType<PlayerController>();
        currentBehaviour = EBehaviour.chasing;
        gVision.LastKnownPlayerPos = player.transform.position;
        Debug.Log("Switch");
    }
    private void OnDestroy()
    {
        gm.RemoveGuard(this);
    }
}
