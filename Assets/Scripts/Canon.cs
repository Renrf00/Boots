using System.Collections;
using FMODUnity;
using UnityEngine;

public class Canon : MonoBehaviour
{
    [Header("References")]
    public Projectile projectile;
    public Transform canonMouth;
    public StudioEventEmitter FMODShoot;

    [Header("Canon parameters")]
    [Min(1)] public int nBullets = 1;
    [Min(0)] public float startDelay;
    [Range(0, 1)] public float bulletInterval;
    [Range(0, 10)] public float burstInterval;

    [Header("Bullet parameters")]
    [Min(0.1f)] public float bulletLifetime;
    [Min(0)] public float bulletSpeed;
    public bool bulletGravity = true;

    void OnEnable()
    {
        StartCoroutine(BurstFire(nBullets));
    }

    private IEnumerator BurstFire(int nBullets)
    {
        float nextBurstTime = Time.time + startDelay;

        while (true)
        {
            yield return new WaitUntil(() => Time.time >= nextBurstTime);

            for (int i = 0; i < nBullets; i++)
            {
                ShootBullet();

                if (i != nBullets - 1)
                {
                    yield return new WaitForSeconds(bulletInterval);
                }
            }
            nextBurstTime += burstInterval;
        }
    }

    private void ShootBullet()
    {
        Projectile projectileInstance;
        Vector3 direction = canonMouth.rotation * Vector3.up;

        projectileInstance = Instantiate(projectile, canonMouth.position, canonMouth.rotation);

        if (projectileInstance)
        {
            projectileInstance.gameObject.GetComponent<Rigidbody>().useGravity = bulletGravity;
            projectileInstance.gameObject.GetComponent<Rigidbody>().AddForce(direction * bulletSpeed, ForceMode.VelocityChange);
            projectileInstance.lifetime = bulletLifetime;
        }
        FMODShoot.Play();
    }
}
