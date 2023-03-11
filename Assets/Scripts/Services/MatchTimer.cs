using System;
using UnityEngine;

public interface IMatchTimer
{
    public void StartTimer(bool reset = false);
    public void StopTimer();
    public void ResetTime();
    public void UpdateTimer(Action<TimeSpan> updateTimeAction);
    public bool IsTimerRunning { get; }
}

public class MatchTimer : IMatchTimer, IGameServices
{
    private TimeSpan startGameTime;

    private bool startTimer = false;

    private float startTime = 0f;
    private float elapsedTime = 0f;

    public TimeSpan GetGameTime => startGameTime;

    public bool IsTimerRunning => startTimer;

    public void ResetTime()
    {
        startTime = 0f;
        elapsedTime = 0f;
        startTimer = false;
        startGameTime = TimeSpan.Zero;
    }

    public void UpdateTimer(Action<TimeSpan> updateTimeAction)
    {
        if (startTimer)
        {
            elapsedTime = Time.time - startTime;

            if (elapsedTime >= 1f)
            {
                elapsedTime = 0f;
                startTime = Time.time;
                startGameTime = startGameTime.Add(new TimeSpan(0, 0, 1));
                updateTimeAction?.Invoke(startGameTime);
            }
        }
    }

    public void StartTimer(bool reset = false)
    {
        if (reset)
            ResetTime();

        if (startTimer)
            return;

        startTimer = true;
        startTime = Time.time;
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
