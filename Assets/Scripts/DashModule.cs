using UnityEngine;

public class DashModule : MonoBehaviour
{
    [Header("References")]
    private Rigidbody rb;
    private PlayerController playerController;
    private Camera playerCamera;

    [Header("Dash")]
    private Vector3 direction = Vector3.forward;
    public float maxDashCharge = 0.5f;
    [HideInInspector] public float currentDashCharge;
    public float dashSpeed = 50;

    [Header("Special effects")]
    public int dashingFOV = 110;
    public float FOVIncreaseSpeed = 5;
    public float FOVDecreaseSpeed = 1;

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
        direction = Quaternion.Euler(
            playerCamera.transform.rotation.eulerAngles.x,
            transform.rotation.eulerAngles.y,
            0
        ) * Vector3.forward;

        rb.linearVelocity = direction * dashSpeed;

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
