using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public Rigidbody rb;

    [Header("Jump controls")]
    public float jumpSpeed = 5;
    public float jumpCooldown = 0.4f;
    public float currentJumpCooldown;

    // did this so the player can't jump by touching the side of a "Floor" object (collision) or jump before touching the ground (Raycast)
    public bool groundCollision = false;
    public bool groundRay = false;


    [Header("Movement controls")]
    public float speed = 5;


    void Update()
    {
        if (currentJumpCooldown > 0)
            currentJumpCooldown -= Time.deltaTime;

        transform.Translate(
            new Vector3(
                speed * Time.deltaTime * Input.GetAxis("Horizontal"),
                0,
                speed * Time.deltaTime * Input.GetAxis("Vertical")),
            Space.Self);

        if (groundCollision && groundRay && currentJumpCooldown <= 0 && Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            currentJumpCooldown = jumpCooldown;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 7)
            groundCollision = true;
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 7)
            groundCollision = false;
    }
}
