using UnityEngine;

public class InstantiateParticles : MonoBehaviour
{
    public GameObject particles;

    public void OnTriggerEnter()
    {
        Instantiate(particles, transform.position, transform.rotation);
    }
}