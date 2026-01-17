using UnityEngine;

public class DoorController : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private Material door;

    private bool doorUnlocked = false;
    [SerializeField] private float doorSpeed = 1;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        SetDoorUnlocked(false);
    }
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
            transform.Translate(Vector3.down * doorSpeed, transform.parent);
        }
    }
    public void OpenDoor()
    {
        if (transform.position.y < transform.parent.position.y + 3.99f)
        {
            transform.Translate(Vector3.up * doorSpeed, transform.parent);
        }
    }

    public void SetDoorUnlocked(bool isUnlocked)
    {
        doorUnlocked = isUnlocked;

        if (doorUnlocked)
        {
            SetEmissionColor(Color.green);
        }
        else
        {
            SetEmissionColor(Color.red);
        }
    }

    public void SetEmissionColor(Color color)
    {
        door.SetColor("_EmissionColor", color);
    }
}
