using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public Rigidbody rb;
    public DashModule playerDash;
    public Transform spawnpoint;

    [Header("Input")]
    public bool moveInput = false;
    public bool jumpInput = false;
    public bool dashInput = false;
    public bool respawnInput = false;

    [Header("Jump control")]
    public float jumpSpeed = 5;
    public float jumpCooldown = 0.4f;
    public float currentJumpCooldown;
    public bool grounded = false;
    public bool groundCollision = false;
    public bool groundRay = false;

    [Header("Movement control")]
    public float speed = 5;

    // [Header("Speed control")]
    // public SpeedLimit speedLimit = SpeedLimit.Grounded;
    // public float maxSpeed = 7;
    // public bool limitYAxis = false;

    [Header("Constrains")]
    public bool spawnInStart = true;
    public bool disableMovement = false;

    // [Header("Modules")]
    // public bool dashModule = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerDash = GetComponent<DashModule>();
        spawnpoint = GameObject.FindGameObjectWithTag("Respawn").GetComponent<Transform>();

        if (spawnInStart)
        {
            Respawn();
        }
        // if (dashModule)
        // {
        //     playerDash.enabled = true;
        // }
    }

    void Update()
    {
        if (groundRay && groundCollision)
            grounded = true;
        else
            grounded = false;

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
            respawnInput = true;
    }

    void FixedUpdate()
    {
        // do some raycasts + parent moving objects it detects
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo, 1.1f))
            groundRay = true;
        else
            groundRay = false;

        if (respawnInput)
        {
            moveInput = false;
            jumpInput = false;
            dashInput = false;

            Respawn();
        }

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
    }

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

        // speedLimit = SpeedLimit.Airborn;
    }

    // private void ResetScene()
    // {
    //     SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    // }

    public void Respawn()
    {
        rb.linearVelocity = Vector3.zero;
        transform.position = spawnpoint.position;
        respawnInput = false;
    }
}
