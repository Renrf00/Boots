using UnityEngine;

public class GameManager : ScriptableObject
{
    [Header("References")]
    public GameObject player;
    public Transform respawnPoint;
    public DoorController doorController;

    [Header("Game")]
    public int lives;
    public string roomName;

    void Start()
    {
        // Instantiate(player, respawnPoint);
        // player = GameObject.Find("Player");
    }

    public void StartRoom()
    {

    }

    public void Rspawn()
    {

    }
    public void lockPlayer()
    {

    }
}