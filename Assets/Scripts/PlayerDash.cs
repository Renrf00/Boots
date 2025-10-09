using Unity.Mathematics;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [Header("References")]
    public Rigidbody rb;
    public PlayerController playerController;
    public Transform playerTransform;
    public Transform cameraTransform;

    [Header("Dash")]
    public Vector3 direction = Vector3.forward;
    public float dashCharge = 0.5f;
    public float currentDashCharge;
    public float speed = 50;

    void Start()
    {
        currentDashCharge = dashCharge;
    }
    void Update()
    {

        Debug.Log("" + direction);
        if (Input.GetKey(KeyCode.LeftShift) && currentDashCharge > 0)
        {
            direction = Quaternion.Euler(
                cameraTransform.rotation.eulerAngles.x,
                playerTransform.rotation.eulerAngles.y,
                0
            ) * Vector3.forward;

            rb.useGravity = false;
            rb.AddForce(direction * speed, ForceMode.Force);

            currentDashCharge -= Time.deltaTime;
        }
        else
        {
            rb.useGravity = true;
            if (playerController.groundCollision && playerController.groundRay && currentDashCharge < dashCharge)
            {
                currentDashCharge += Time.deltaTime;
            }
        }
        
    }
}
