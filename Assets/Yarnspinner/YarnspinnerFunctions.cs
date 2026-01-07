using System.Linq;
using UnityEngine;
using Yarn.Unity;

public class YarnspinnerFunctions
{
    [YarnFunction("CompletedRoom")]
    public static bool CompletedRoom()
    {
        GameManager gm = GameObject.FindFirstObjectByType<GameManager>();

        return gm.levelStates.ElementAt(gm.GetRoomNumber()).complete;
    }

    [YarnCommand("DisablePlayerControl")]
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

    [YarnCommand("SetTimerDelay")]
    public static void SetTimerDelay()
    {
        GameManager gm = GameObject.FindFirstObjectByType<GameManager>();

        gm.subtractToTimer = Time.timeSinceLevelLoad;
    }
}
