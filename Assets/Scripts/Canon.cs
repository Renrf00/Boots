using System.Collections;
using FMODUnity;
using UnityEngine;

public class Canon : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Projectile[] posibleProjectiles;
    [SerializeField] private Transform canonMouth;
    [SerializeField] private StudioEventEmitter FMODShoot;

    [Header("Canon parameters")]
    [Min(1)][SerializeField] private int nBullets = 1;
    [Min(0)][SerializeField] private float startDelay;
    [Range(0.075f, 1)][SerializeField] private float BulletInterval;
    [SerializeField] private bool randomizeBurstInterval;
    [Range(0.075f, 10)][SerializeField] private float maxBurstInterval;

    [Header("Bullet parameters")]
    [Min(0.1f)][SerializeField] private float bulletLifetime;
    [Min(0)][SerializeField] private float bulletSpeed;
    [SerializeField] private bool bulletGravity = true;

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
                    yield return new WaitForSeconds(BulletInterval);
                }
            }
            nextBurstTime += randomizeBurstInterval ? Random.Range(0.075f, maxBurstInterval) : maxBurstInterval;
        }
    }

    private void ShootBullet()
    {
        Projectile projectileInstance;
        Vector3 direction = canonMouth.rotation * Vector3.up;

        projectileInstance = Instantiate(posibleProjectiles[Random.Range(0, posibleProjectiles.Length)], canonMouth.position, canonMouth.rotation);

        if (projectileInstance)
        {
            projectileInstance.gameObject.GetComponent<Rigidbody>().useGravity = bulletGravity;
            projectileInstance.gameObject.GetComponent<Rigidbody>().AddForce(direction * bulletSpeed, ForceMode.VelocityChange);
            projectileInstance.lifetime = bulletLifetime;
        }
        FMODShoot.Play();
    }
}
