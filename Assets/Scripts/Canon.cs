using UnityEngine;

public class Canon : MonoBehaviour
{
[Header("References")]
public Rigidbody projectile;

[Header("Canon parameters")]
[Min(1)] public int nBullets;
[Min(0)] public float speed;
[Min(1)] public float mass;
[Range(0.1f, 1)] public float bulletInterval;
[Min(1)] public float burstInterval;
private bool shootingBurst;
private float currentBulletInterval;
private float currentBurstInterval;

    void FixedUpdate()
    {
        if (nBullets > 1)
        {
            BurstFire(nBullets);
        } 
        else
        {
            SingleFire();
        }
    }

    private void SingleFire()
    {
        if (currentBurstInterval <= 0)
        {
            Rigidbody projectileInstance;

            currentBurstInterval = burstInterval;
            
            projectileInstance  = Instantiate(projectile, transform);

            projectileInstance.mass = mass;
            projectileInstance.AddForce(Vector3.up * speed, ForceMode.VelocityChange);
        }

        currentBurstInterval -= Time.deltaTime;
    }

    private void BurstFire(int nBullets)
    {
        if (currentBurstInterval <= 0){
            shootingBurst = true;
            currentBurstInterval = burstInterval;

            for (int i = 0; i < nBullets; i++)
            {
                if (currentBulletInterval <= 0)
                {
                    Rigidbody projectileInstance;

                    currentBulletInterval = bulletInterval;
                    
                    projectileInstance  = Instantiate(projectile, transform);

                    projectileInstance.mass = mass;
                    projectileInstance.AddForce(Vector3.up * speed, ForceMode.VelocityChange); // find a way to shoot in the same direction as the canon
                }
                currentBulletInterval -= Time.deltaTime;
            }
            shootingBurst = false;
        }
        if (!shootingBurst)
        {
            currentBurstInterval -= Time.deltaTime;
        }
    }
}
