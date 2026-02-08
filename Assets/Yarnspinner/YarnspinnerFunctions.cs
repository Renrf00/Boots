using System.Linq;
using UnityEngine;
using Yarn.Unity;

public class YarnspinnerFunctions
{
    [YarnFunction]
    public static bool CompletedRoom()
    {
        GameManager gm = GameManager.gameManagerInstance;

        return gm.levelStates.ElementAt(gm.GetRoomNumber()).complete;
    }

    [YarnCommand]
    public static void DisablePlayerControl(bool disable)
    {
        PlayerController player = GameObject.FindFirstObjectByType<PlayerController>();

        if (player == null)
        {
            Debug.LogError("No player found");
            return;
        }
        if (disable)
        {
            player.disableInput = true;
            player.DisableCameraMove(true);
        }
        else
        {
            player.disableInput = false;
            player.DisableCameraMove(false);
        }
    }

    [YarnCommand]
    public static void SetTimerDelay()
    {
        GameManager gm = GameManager.gameManagerInstance;

        gm.subtractToTimer = Time.timeSinceLevelLoad;
        gm.stopTimer = false;
    }
    [YarnCommand]
    public static void StopTimer()
    {
        GameManager gm = GameManager.gameManagerInstance;
        gm.stopTimer = true;
    }
}
