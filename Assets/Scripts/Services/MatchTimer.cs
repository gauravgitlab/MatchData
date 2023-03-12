using System;
using UnityEngine;

public interface IMatchTimer
{
    public void StartTimer(bool reset = false);
    public void StopTimer();
    public void ResetTime();
    public void UpdateTimer(Action<float> updateTimeAction);
    public bool IsTimerRunning { get; }
}

public class MatchTimer : IMatchTimer, IGameServices
{
    private bool startTimer = false;

    private float timer = 0f;
    public bool IsTimerRunning => startTimer;

    public void ResetTime()
    {
        timer = 0f;
        startTimer = false;
    }

    public void UpdateTimer(Action<float> updateTimeAction)
    {
        if (startTimer)
        {
            timer += Time.deltaTime;
            updateTimeAction?.Invoke(timer);
        }
    }

    public void StartTimer(bool reset = false)
    {
        if (reset)
            ResetTime();

        if (startTimer)
            return;

        startTimer = true;
    }

    public void StopTimer()
    {
        startTimer = false;
    }

    public void Init() { }
    public void Update() { }
    public void FixedUpdate() { }
    public void Release() { }
}
