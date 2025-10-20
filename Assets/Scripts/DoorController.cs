using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject player;
    public Material door;

    public float speed = 1;

    void Update()
    {
        if ((player.transform.position - transform.parent.position).magnitude < 8)
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
    public void ChangeEmissionColor(Color color)
    {
        door.SetColor("_EmissionColor", color);
    }
}
