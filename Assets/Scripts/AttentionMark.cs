using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttentionMark : MonoBehaviour
{
    [SerializeField]
    private Sprite chaseExclam;
    [SerializeField]
    private Sprite searchExclam;

    private SpriteRenderer exclamRenderer;

    private GuardBehaviour gBehaviour;

    private void Awake()
    {
        gBehaviour = GetComponentInParent<GuardBehaviour>();
        exclamRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (gBehaviour.CurrentBehaviour == GuardBehaviour.EBehaviour.chasing)
            ShowExclam(chaseExclam);

        else if (gBehaviour.CurrentBehaviour == GuardBehaviour.EBehaviour.searching)
            ShowExclam(searchExclam);

        else if (gBehaviour.CurrentBehaviour == GuardBehaviour.EBehaviour.patrolling)
            HideExclam();
    }

    private void ShowExclam(Sprite _exclamSprite)
    {
        exclamRenderer.enabled = true;
        exclamRenderer.sprite = _exclamSprite;
    }
    private void HideExclam()
    {
        exclamRenderer.enabled = false;
        exclamRenderer.sprite = null;
    }
}
