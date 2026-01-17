using FMODUnity;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    private Rigidbody rb;
    private DashModule playerDash;
    private Transform spawnpoint;
    [SerializeField] private StudioEventEmitter FMODLand;
    [SerializeField] private StudioEventEmitter FMODDash;

    [Header("Input")]
    private bool moveInput = false;
    private bool jumpInput = false;
    private bool dashInput = false;
    private bool firstDash = true;
    private bool respawnInput = false;

    [Header("Jump control")]
    [SerializeField] private float jumpSpeed = 5;
    [SerializeField] private float jumpCooldown = 0.4f;

    private float currentJumpCooldown;
    private bool grounded = false;
    private bool firstGrounded = true;
    private bool groundCollision = false;
    private bool groundRay = false;

    [Header("Movement control")]
    [SerializeField] private float speed = 5;

    [Header("Constrains")]
    [SerializeField] private bool spawnInStart = true;
    [HideInInspector] public bool disableWalking = false;
    [HideInInspector] public bool disableInput = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerDash = GetComponent<DashModule>();
        spawnpoint = GameObject.FindGameObjectWithTag("Respawn").GetComponent<Transform>();

        respawnInput = spawnInStart;
        GameManager.LockCursor(true);
    }

    void Update()
    {
        if (groundRay && groundCollision)
        {
            if (firstGrounded)
            {
                FMODLand.Play();
                firstGrounded = false;
            }
            grounded = true;
        }
        else
        {
            grounded = false;
            firstGrounded = true;
        }

        if (currentJumpCooldown > 0)
            currentJumpCooldown -= Time.deltaTime;

        if (grounded && playerDash.currentDashCharge < playerDash.maxDashCharge)
            playerDash.currentDashCharge += Time.deltaTime;

        Mathf.Clamp(currentJumpCooldown, 0, jumpCooldown);
        Mathf.Clamp(playerDash.currentDashCharge, 0, playerDash.maxDashCharge);

        if (disableInput)
            return;

        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
            moveInput = true;
        else
            moveInput = false;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (firstDash)
            {
                FMODDash.Play();
                firstDash = false;
            }
            dashInput = true;
        }
        else
        {
            dashInput = false;
            firstDash = true;
        }

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

        if (!disableWalking && moveInput)
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

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Respawn()
    {
        rb.linearVelocity = Vector3.zero;
        transform.position = spawnpoint.position;
        respawnInput = false;
    }

    public void DisableCameraMove(bool disable)
    {
        FindFirstObjectByType<CameraController>().enabled = !disable;
    }
}
