
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

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
        private int fixedTimestepInMilliSeconds = 0;

        private float timeElapsed = 0f;

        private async void Start()
        {
            fixedTimestep = 1f / framePerSeconds;
            fixedTimestepInMilliSeconds = (int)(fixedTimestep * 1000);

            matchTimer = new MatchTimer();
            string filePath = Path.Combine(Application.dataPath, "Data", fileName);

            // Fetch data 
            matchTimer.StartTimer(true);
            var data = await MatchDataReader.FetchMatchData(filePath);
            matchTimer.StopTimer();
            Debug.Log(matchTimer.Timer + ", " + MatchHUD.GetTimeInString(matchTimer.Timer));

            matchTimer.ResetTime();

            // fetch data In Bytes
            matchTimer.StartTimer(true);
            MatchData = await MatchDataReader.FetchMatchDataInBytes(filePath);
            matchTimer.StopTimer();
            Debug.Log(matchTimer.Timer + ", " + MatchHUD.GetTimeInString(matchTimer.Timer));

            // Show data
            currentFrame = 0;
            PerformMatchData();
        }

        private async Task<MatchData> FetchMatchData()
        {
            string filePath = Path.Combine(Application.dataPath, "Data", fileName);
            return await MatchDataReader.FetchMatchData(filePath);
        }

        private async void PerformMatchData()
        {
            while (currentFrame < MatchData.Frames.Count)
            {
                // tracked Objects
                List<TrackedObjectData> playerData = MatchData.Frames[currentFrame].playerData;
                trackedObjectManager.SetPlayersOnFrame(playerData);

                // ball data
                BallData ballData = MatchData.Frames[currentFrame].ballData;
                trackedObjectManager.SetBallOnFrame(ballData);

                currentFrame++;

                await Task.Delay(fixedTimestepInMilliSeconds);
            }
        }

        public void Update()
        {
            if(matchTimer != null && matchTimer.IsTimerRunning)
            {
                matchTimer.UpdateTimer(null);
            }

            //if (MatchData == null || MatchData.Frames == null)
            //    return;

            //timeElapsed += Time.deltaTime;
            //if(timeElapsed >= fixedTimestep) {
            //    Debug.Log($"{timeElapsed}, {fixedTimestep}");
            //    timeElapsed = 0f;
            //    DrawFrame();
            //}
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
