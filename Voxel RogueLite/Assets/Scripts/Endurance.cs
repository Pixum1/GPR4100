using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endurance : MonoBehaviour
{
    public float GetEndurance { get { return endurance; } }
    private float endurance;
    public float MaxEndurance { get { return maxEndurance; } }
    [SerializeField]
    private float maxEndurance;
    public bool Excercising { get { return excercising; } set { excercising = value; } }
    private bool excercising;

    public bool AllowSprint { get { return allowSprint; } }
    private bool allowSprint = true;

    private bool depleted;

    [SerializeField]
    ParticleSystem particles;

    [SerializeField]
    private float depletionMultiplier = 1;

    private void Start()
    {
        endurance = maxEndurance;
    }
    private void Update()
    {
        if(!depleted && excercising)
        {
            depleted = false;
            endurance -= Time.deltaTime * depletionMultiplier;
            particles.Play();
        }
        else if (!depleted && !excercising && endurance <= maxEndurance)
        {
            depleted = false;
            endurance += Time.deltaTime;
            particles.Stop();
        }
        if(endurance < 0)
        {
            depleted = true;
        }
        if(depleted)
        {
            endurance += Time.deltaTime;
            particles.Stop();
            allowSprint = false;
            if(endurance >= maxEndurance)
            {
                allowSprint = true;
                depleted = false;
            }
        }
    }
}
