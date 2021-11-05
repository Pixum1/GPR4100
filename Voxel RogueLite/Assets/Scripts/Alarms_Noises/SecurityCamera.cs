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

    [SerializeField]
    private float visionRadius;

    [SerializeField]
    private GameObject radiusOrigin;

    private PlayerController player;

    GameManager gm;

    Vector3 playerPos;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        player = FindObjectOfType<PlayerController>();
    }
    private void Update()
    {
        if (dir == "left")
            RotateLeft();
        else if (dir == "right")
            RotateRight();
        else
            RotateLeft();

        CheckForPlayer();
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

    void CheckForPlayer()
    {
        if (Vector3.Distance(radiusOrigin.transform.position, player.transform.position) <= visionRadius)
        {
            RaycastHit hit;
            Vector3 direction = player.transform.position - transform.position; //Vector from guard position to player position

            //cast ray towards player's position | collide with everything
            if (Physics.Raycast(transform.position, direction, out hit, Mathf.Infinity))
            {
                //if ray collides with player's collider
                if (hit.collider.CompareTag("Player"))
                {
                    inLOS = true;
                    playerPos = player.transform.position;
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
        else
            inLOS = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(radiusOrigin.transform.position, visionRadius);
    }
}
