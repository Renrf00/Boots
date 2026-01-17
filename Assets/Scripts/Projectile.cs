using UnityEngine;

public class Projectile : MonoBehaviour
{
    public string TagToCollide;
    [Min(1)] public float lifetime = 10;
    public int minY = -10;
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
        if (collision.gameObject.tag == TagToCollide)
        {
            if (TagToCollide == "Player")
            {
                collision.gameObject.GetComponent<PlayerController>().Respawn();
            }
            else
            {
                Destroy(collision.gameObject);
            }
        }
    }

    public void DestroyExistingBullets()
    {
        Projectile[] bullets = FindObjectsByType<Projectile>(0);
        foreach (Projectile bullet in bullets)
        {
            Destroy(bullet.gameObject);
        }
    }
}
