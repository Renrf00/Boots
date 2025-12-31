using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager gameManagerInstance;

    [Header("References")]
    public GameObject player;
    public Transform spawnpoint;

    private void Awake()
    {
        if (gameManagerInstance != null && gameManagerInstance != this)
        {
            Destroy(gameObject);
            return;
        }

        gameManagerInstance = this;
        DontDestroyOnLoad(gameObject);
    }

}