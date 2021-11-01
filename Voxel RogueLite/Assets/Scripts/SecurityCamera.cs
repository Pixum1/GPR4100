using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    [SerializeField]
    private float rotLeft, rotRight;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float CameraSmooth;
    [SerializeField]
    private float alarmTimer = 2;
    private string dir = "left";
    private bool inLOS = false;

    GameManager gm;

    Vector3 playerPos;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
    }
    private void Update()
    {
        if (dir == "left")
            RotateLeft();
        else if (dir == "right")
            RotateRight();
        else
            RotateLeft();
    }
    private void RotateLeft()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, rotLeft, 0), speed * Time.deltaTime);
        if (Quaternion.Angle(transform.rotation, Quaternion.Euler(0, rotLeft, 0)) < CameraSmooth)
            dir = "right";
    }
    private void RotateRight()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, rotRight, 0), speed * Time.deltaTime);
        if (Quaternion.Angle(transform.rotation, Quaternion.Euler(0, rotRight, 0)) < CameraSmooth)
            dir = "left";
    }
    private void FollowPlayer()
    {
        transform.LookAt(playerPos);
    }
    private void OnTriggerStay(Collider _other)
    {
        PlayerController player = _other.GetComponent<PlayerController>();
        if (player != null)
        {
            RaycastHit hit;
            Vector3 direction = _other.transform.position - transform.position; //Vector from guard position to player position

            //cast ray towards player's position | collide with everything
            if (Physics.Raycast(transform.position, direction, out hit, Mathf.Infinity))
            {
                //if ray collides with player's collider
                if (hit.collider.CompareTag("Player"))
                {
                    inLOS = true;
                    playerPos = _other.transform.position;
                    dir = null;
                    FollowPlayer();
                    StartCoroutine(AlarmTimer(alarmTimer));
                }
                else
                {
                    inLOS = false;
                }
            }
        }
    }
    private void OnTriggerExit(Collider _other)
    {
        PlayerController player = _other.GetComponent<PlayerController>();
        if (player != null)
        {
            inLOS = false;
        }
    }
    private IEnumerator AlarmTimer(float _time)
    {
        if(inLOS)
        {
            if(_time > 0)
            {
                yield return new WaitForSeconds(1);
                _time--;
                StartCoroutine(AlarmTimer(_time));
            }
            else
            {
                foreach (var guard in gm.Guards)
                {
                    if (guard.gameObject != null)
                    {
                        guard.AlarmGuards();
                    }
                }
            }
        }
        else
            StopAllCoroutines();
    }
}
