using System;
using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private string checkTag;
    [SerializeField] private ProjectileType projectileType = ProjectileType.Kill;
    [SerializeField][Min(0.1f)] private float force;
    [SerializeField][Min(0.1f)] private float knockDuration = 0.1f;
    [Min(1)] public float lifetime = 10;
    [SerializeField] private int minY = -10;
    private float currentLifetime = 0;

    void Update()
    {
        if (transform.position.y < minY || currentLifetime >= lifetime)
        {
            Destroy(gameObject);
        }

        currentLifetime += Time.deltaTime;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == checkTag)
        {
            if (checkTag == "Player")
            {
                switch (projectileType)
                {
                    case ProjectileType.Push:
                        collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * force, ForceMode.VelocityChange);
                        StartCoroutine(Knock(knockDuration, collision.gameObject.GetComponent<PlayerController>()));
                        break;
                    case ProjectileType.Bouncy:
                        GetComponent<Rigidbody>().AddForce((transform.position - collision.gameObject.transform.position).normalized * force, ForceMode.VelocityChange);
                        GetComponent<Rigidbody>().useGravity = true;
                        GetComponent<Rigidbody>().excludeLayers = LayerMask.NameToLayer("Default");
                        GetComponent<Collider>().excludeLayers = LayerMask.NameToLayer("Default");
                        lifetime = 20;
                        StartCoroutine(Knock(knockDuration, collision.gameObject.GetComponent<PlayerController>()));
                        break;
                    case ProjectileType.Kill:
                        collision.gameObject.GetComponent<PlayerController>().Respawn();
                        break;
                }
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

    private IEnumerator Knock(float duration, PlayerController player)
    {
        player.disableInput = true;
        lifetime += duration + 1;

        yield return new WaitForSeconds(duration);

        player.disableInput = false;
    }

    public enum ProjectileType
    {
        Push,
        Bouncy,
        Kill
    }
}