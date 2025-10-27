using NUnit.Framework.Internal.Commands;
using UnityEngine;

public class ControlSpeed : MonoBehaviour
{
    [Header("References")]
    public Rigidbody rb;

    [Header("Speed control")]
    public SpeedLimit speedLimit = SpeedLimit.Grounded;
    public float maxSpeed = 7;
    public bool limitYAxis = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void LateUpdate()
    {
        LimitSpeed(speedLimit, limitYAxis);
    }
    
    private void LimitSpeed(SpeedLimit speedLimit, bool limitYAxis)
    {
        Vector3 horizontalVelocity = new Vector3(
            rb.linearVelocity.x,
            0,
            rb.linearVelocity.z);

        Vector3 verticalVelocity = new Vector3(
            0,
            rb.linearVelocity.y,
            0);

        Vector3 newLinearVelocity;

        float multiplySpeed;

        switch (speedLimit)
        {

            case SpeedLimit.Airborn:
                multiplySpeed = maxSpeed + (horizontalVelocity.magnitude - maxSpeed) / 2;
                limitYAxis = false;
                break;

            case SpeedLimit.Dash:
                multiplySpeed = maxSpeed + (horizontalVelocity.magnitude - maxSpeed) / 2;
                limitYAxis = true;
                break;

            // default is Speedlimit.Grounded
            default:
                multiplySpeed = maxSpeed;
                limitYAxis = false;
                break;
        }

        //tend horizontal speed to less than max speed
        if (horizontalVelocity.magnitude > maxSpeed)
        {
            if (limitYAxis)
                newLinearVelocity = multiplySpeed * (horizontalVelocity + verticalVelocity).normalized;
            else
                newLinearVelocity = multiplySpeed * horizontalVelocity.normalized + verticalVelocity;   
            
            rb.linearVelocity = newLinearVelocity;
        }
    }
}

public enum SpeedLimit
{
    Grounded,
    Airborn,
    Dash,
}