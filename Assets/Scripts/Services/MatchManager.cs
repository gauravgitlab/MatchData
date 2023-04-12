
using System.IO;
using System.Collections.Generic;
using UnityEngine;

namespace BSports
{
    public class MatchManager : MonoBehaviour
    {
        public string fileName = string.Empty;
        public TrackedObjectsManager trackedObjectManager;

        private MatchData MatchData { get; set; }
        private int currentFrame = 0;
        private MatchTimer matchTimer;

        private int framePerSeconds = 25;
        private float fixedTimestep = 0f;

        private float timeElapsed = 0f;

        private void Start()
        {
            fixedTimestep = 1f / framePerSeconds;

            matchTimer = new MatchTimer();

            FetchMatchData();
            Debug.Log("All good");
        }

        private async void FetchMatchData()
        {
            string filePath = Path.Combine(Application.dataPath, "Data", fileName);
            MatchData = await MatchDataReader.FetchMatchData(filePath);
        }

        public void Update()
        {
            if (MatchData == null || MatchData.Frames == null)
                return;

            timeElapsed += Time.deltaTime;
            if(timeElapsed >= fixedTimestep) {
                Debug.Log($"{timeElapsed}, {fixedTimestep}");
                timeElapsed = 0f;
                DrawFrame();
            }
        }

        private void DrawFrame()
        {
            if (currentFrame >= MatchData.Frames.Count)
            {
                if (matchTimer.IsTimerRunning)
                    matchTimer.StopTimer();

                return;
            }

            if (!matchTimer.IsTimerRunning)
                matchTimer.StartTimer(true);

            matchTimer.UpdateTimer((time) =>
            {
                MatchHUD.OnUpdateTime?.Invoke(time);
            });

            // tracked Objects
            List<TrackedObjectData> playerData = MatchData.Frames[currentFrame].playerData;
            trackedObjectManager.SetPlayersOnFrame(playerData);

            // ball data
            BallData ballData = MatchData.Frames[currentFrame].ballData;
            trackedObjectManager.SetBallOnFrame(ballData);

            currentFrame++;
        }
    }
}
