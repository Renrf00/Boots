using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager gameManagerInstance;


    [Header("GameState")]
    public LevelState[] levelStates;
    public float subtractToTimer;
    private int roomNumber;

    private void Awake()
    {
        if (gameManagerInstance != null && gameManagerInstance != this)
        {
            Destroy(gameObject);
            return;
        }

        gameManagerInstance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;

        roomNumber = -1;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "Room0":
                roomNumber = 0;
                break;
            case "Room1":
                roomNumber = 1;
                break;
            case "Room2":
                roomNumber = 2;
                break;
            case "Room3":
                roomNumber = 3;
                break;
            default:
                roomNumber = -1;
                break;
        }
    }
    public int GetRoomNumber()
    {
        return roomNumber;
    }

    public static void LockCursor(bool lockCursor)
    {
        if (lockCursor)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    public static GameManager GetGameManager()
    {
        return gameManagerInstance;
    }
}

[Serializable]
public class LevelState
{
    public string name;
    public bool complete;
    public float timeSeconds;
}