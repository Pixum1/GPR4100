using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FindCamera : MonoBehaviour
{
    CinemachineVirtualCamera cCam;

    private void Awake()
    {
        cCam = FindObjectOfType<CinemachineVirtualCamera>();
    }
    private void Start()
    {
        cCam.LookAt = this.gameObject.transform;
        cCam.Follow = this.gameObject.transform;
    }
}
