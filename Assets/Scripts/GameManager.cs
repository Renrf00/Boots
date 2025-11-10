using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("References")]
    public GameObject player;
    public Transform spawnpoint;

    [Header("Game")]
    private Stopwatch timer = new Stopwatch();
    private Dictionary<string, float> roomTimes;

    void Start()
    {
        StartRoom();
    }

    private void NextRoom()
    {
        
    }

    private void StartRoom()
    {
        spawnpoint = GameObject.FindGameObjectWithTag("Respawn").GetComponent<Transform>();

        timer.Reset();
        timer.Start();        
        
        player = Instantiate(player, spawnpoint);
    }

    private void lockPlayer(bool lockPlayer)
    {
        if (lockPlayer)
        {
            player.GetComponent<PlayerController>().enabled = false;
            player.GetComponent<DashModule>().enabled = false;
        }
        else
        {
            player.GetComponent<PlayerController>().enabled = true;
            player.GetComponent<DashModule>().enabled = true;        
        }
    }
}