using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace BSports
{
    public static class MatchDataReader
    {
        public static async Task<MatchData> FetchMatchData(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return null;

            string matchDataInString = await File.ReadAllTextAsync(filePath);
            if (string.IsNullOrEmpty(matchDataInString))
                return null;

            string[] frames = matchDataInString.Split('\n');
            frames = frames.Where(x => !string.IsNullOrEmpty(x)).ToArray();

            MatchData matchData = await Task.Run(() =>
            {
                MatchData matchData = new MatchData();
                for (int i = 0; i < frames.Length; i++)
                {
                    string[] frameEntry = frames[i].Split(':');
                    frameEntry = frameEntry.Where(x => !string.IsNullOrEmpty(x.Trim())).ToArray();

                    FrameData frameData = new FrameData()
                    {
                        frameCount = Convert.ToUInt32(frameEntry[0]),
                        playerData = GetTrackedObjectData(frameEntry[1]),
                        ballData = GetBallData(frameEntry[2])
                    };
                    matchData.Frames.Add(frameData);
                }
                return matchData;
            });

            return matchData;
        }

        private static List<TrackedObjectData> GetTrackedObjectData(string entry)
        {
            List<TrackedObjectData> trackedObjectDataList = new List<TrackedObjectData>();

            string[] teamData = entry.Split(";");
            teamData = teamData.Where(x => !string.IsNullOrEmpty(x.Trim())).ToArray();
            for (int i = 0; i < teamData.Length; i++)
            {
                string[] objectData = teamData[i].Split(',');
                TrackedObjectData trackedObjectData = new();

                if (int.TryParse(objectData[0], out int team)) trackedObjectData.team = team;
                if (int.TryParse(objectData[1], out int trackingId)) trackedObjectData.trackingId = trackingId;
                if (int.TryParse(objectData[2], out int playerNumber)) trackedObjectData.playerNumber = playerNumber;
                if (float.TryParse(objectData[3], out float posX)) trackedObjectData.positionX = posX;
                if (float.TryParse(objectData[4], out float posY)) trackedObjectData.positionY = posY;
                if (float.TryParse(objectData[5], out float posZ)) trackedObjectData.positionZ = posZ;

                trackedObjectDataList.Add(trackedObjectData);
            }

            return trackedObjectDataList;
        }

        private static BallData GetBallData(string entry)
        {
            string[] ballEntry = entry.Split(",");
            BallData ballData = new();

            if (float.TryParse(ballEntry[0], out float posX)) ballData.positionX = posX;
            if (float.TryParse(ballEntry[1], out float posY)) ballData.positionY = posY;
            if (float.TryParse(ballEntry[2], out float posZ)) ballData.positionZ = posZ;
            if (float.TryParse(ballEntry[3], out float speed)) ballData.ballSpeed = speed;

            return ballData;
        }
    }
}

