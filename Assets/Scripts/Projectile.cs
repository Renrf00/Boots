using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private string checkTag;
    [Range(1, 30)] public float lifetime = 10;
    [SerializeField] private int minY = -10;
    private float currentLifetime;

    void Start()
    {
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
                collision.gameObject.GetComponent<PlayerController>().Respawn();
            }
            else
            {
                Destroy(collision.gameObject);
            }
        }
    }

    public void DestroyAllBullets()
    {
        Projectile[] bullets = FindObjectsByType<Projectile>(0);
        foreach (Projectile bullet in bullets)
        {
            Destroy(bullet.gameObject);
        }
    }
}
