using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField]
    private float groundDrive = 40;
    [SerializeField]
    private float airDrive = 20;

    [SerializeField]
    private Vector2 dragCoefficients;

    [SerializeField]
    private float jumpImpulse = 10;

    [SerializeField]
    private float groundDistance = 0.4f;

    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private float jumpThreshold = 0.1f; // seconds
    private float lastTimeOnGround = float.NegativeInfinity;
    private float lastTimePressedJump = float.NegativeInfinity;

    private Rigidbody2D rigidbody;
    private float moveDir = 0;
    private bool jump = false;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float drive = IsOnGround() ? groundDrive : airDrive; 
        rigidbody.AddForce(Vector3.right * drive * moveDir);

        Vector2 drag;
        drag.x = -rigidbody.velocity.x * dragCoefficients.x;
        drag.y = -rigidbody.velocity.y * dragCoefficients.y;
        rigidbody.AddForce(drag);

        if (jump)
        {
            rigidbody.AddForce(jumpImpulse * Vector3.up, ForceMode2D.Impulse);
            jump = false;
        }
    }

    void Update()
    {
        moveDir = Input.GetAxis(InputAxes.Horizontal);

        if (IsOnGround())
        {
            lastTimeOnGround = Time.time;
        }

        if (Input.GetButtonDown(InputAxes.Jump))
        {
            lastTimePressedJump = Time.time;
        }

        if (Time.time - lastTimeOnGround <= jumpThreshold && Time.time - lastTimePressedJump <= jumpThreshold)
        {
            jump = true;
            lastTimePressedJump = float.NegativeInfinity;
        }
    }

    private bool IsOnGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, groundDistance, groundLayer);
        return (hit.collider != null);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + groundDistance * Vector3.down);
    }
}
