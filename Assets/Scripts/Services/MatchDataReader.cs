using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public interface IMatchDataReader
{
    MatchData MatchData { get; }
    void FetchMatchData(string filePath);
}

public class MatchDataReader : IMatchDataReader, IGameServices
{
    public MatchData MatchData { get; private set; }

    public void Init() 
    {
        string filePath = Path.Combine(Application.dataPath, "Data/Applicant-test.dat");
        FetchMatchData(filePath);
    }

    public void Update() { }
    public void FixedUpdate() { }
    public void Release() { }
    
    public void FetchMatchData(string filePath)
    {
        if (string.IsNullOrEmpty(filePath))
            return;

        string matchDataInString;
        FileStream f = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        using (StreamReader sr = new StreamReader(f))
        {
            matchDataInString = sr.ReadToEnd();
        }

        if (string.IsNullOrEmpty(matchDataInString))
            return;

        string[] frames = matchDataInString.Split('\n');
        frames = frames.Where(x => !string.IsNullOrEmpty(x)).ToArray();
        
        MatchData matchData = new MatchData();
        for(int i=0; i<frames.Length; i++)
        {
            string[] frameEntry = frames[i].Split(':');
            frameEntry = frameEntry.Where(x => !string.IsNullOrEmpty(x.Trim())).ToArray();

            FrameData frameData = new FrameData()
            {
                frameCount = Convert.ToUInt32(frameEntry[0]),
                trackedObjectData = GetTrackedObjectData(frameEntry[1]),
                ballData = GetBallData(frameEntry[2])
            };

            matchData.Frames.Add(frameData);
        }

        Debug.Log($"all {frames.Length} entries done !!");
        MatchData = matchData;
    }

    private List<TrackedObjectData> GetTrackedObjectData(string entry)
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

    private BallData GetBallData(string entry)
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
