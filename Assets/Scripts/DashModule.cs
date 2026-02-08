using UnityEngine;

public class DashModule : MonoBehaviour
{
    [Header("References")]
    private Rigidbody rb;
    private PlayerController playerController;
    private Camera playerCamera;

    [Header("Dash")]
    private Vector3 dashDirection = Vector3.forward;
    public float maxDashCharge = 0.5f;
    [HideInInspector] public float currentDashCharge;
    [SerializeField] private float dashSpeed = 10;

    [Header("Special effects")]
    [SerializeField] private int dashingFOV = 110;
    [SerializeField] private float FOVIncreaseSpeed = 5;
    [SerializeField] private float FOVDecreaseSpeed = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController>();
        playerCamera = GetComponentInChildren<Camera>();

        currentDashCharge = maxDashCharge;
    }

    public void Dash()
    {
        playerController.disableWalking = true;

        if (playerCamera.fieldOfView < dashingFOV)
        {
            playerCamera.fieldOfView += FOVIncreaseSpeed;
        }

        // calculate direction based on camera (X) and player (Y) rotation
        dashDirection = Quaternion.Euler(
            playerCamera.transform.rotation.eulerAngles.x,
            transform.rotation.eulerAngles.y,
            0
        ) * Vector3.forward;

        rb.linearVelocity = dashDirection * dashSpeed;

        rb.useGravity = false;

        // playerController.speedLimit = SpeedLimit.Dash;

        currentDashCharge -= Time.deltaTime;
    }

    public void StopDash()
    {
        if (playerCamera.fieldOfView > 90)
        {
            playerCamera.fieldOfView -= FOVDecreaseSpeed;
        }

        rb.useGravity = true;

        // playerController.speedLimit = SpeedLimit.Airborn;
        playerController.disableWalking = false;
    }
}
