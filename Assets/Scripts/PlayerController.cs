using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public Rigidbody rb;
    public DashModule playerDash;

    [Header("Jump controls")]
    public float jumpSpeed = 5;
    public float jumpCooldown = 0.4f;
    public float currentJumpCooldown;
    // did this so the player can't jump by touching the side of a "Floor" object (collision) or jump before touching the ground (Raycast)
    public bool groundCollision = false;
    public bool groundRay = false;
    // you could loose the ability to jump if you were on the ground and left the collision of another "Floor" obstacle so I created a list of the current collisions
    public List<GameObject> currentCollisions = new List<GameObject>();

    [Header("Movement controls")]
    public float speed = 5;
    public float maxSpeed = 7;

    [Header("Modules")]
    public bool dashModule = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerDash = GetComponent<DashModule>();

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

        // lower jump cooldown also here so you don't have a lower cooldown
        if (currentJumpCooldown > 0)
            currentJumpCooldown -= Time.deltaTime;

        Move();

        if (groundCollision && groundRay && currentJumpCooldown <= 0 && Input.GetKey(KeyCode.Space))
        {
            Jump();
        }

        LimitSpeed();
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
    }

    private void LimitSpeed()
    {
        Vector3 horizontalVelocity = new Vector3(
            rb.linearVelocity.x,
            0,
            rb.linearVelocity.z);

        Vector3 newLinearVelocity;

        //tend horizontal speed to less than max speed
        if (horizontalVelocity.magnitude > maxSpeed)
        {
            newLinearVelocity =
                // I'm not directly multiplying horizontalVelocity by maxSpeed so the player decelerates gradualy instead of suddenly
                horizontalVelocity.normalized * (maxSpeed + (horizontalVelocity.magnitude - maxSpeed) / 2) +
                new Vector3(0, rb.linearVelocity.y, 0);
            
            rb.linearVelocity = newLinearVelocity;
        }
    }
}
