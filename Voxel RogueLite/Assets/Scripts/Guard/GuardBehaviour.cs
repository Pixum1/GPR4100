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
    public enum EBehaviour { patrolling, chasing, searching }
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
    public void AlarmGuards()
    {
        alarmed = true;
        currentBehaviour = EBehaviour.chasing;
        PlayerController player = FindObjectOfType<PlayerController>();
        gVision.LastKnownPlayerPos = player.transform.position;
    }
    public void SearchNoise()
    {
        alarmed = true;
        currentBehaviour = EBehaviour.searching;
    }
    private void OnDestroy()
    {
        gm.RemoveGuard(this);
    }
}
