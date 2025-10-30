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
    public float FOVincrease = 5;
    public float FOVdecrease = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController>();
        playerTransform = GetComponent<Transform>();

        currentDashCharge = dashCharge;
    }

    public void Dash()
    {
        playerController.disableMovement = true;
        Debug.Log("Dashing");
        // camera effects
        if (playerCamera.fieldOfView < dashingFOV)
        {
            playerCamera.fieldOfView += FOVincrease;
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

        playerController.speedLimit = SpeedLimit.Dash;

        currentDashCharge -= Time.deltaTime;
    }

    public void StopDash()
    {
        if (playerCamera.fieldOfView > 90)
        {
            playerCamera.fieldOfView -= FOVdecrease;
        }

        rb.useGravity = true;

        playerController.speedLimit = SpeedLimit.Airborn;
        playerController.disableMovement = false;
    }
}
