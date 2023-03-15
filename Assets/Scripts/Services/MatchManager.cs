
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

        private void Start()
        {
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
