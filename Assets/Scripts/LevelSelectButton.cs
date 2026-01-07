using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CheckOnLoad : MonoBehaviour
{
    [Header("References")]
    private GameManager gm;

    public int ElementAt;
    public UnityEvent unityEvent;
    public TMP_Text textTime;

    void Start()
    {
        StartCoroutine(LateStart());
    }

    private IEnumerator LateStart()
    {
        yield return new WaitForEndOfFrame();

        gm = FindFirstObjectByType<GameManager>();

        if (0 < ElementAt && ElementAt <= gm.levelStates.Length && gm.levelStates.ElementAt(ElementAt - 1).complete)
        {
            unityEvent.Invoke();
        }
    }

    public void UpdateTextTime()
    {
        float time = gm.levelStates.ElementAt(ElementAt).timeSeconds;

        int minutes = (int)(time - time % 60) / 60;
        int seconds = (int)(Math.Truncate(time) - minutes * 60);
        int milliseconds = (int)Math.Truncate((time - Math.Truncate(time)) * 1000);

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

        if (time != 0)
        {
            textTime.text = "Best time: " + minutesText + ":" + secondsText + ":" + millisecondsText;
        }
        else
        {
            textTime.text = "Best time: -";
        }
    }
}
