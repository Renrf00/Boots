using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    public GameObject player;
    public Transform respawnPoint;

    void Start()
    {
        Instantiate(player, respawnPoint);
        player = GameObject.Find("Player");

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