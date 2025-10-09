using System.Globalization;
using System.Threading;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Animations;

public enum MovementAxis
{
    rightLeft,
    upDown,
    forwardBackward
}

public class MovingObject : MonoBehaviour
{
    public MovementAxis MovementAxis;
    private Vector3 direction;
    private Vector3 startingPosition;
    public float distanceToEnd;
    public float speed = 1f;

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
        transform.position = startingPosition + direction * distanceToEnd * (0.5f + Mathf.Sin(Time.time * speed * Mathf.PI) / 2);
    }
}