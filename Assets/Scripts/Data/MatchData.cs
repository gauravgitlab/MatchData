using System.Collections.Generic;

public class MatchData
{
    public List<FrameData> Frames;

    public MatchData()
    {
        Frames = new List<FrameData>();
    }
}

public class FrameData
{
    public uint frameCount; 
    public List<TrackedObjectData> trackedObjectData;
    public BallData ballData;
}

public class TrackedObjectData
{
    public int team;
    public int trackingId;
    public int playerNumber;
    public float positionX;
    public float positionY;
    public float positionZ;
}

public class BallData
{
    public float positionX;
    public float positionY;
    public float positionZ;
    public float ballSpeed;
}
