using System;
using UnityEngine;

namespace BSports
{
    public class MatchTimer
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
    }
}
