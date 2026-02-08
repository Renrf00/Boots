using UnityEngine;

public enum MovementAxis
{
    rightLeft,
    upDown,
    forwardBackward
}

public class MovingPlatform : MonoBehaviour
{
    [Header("References")]
    private Rigidbody rb;

    [Header("Platform Movement")]
    [SerializeField] private bool MoveWhenPlayer;
    [SerializeField] private MovementAxis MovementAxis;
    private Vector3 platformDirection;
    [SerializeField] private float distanceToEnd;
    [SerializeField] private float platformSpeed = 1f;

    private float startingPositionInAxis;
    private float endingPositionInAxis;
    private float positionInAxis;

    private bool playerOnPlatform;

    [Header("Gizmos")]
    public Color gizmosColor = new Color(255, 255, 0, 150);

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        switch (MovementAxis)
        {
            case MovementAxis.rightLeft:
                platformDirection = (distanceToEnd > 0) ? Vector3.right : Vector3.left;
                rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;

                startingPositionInAxis = transform.position.x;
                endingPositionInAxis = startingPositionInAxis + distanceToEnd;
                break;

            case MovementAxis.upDown:
                platformDirection = (distanceToEnd > 0) ? Vector3.up : Vector3.down;
                rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;

                startingPositionInAxis = transform.position.y;
                endingPositionInAxis = startingPositionInAxis + distanceToEnd;
                break;

            case MovementAxis.forwardBackward:
                platformDirection = (distanceToEnd > 0) ? Vector3.forward : Vector3.back;
                rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;

                startingPositionInAxis = transform.position.z;
                endingPositionInAxis = startingPositionInAxis + distanceToEnd;
                break;
        }
    }

    private void FixedUpdate()
    {
        positionInAxis = GetAxisValue(transform.position);

        if (!MoveWhenPlayer)
        {
            if (positionInAxis <= Mathf.Min(startingPositionInAxis, endingPositionInAxis))
            {
                platformDirection = SetAxisValue(platformDirection, 1);
            }
            else if (positionInAxis >= Mathf.Max(startingPositionInAxis, endingPositionInAxis))
            {
                platformDirection = SetAxisValue(platformDirection, -1);
            }
            else if (rb.linearVelocity == Vector3.zero)
            {
                platformDirection *= -1;
            }

            rb.linearVelocity = platformDirection * platformSpeed;
        }
        else
        {
            if (playerOnPlatform)
            {
                if (positionInAxis >= Mathf.Max(startingPositionInAxis, endingPositionInAxis))
                {
                    platformDirection = SetAxisValue(platformDirection, 0);
                }
                else
                    platformDirection = SetAxisValue(platformDirection, 1);
            }
            else
            {
                if (positionInAxis <= Mathf.Min(startingPositionInAxis, endingPositionInAxis))
                {
                    platformDirection = SetAxisValue(platformDirection, 0);
                }
                else
                    platformDirection = SetAxisValue(platformDirection, -1);
            }

            rb.linearVelocity = platformDirection * platformSpeed;
        }
    }

    private void OnDrawGizmosSelected()
    {
        switch (MovementAxis)
        {
            case MovementAxis.rightLeft:
                platformDirection = Vector3.right;
                break;

            case MovementAxis.upDown:
                platformDirection = Vector3.up;
                break;

            case MovementAxis.forwardBackward:
                platformDirection = Vector3.forward;
                break;
        }

        Gizmos.color = gizmosColor;
        Gizmos.DrawLine(transform.position, transform.position + platformDirection * distanceToEnd);
        Gizmos.DrawCube(transform.position + platformDirection * distanceToEnd + GetComponent<BoxCollider>().center, GetComponent<BoxCollider>().size);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerOnPlatform = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerOnPlatform = false;
        }
    }

    private float GetAxisValue(Vector3 vector)
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

    private Vector3 SetAxisValue(Vector3 vector, float value)
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