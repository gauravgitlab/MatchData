using System;
using TMPro;
using UnityEngine;

public class MatchHUD : MonoBehaviour
{
    public TextMeshProUGUI TimerText;

    public static Action<float> OnUpdateTime;

    // Start is called before the first frame update
    void Start()
    {
        OnUpdateTime += UpdateTimer;
    }

    private void UpdateTimer(float timer)
    {
        TimerText.text = GetTimeInString(timer);
    }

    public string GetTimeInString(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time - minutes * 60f);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
