using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;

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
        
        MatchData matchData = new MatchData();
        var tasks = new List<Task<FrameData>>();
        for(int i=0; i<frames.Length; i++)
        {
            tasks.Add(FetchFrame(frames[i]));
        }

        FrameData[] result = await Task.WhenAll(tasks);
        matchData.Frames.AddRange(result);

        Debug.Log($"all {frames.Length} entries done !!");
        return matchData;
    }

    private static Task<FrameData> FetchFrame(string frame)
    {
        return Task.Run(() =>
        {
            string[] frameEntry = frame.Split(':');
            frameEntry = frameEntry.Where(x => !string.IsNullOrEmpty(x.Trim())).ToArray();

            FrameData frameData = new FrameData()
            {
                frameCount = Convert.ToUInt32(frameEntry[0]),
                trackedObjectData = GetTrackedObjectData(frameEntry[1]),
                ballData = GetBallData(frameEntry[2])
            };

            return frameData;
        });
    } 

    private static List<TrackedObjectData> GetTrackedObjectData(string entry)
    {
        List<TrackedObjectData> trackedObjectDataList = new List<TrackedObjectData>();

        string[] teamData = entry.Split(";");
        teamData = teamData.Where(x => !string.IsNullOrEmpty(x.Trim())).ToArray();
        for(int i=0; i<teamData.Length; i++)
        {
            string[] objectData = teamData[i].Split(',');  
            TrackedObjectData trackedObjectData = new TrackedObjectData()
            {
                team = Convert.ToInt32(objectData[0]),
                trackingId = Convert.ToInt32(objectData[1]),
                playerNumber = Convert.ToInt32(objectData[2]),
                positionX = float.Parse(objectData[3]),
                positionY = float.Parse(objectData[4]),
                positionZ = float.Parse(objectData[5])
            };

            trackedObjectDataList.Add(trackedObjectData);
        }

        return trackedObjectDataList;
    }

    private static BallData GetBallData(string entry)
    {
        string[] ballEntry = entry.Split(",");
        BallData ballData = new BallData()
        {
            positionX = float.Parse(ballEntry[0]),
            positionY = float.Parse(ballEntry[1]),
            positionZ = float.Parse(ballEntry[2]),
            ballSpeed = float.Parse(ballEntry[3])
        };

        return ballData;
    }
}
