using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Movement
    [SerializeField]
    private float m_moveSpeed = 5f;
    private float maxSpeed;
    private Vector3 currMoveDir;
    private Rigidbody rb;
    //

    bool isLeaving;
    public bool GetIsLeaving() { return isLeaving; }

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
    private void OnTriggerEnter(Collider _other)
    {
        //if player collects coin
        if(_other.CompareTag("Coin"))
        {
            Destroy(_other.gameObject); //delete coin
        }
    }
}
