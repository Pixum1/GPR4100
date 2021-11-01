using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Movement variables
    [SerializeField]
    private float m_moveSpeed = 5f; //player's movementspeed
    private float maxSpeed; //player's maximum allowed movementspeed
    private Vector3 currMoveDir; //direction that the player moves in
    private Rigidbody rb;
    #endregion
    [SerializeField]
    private Transform m_playerSprites;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        maxSpeed = m_moveSpeed / 12.5f; //set the maximum speed of the player according to its initial speed
    }
    private void Update()
    {
        #region Movement Direction
        currMoveDir = Vector3.zero;

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        if(x != 0)
        {
            //if +, move to lower right || if -, move to upper left (- x - = +)
            currMoveDir += new Vector3(x, 0, -x);
        }
        if (z != 0)
        {
            //if +, move to upper right || if -, move to lower left
            currMoveDir += new Vector3(z, 0, z);
        }
        #endregion

        #region RotatePlayerSprites
        if (currMoveDir == new Vector3(0,0,2)) //move upperleft
            m_playerSprites.rotation = Quaternion.Euler(new Vector3(-90, m_playerSprites.rotation.y, -180));

        if (currMoveDir == new Vector3(1, 0, 1)) //move up
            m_playerSprites.rotation = Quaternion.Euler(new Vector3(-90, m_playerSprites.rotation.y, -135));

        if (currMoveDir == new Vector3(2, 0, 0)) //move upperright
            m_playerSprites.rotation = Quaternion.Euler(new Vector3(-90, m_playerSprites.rotation.y, -90));

        if (currMoveDir == new Vector3(1, 0, -1)) //move right
            m_playerSprites.rotation = Quaternion.Euler(new Vector3(-90, m_playerSprites.rotation.y, -45));

        if (currMoveDir == new Vector3(0, 0, -2)) //move lowerright
            m_playerSprites.rotation = Quaternion.Euler(new Vector3(-90, m_playerSprites.rotation.y, 0));

        if (currMoveDir == new Vector3(-1, 0, -1)) //move down
            m_playerSprites.rotation = Quaternion.Euler(new Vector3(-90, m_playerSprites.rotation.y, 45));

        if (currMoveDir == new Vector3(-2, 0, 0)) //move lowerleft
            m_playerSprites.rotation = Quaternion.Euler(new Vector3(-90, m_playerSprites.rotation.y, 90));

        if (currMoveDir == new Vector3(-1, 0, 1)) //move left
            m_playerSprites.rotation = Quaternion.Euler(new Vector3(-90, m_playerSprites.rotation.y, 135));
        #endregion
    }
    private void FixedUpdate()
    {
        #region Rigidbody Movement
        rb.AddForce(currMoveDir * m_moveSpeed);

        #region limit speed
        //if player gets to fast
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed; //limit its speed
        }
        #endregion

        #endregion
    }
}
