using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.AI;

public class NavMeshBaker : MonoBehaviour
{
    private void Start()
    {
        NavMeshBuilder.BuildNavMesh();
    }
}
