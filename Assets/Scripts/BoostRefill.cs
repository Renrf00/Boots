using System.Collections;
using UnityEngine;

public class BoostRefill : MonoBehaviour
{
    [SerializeField] private float BoostRefillCooldown = 0.5f;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(RefillCooldown(other.gameObject.GetComponent<DashModule>()));
        }
    }

    private IEnumerator RefillCooldown(DashModule dashModule)
    {
        dashModule.currentDashCharge = dashModule.maxDashCharge;
        gameObject.GetComponent<Collider>().enabled = false;
        MeshRenderer[] GemRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer GemRenderer in GemRenderers)
        {
            GemRenderer.enabled = false;
        }

        yield return new WaitForSeconds(BoostRefillCooldown);

        gameObject.GetComponent<Collider>().enabled = true;
        foreach (MeshRenderer GemRenderer in GemRenderers)
        {
            GemRenderer.enabled = true;
        }
    }
}
