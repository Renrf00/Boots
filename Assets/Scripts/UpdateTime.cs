using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UpdateTime : MonoBehaviour
{
    [Header("References")]
    private GameManager gm;

    [SerializeField] private int RoomAtIndex;
    [SerializeField] private UnityEvent checkToEnableEvents;
    [SerializeField] private string text;
    [SerializeField] private TMP_Text textTime;

    public void UpdateTextBestTime()
    {
        gm = GameManager.gameManagerInstance;

        float time = gm.levelStates.ElementAt(RoomAtIndex).timeSeconds;

        textTime.text = time == 0 ? text + " -" : text + " " + gm.SectondsToString(time);
    }

    public void CheckToEnable()
    {
        gm = GameManager.gameManagerInstance;

        if (0 < RoomAtIndex && RoomAtIndex <= gm.levelStates.Length && gm.levelStates.ElementAt(RoomAtIndex - 1).complete)
        {
            checkToEnableEvents.Invoke();
        }
    }
    public void UpdateCurrentTime()
    {
        gm = GameManager.gameManagerInstance;

        textTime.text = gm.currentTime == 0 ? text + " -" : text + " " + gm.SectondsToString(gm.currentTime);
    }
}
