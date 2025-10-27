using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject player;
    public Material door;

    public bool doorUnlocked = false;
    public float speed = 1;

    void Update()
    {
        if ((player.transform.position - transform.parent.position).magnitude < 8 && doorUnlocked)
        {
            OpenDoor();
        } 
        else
        {
            CloseDoor();
        }
    }
    public void CloseDoor()
    {
        if (transform.position.y > transform.parent.position.y)
        {
            transform.Translate(Vector3.down * speed, transform.parent);
        }
    }
    public void OpenDoor()
    {
        if (transform.position.y < transform.parent.position.y + 3.99f)
        {
            transform.Translate(Vector3.up * speed, transform.parent);
        }
    }

    public void SetDoorUnlocked(bool isUnlocked)
    {
        doorUnlocked = isUnlocked;
        
        if (doorUnlocked)
        {
            SetEmissionColor(Color.green);
        } else
        {
            SetEmissionColor(Color.red);
        }
    }

    public void SetEmissionColor(Color color)
    {
        door.SetColor("_EmissionColor", color);
    }
}
