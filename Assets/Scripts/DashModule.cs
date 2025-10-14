using UnityEngine;

public class DashModule : MonoBehaviour
{
    [Header("References")]
    public Camera playerCamera;
    public Rigidbody rb;
    public PlayerController playerController;
    public Transform playerTransform;
    public Transform cameraTransform;

    [Header("Dash")]
    public Vector3 direction = Vector3.forward;
    public float dashCharge = 0.5f;
    public float currentDashCharge;
    public float speed = 50;

    [Header("Special effects")]
    public int dashingFOV = 110;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController>();
        playerTransform = GetComponent<Transform>();

        playerCamera = GetComponentInChildren<Camera>();
        cameraTransform = GetComponentInChildren<Transform>();

        currentDashCharge = dashCharge;
    }
    void Update()
    {
        if (playerController.groundCollision && playerController.groundRay && currentDashCharge < dashCharge)
        {
            currentDashCharge += Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftShift) && currentDashCharge > 0)
        {
            Dash();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || currentDashCharge <= 0)
        {
            StopDash();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || (playerController.groundCollision && playerController.groundRay))
        {
            playerController.speedLimit = SpeedLimit.HardLimit;
        }
    }
    private void Dash()
    {
        // camera effects
        if (playerCamera.fieldOfView < dashingFOV)
        {
            playerCamera.fieldOfView += Time.deltaTime;
        }

        // calculate direction based on camera (X) and player (Y) rotation
        direction = Quaternion.Euler(
            cameraTransform.rotation.eulerAngles.x,
            playerTransform.rotation.eulerAngles.y,
            0
        ) * Vector3.forward;

        // rb.AddForce(direction * speed, ForceMode.Impulse);
        rb.linearVelocity = direction * speed;

        rb.useGravity = false;

        playerController.speedLimit = SpeedLimit.SoftLimit;

        currentDashCharge -= Time.deltaTime;
    }

    private void StopDash()
    {
        while (playerCamera.fieldOfView > 90)
        {
            playerCamera.fieldOfView -= Time.deltaTime;
        }

        rb.useGravity = true;
    }
}
