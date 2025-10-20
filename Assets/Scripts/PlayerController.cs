using System;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public Rigidbody rb;
    public DashModule playerDash;
    public ControlSpeed controlSpeed;

    [Header("Jump controls")]
    public float jumpSpeed = 5;
    public float jumpCooldown = 0.4f;
    public float currentJumpCooldown;
    public bool grounded = false;
    // did this so the player can't jump by touching the side of a "Floor" object (collision) or jump before touching the ground (Raycast)
    [HideInInspector] public bool groundCollision = false;
    [HideInInspector] public bool groundRay = false;
    // you could loose the ability to jump if you were on the ground and left the collision of another "Floor" obstacle so I created a list of the current collisions
    public List<GameObject> currentCollisions = new List<GameObject>();

    [Header("Movement controls")]
    public float speed = 5;

    [Header("Modules")]
    public bool dashModule = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerDash = GetComponent<DashModule>();
        controlSpeed = GetComponent<ControlSpeed>();

        if (dashModule)
        {
            playerDash.enabled = true;
        }
    }
    void FixedUpdate()
    {
        // do some raycasts + parent moving objects it detects
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo, 1.1f))
        {
            groundRay = true;
            if (hitInfo.collider.tag == "Moving")
            {
                transform.parent = hitInfo.transform;
            }
        }
        else
        {
            groundRay = false;
            transform.parent = null;
        }

        if (groundRay && groundCollision)
            grounded = true;
        else
            grounded = false;

        // lower jump cooldown also here so you don't have a lower cooldown
        if (currentJumpCooldown > 0)
            currentJumpCooldown -= Time.deltaTime;

        Move();

        if (grounded && currentJumpCooldown <= 0 && Input.GetKey(KeyCode.Space))
        {
            Jump();
        }
    }

    // if you collide with two "Floor" objects at the same time one won't register
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            rb.linearDamping = 5;

            currentCollisions.Add(collision.gameObject);
            groundCollision = true;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            rb.linearDamping = 0;

            currentCollisions.Remove(collision.gameObject);

            if (currentCollisions.Count <= 0)
                groundCollision = false;
        }
    }

    private void Move()
    {
        transform.Translate(
            new Vector3(
                speed * Time.deltaTime * Input.GetAxis("Horizontal"),
                0,
                speed * Time.deltaTime * Input.GetAxis("Vertical")),
                Space.Self);
    }
    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
        currentJumpCooldown = jumpCooldown;

        controlSpeed.speedLimit = SpeedLimit.Jumping;
    }
}
