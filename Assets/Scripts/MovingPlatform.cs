using UnityEngine;

public enum MovementAxis
{
    rightLeft,
    upDown,
    forwardBackward
}

public class MovingObject : MonoBehaviour
{
    [Header("References")]
    public Rigidbody rb;

    [Header("Platform Movement")]
    public MovementAxis MovementAxis;
    private Vector3 direction;
    public float distanceToEnd;
    public float speed = 1f;

    [HideInInspector] public float startingPositionInAxis;
    [HideInInspector] public float endingPositionInAxis;
    [HideInInspector] public float positionInAxis;


    [Header("Gizmos")]
    public Color gizmosColor = new Color(255, 255, 0, 150);

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        switch (MovementAxis)
        {
            case MovementAxis.rightLeft:
                direction = (distanceToEnd > 0) ? Vector3.right : Vector3.left;
                rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;

                startingPositionInAxis = transform.position.x;
                endingPositionInAxis = startingPositionInAxis + distanceToEnd;
                break;

            case MovementAxis.upDown:
                direction = (distanceToEnd > 0) ? Vector3.up : Vector3.down;
                rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;

                startingPositionInAxis = transform.position.y;
                endingPositionInAxis = startingPositionInAxis + distanceToEnd;
                break;

            case MovementAxis.forwardBackward:
                direction = (distanceToEnd > 0) ? Vector3.forward : Vector3.back;
                rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;

                startingPositionInAxis = transform.position.z;
                endingPositionInAxis = startingPositionInAxis + distanceToEnd;
                break;
        }
    }

    void FixedUpdate()
    {
        positionInAxis = GetAxisValue(transform.position, MovementAxis);

        if (positionInAxis <= Mathf.Min(startingPositionInAxis, endingPositionInAxis))
        {
            direction = SetAxisValue(direction, 1, MovementAxis);
        }

        if (positionInAxis >= Mathf.Max(startingPositionInAxis, endingPositionInAxis))
        {
            direction = SetAxisValue(direction, -1, MovementAxis);
        }
        
        rb.linearVelocity = direction * speed;
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

    private float GetAxisValue(Vector3 vector, MovementAxis movementAxis)
    {
        switch (MovementAxis)
        {
            case MovementAxis.rightLeft:
                return vector.x;
            case MovementAxis.upDown:
                return vector.y;
            case MovementAxis.forwardBackward:
                return vector.z;
            default:
                return 0f;
        }
    }

    private Vector3 SetAxisValue(Vector3 vector, float value, MovementAxis movementAxis)
    {
        switch (MovementAxis)
        {
            case MovementAxis.rightLeft:
                vector = new Vector3(value, vector.y, vector.z);
                return vector;
            case MovementAxis.upDown:
                vector = new Vector3(vector.x, value, vector.z);
                return vector;
            case MovementAxis.forwardBackward:
                vector = new Vector3(vector.x, vector.y, value);
                return vector;
            default:
                return Vector3.zero;
        }
    }
}