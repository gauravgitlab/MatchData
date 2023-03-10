using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEngine;

public class DataReader : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        string filePath = Path.Combine(Application.dataPath, "Data/Applicant-test.dat");
        FetchMatchData(filePath);
    }

    private void FetchMatchData(string filePath)
    {
        if (string.IsNullOrEmpty(filePath))
            return;

        string matchData;
        FileStream f = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        using (StreamReader sr = new StreamReader(f))
        {
            matchData = sr.ReadToEnd();
        }

        if (string.IsNullOrEmpty(matchData))
            return;

        string[] frames = matchData.Split('\n');
        frames = frames.Where(x => !string.IsNullOrEmpty(x)).ToArray();
        
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
        }

        Debug.Log($"all {frames.Length} entries done !!");
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
