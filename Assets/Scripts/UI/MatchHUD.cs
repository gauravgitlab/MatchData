using System;
using TMPro;
using UnityEngine;

public class MatchHUD : MonoBehaviour
{
    public TextMeshProUGUI TimerText;

    public static Action<TimeSpan> OnUpdateTime;

    // Start is called before the first frame update
    void Start()
    {
        OnUpdateTime += UpdateTimer;
    }

    private void UpdateTimer(TimeSpan ts)
    {
        TimerText.text = GetTimeInString(ts);
    }

    public string GetTimeInString(TimeSpan time)
    {
        return string.Format("{0:00}:{1:00}:{2:00}", 
                            time.Hours, time.Minutes, time.Seconds);
    }
}
