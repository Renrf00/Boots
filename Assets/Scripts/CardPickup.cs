using System;
using UnityEngine;

public class CardPickup : MonoBehaviour
{
    [Header("References")]
    public DoorController doorController;
    

    void Start()
    {
        doorController = GameObject.FindGameObjectWithTag("Door").GetComponent<DoorController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Destroy(gameObject);
            doorController.SetDoorUnlocked(true);
        }
    }
}
