using UnityEngine;

public class PlayerRaycasts : MonoBehaviour
{
    public PlayerController player;
    
    void Update()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo, 1.1f))
        {
            player.groundRay = true;
            if (hitInfo.collider.tag == "Moving")
            {
                transform.parent = hitInfo.transform;
            }
        }
        else
        {
            player.groundRay = false;
            transform.parent = null;
        }
    }
}
