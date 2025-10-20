using System;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class GameManager : MonoBehaviour
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