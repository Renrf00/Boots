using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("References")]
    public Transform player;

    [Header("Rotation")]
    public ScriptableNumber sensibility;
    public float rotationXHighClamp = 90;
    public float rotationXLowClamp = -60;
    private float rotationX;
    private float rotationY;

    void Update()
    {
        // rotate based on mouse input
        rotationX += -sensibility.number * Time.deltaTime * Input.GetAxis("Mouse Y");
        rotationY += sensibility.number * Time.deltaTime * Input.GetAxis("Mouse X");

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
