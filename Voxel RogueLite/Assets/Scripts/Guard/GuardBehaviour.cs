using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    private PlayerController player;

    [SerializeField]
    private LineRenderer line;

    [SerializeField]
    private Material[] mats;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        gm = FindObjectOfType<GameManager>();
        gVision = GetComponentInChildren<GuardVision>();
    }

    private void Start()
    {
        gm.AddGuard(this);
    }
    public void AlarmGuards()
    {
        if(player != null)
        {
            Debug.Log("Called");
            alarmed = true;
            gVision.LastKnownPlayerPos = player.transform.position;
            currentBehaviour = EBehaviour.chasing;
        }
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
