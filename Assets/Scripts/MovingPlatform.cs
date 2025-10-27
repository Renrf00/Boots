using UnityEngine;

public enum MovementAxis
{
    rightLeft,
    upDown,
    forwardBackward
}

public class MovingObject : MonoBehaviour
{
    [Header("Platform Movement")]
    public MovementAxis MovementAxis;
    private Vector3 direction;
    private Vector3 startingPosition;
    public float distanceToEnd;
    public float speed = 1f;

    [Header("Gizmos")]
    public Color gizmosColor = new Color(255, 255, 0, 150);

    void Start()
    {
        startingPosition = transform.position;

        switch (MovementAxis)
        {
            case MovementAxis.rightLeft:
                direction = Vector3.right;
                break;

            case MovementAxis.upDown:
                direction = Vector3.up;
                break;

            case MovementAxis.forwardBackward:
                direction = Vector3.forward;
                break;
        }
    }

    void Update()
    {
        transform.position =  startingPosition + direction * distanceToEnd * (0.5f + Mathf.Sin(Time.time * speed * Mathf.PI) / 2);
    }
    void OnDrawGizmosSelected()
    {
        switch (MovementAxis)
        {
            case MovementAxis.rightLeft:
                direction = Vector3.right;
                break;

            case MovementAxis.upDown:
                direction = Vector3.up;
                break;

            case MovementAxis.forwardBackward:
                direction = Vector3.forward;
                break;
        }

        Gizmos.color = gizmosColor;
        Gizmos.DrawLine(transform.position, transform.position + direction * distanceToEnd);
        Gizmos.DrawCube(transform.position + direction * distanceToEnd + GetComponent<BoxCollider>().center, GetComponent<BoxCollider>().size);
    }
}