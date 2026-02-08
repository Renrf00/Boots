using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;

    [Header("Rotation")]
    [SerializeField] private ScriptableNumber sensitibity;
    [SerializeField] private float rotationXHighClamp = 90;
    [SerializeField] private float rotationXLowClamp = -60;
    private float rotationX;
    private float rotationY;

    void Update()
    {
        // rotate based on mouse input
        rotationX += -sensitibity.number * Time.deltaTime * Input.GetAxis("Mouse Y");
        rotationY += sensitibity.number * Time.deltaTime * Input.GetAxis("Mouse X");

        rotationX = Mathf.Clamp(rotationX, -rotationXHighClamp, -rotationXLowClamp);

        // rotate player character's Y axis
        player.rotation = Quaternion.Euler(
                0,
                rotationY,
                0
            );

        // rotate camera's X axis
        transform.rotation = Quaternion.Euler(
                rotationX,
                player.rotation.eulerAngles.y, // if 0 camera will not rotate vs world not parent
                player.rotation.eulerAngles.z
            );
    }
}
