using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Serialization.Formatters;
using NUnit.Framework.Internal.Filters;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public Rigidbody rb;
    public DashModule playerDash;
    public ControlSpeed controlSpeed;
    public Transform spawnpoint;

    [Header("Input")]
    public bool moveInput = false;
    public bool jumpInput = false;
    public bool dashInput = false;
    public bool resetInput = false;

    [Header("Jump control")]
    public float jumpSpeed = 5;
    public float jumpCooldown = 0.4f;
    public float currentJumpCooldown;
    public bool grounded = false;
    // did this so the player can't jump by touching the side of a "Floor" object (collision) or jump before touching the ground (Raycast)
    [HideInInspector] public bool groundCollision = false;
    [HideInInspector] public bool groundRay = false;
    // you could loose the ability to jump if you were on the ground and left the collision of another "Floor" obstacle so I created a list of the current collisions

    [Header("Movement control")]
    public float speed = 5;

    [Header("Speed control")]
    public SpeedLimit speedLimit = SpeedLimit.Grounded;
    public float maxSpeed = 7;
    public bool limitYAxis = false;

    [Header("Constrains")]
    public bool disableMovement = false;

    [Header("Modules")]
    public bool dashModule = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerDash = GetComponent<DashModule>();
        controlSpeed = GetComponent<ControlSpeed>();
        spawnpoint = GameObject.FindGameObjectWithTag("Respawn").GetComponent<Transform>();

        if (dashModule)
        {
            playerDash.enabled = true;
        }
    }

    void Update()
    {
        if (groundRay && groundCollision)
            grounded = true;
        else
            grounded = false;

        // lower jump cooldown also here so you don't have a lower cooldown
        if (currentJumpCooldown > 0)
            currentJumpCooldown -= Time.deltaTime;

        if (grounded && playerDash.currentDashCharge < playerDash.dashCharge)
            playerDash.currentDashCharge += Time.deltaTime;

        Mathf.Clamp(currentJumpCooldown, 0, jumpCooldown);
        Mathf.Clamp(playerDash.currentDashCharge, 0, playerDash.dashCharge);

        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
            moveInput = true;
        else
            moveInput = false;

        if (Input.GetKey(KeyCode.LeftShift))
            dashInput = true;
        else
            dashInput = false;

        if (Input.GetKey(KeyCode.Space))
            jumpInput = true;
        else
            jumpInput = false;

        if (Input.GetKeyDown(KeyCode.R)) 
            ResetScene();
    }
    
    void FixedUpdate()
    {
        // do some raycasts + parent moving objects it detects
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo, 1.1f))
            groundRay = true;
        else
            groundRay = false;

        if (dashInput && playerDash.currentDashCharge > 0)
        {
            playerDash.Dash();
        }
        else
        {
            playerDash.StopDash();
        }

        if (jumpInput && grounded && currentJumpCooldown <= 0)
        {
            Jump();
        }
        
        if (!disableMovement && moveInput)
            Move();

        controlSpeed.LimitSpeed(maxSpeed, speedLimit, limitYAxis);
    }

    // if you collide with two "Floor" objects at the same time one won't register
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            groundCollision = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            groundCollision = false;
        }
    }

    private void Move()
    {
        Vector3 lastLinearVelocity = rb.linearVelocity;

        Vector3 movement = new Vector3(
            speed * Input.GetAxis("Horizontal"),
            lastLinearVelocity.y,
            speed * Input.GetAxis("Vertical"));

        rb.linearVelocity = Quaternion.Euler(0, transform.eulerAngles.y, 0) * movement;
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
        currentJumpCooldown = jumpCooldown;

        speedLimit = SpeedLimit.Airborn;
    }

    private void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    } 

    public void Respawn()
    {
        transform.position = spawnpoint.position;
    }
}
