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
    private bool isGrounded = false;
    private bool firstGrounded = true;
    private bool touchingGround = false;
    private bool nearGround = false;

    [Header("Movement control")]
    [SerializeField] private float walkingSpeed = 5;

    [Header("Constrains")]
    [SerializeField] private bool spawnInStart = true;
    public bool disableWalking = false;
    public bool disableInput = false;

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
        if (nearGround && touchingGround)
        {
            if (firstGrounded)
            {
                FMODLand.Play();
                firstGrounded = false;
            }
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
            firstGrounded = true;
        }

        if (currentJumpCooldown > 0)
            currentJumpCooldown -= Time.deltaTime;

        if (isGrounded && playerDash.currentDashCharge < playerDash.maxDashCharge)
            playerDash.currentDashCharge += Time.deltaTime;

        Mathf.Clamp(currentJumpCooldown, 0, jumpCooldown);
        Mathf.Clamp(playerDash.currentDashCharge, 0, playerDash.maxDashCharge);

        if (disableInput)
        {
            moveInput = false;
            jumpInput = false;
            dashInput = false;
            respawnInput = false;
            return;
        }

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
            nearGround = true;
        else
            nearGround = false;

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

        if (jumpInput && isGrounded && currentJumpCooldown <= 0)
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
            touchingGround = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            touchingGround = false;
        }
    }

    private void Move()
    {
        Vector3 lastLinearVelocity = rb.linearVelocity;

        Vector3 movement = new Vector3(
            walkingSpeed * Input.GetAxis("Horizontal"),
            lastLinearVelocity.y,
            walkingSpeed * Input.GetAxis("Vertical"));

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
