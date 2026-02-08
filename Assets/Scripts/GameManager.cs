using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManagerInstance;

    [Header("GameState")]
    public LevelState[] levelStates;
    public float subtractToTimer;
    public float currentTime;
    public bool stopTimer = true;
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
    void Update()
    {
        if (!stopTimer)
        {
            currentTime = Time.timeSinceLevelLoad - subtractToTimer;
        }
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
        stopTimer = false;
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

    public string SectondsToString(float timeSeconds)
    {
        int minutes = (int)(timeSeconds - timeSeconds % 60) / 60;
        int seconds = (int)(Math.Truncate(timeSeconds) - minutes * 60);
        int milliseconds = (int)Math.Truncate((timeSeconds - Math.Truncate(timeSeconds)) * 1000);

        string minutesText = minutes.ToString();
        string secondsText = seconds.ToString().Length == 1 ? "0" + seconds.ToString() : seconds.ToString();
        string millisecondsText;

        if (milliseconds.ToString().Length < 3)
        {
            millisecondsText = milliseconds.ToString().Length == 2 ? "0" + milliseconds.ToString() : "00" + milliseconds.ToString();
        }
        else
        {
            millisecondsText = milliseconds.ToString();
        }

        return minutesText + ":" + secondsText + ":" + millisecondsText;
    }
}

[Serializable]
public class LevelState
{
    public string name;
    public bool complete;
    public float timeSeconds;
}