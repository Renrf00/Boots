using UnityEngine;

public class Projectile : MonoBehaviour
{
    public PlayerController player;
    public string checkTag;
    [Range(1, 30)]public float lifetime = 10;
    public int minY = -10;
    private float currentLifetime;

    void Start()
    {
        player = FindFirstObjectByType<PlayerController>().GetComponent<PlayerController>();
        currentLifetime = lifetime;
    }
    void Update()
    {
        if (transform.position.y < minY || currentLifetime <= 0)
        {
            Destroy(gameObject);
        }

        currentLifetime -= Time.deltaTime;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == checkTag)
        {
            if (checkTag == "Player")
            {
                player.Respawn();
            } 
            else
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
