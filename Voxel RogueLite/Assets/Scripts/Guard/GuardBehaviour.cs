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

    [SerializeField]
    private LineRenderer line;

    [SerializeField]
    private Material[] mats;

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
        if(currentBehaviour != EBehaviour.chasing)
        {
            alarmed = true;
            currentBehaviour = EBehaviour.searching;
        }
    }
    private void Update()
    {
        UpdateLineColor();
    }

    private void UpdateLineColor()
    {
        if(currentBehaviour == EBehaviour.patrolling)
            line.material = mats[0];
        else if(currentBehaviour == EBehaviour.searching)
            line.material = mats[1];
        else if(currentBehaviour == EBehaviour.chasing)
            line.material = mats[2];
    }
    private void OnDestroy()
    {
        gm.RemoveGuard(this);
    }
}
