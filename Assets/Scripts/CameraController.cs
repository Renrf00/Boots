using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("References")]
    public GameObject player;

    [Header("Rotation")]
    public float rotationSpeed;
    public float rotationXClamp = 45;
    private float rotationX;
    private float rotationY;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        rotationX += -rotationSpeed * Time.deltaTime * Input.GetAxis("Mouse Y");
        rotationY += rotationSpeed * Time.deltaTime * Input.GetAxis("Mouse X");
        
        rotationX = Mathf.Clamp(rotationX, -rotationXClamp, rotationXClamp);

        player.transform.rotation = Quaternion.Euler(
                0,
                rotationY,
                0
            );

        transform.rotation = Quaternion.Euler(
                rotationX,
                player.transform.rotation.eulerAngles.y, // if 0 camera will not rotate vs world not parent
                player.transform.rotation.eulerAngles.z
            );
    }
}
