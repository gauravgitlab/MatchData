using System.Collections.Generic;
using UnityEngine;

public interface ITrackedObjectsManager
{
    List<TrackedObject> TrackedObjects { get; }
    void SetTrackedObjects(int currentFrame);
}

public class TrackedObjectsManager : ITrackedObjectsManager, IGameServices
{
    private IMatchDataReader matchDataReader;

    public List<TrackedObject> TrackedObjects { get; private set; }

    public void Init()
    {
        matchDataReader = GameClient.Get<IMatchDataReader>();

        if (matchDataReader == null || matchDataReader.MatchData == null)
            return;

        TrackedObjects = new List<TrackedObject>();
        List<TrackedObjectData> trackedObjects = matchDataReader.MatchData.Frames[0].trackedObjectData;
        for (int i=0; i < trackedObjects.Count; i++)
        {
            GameObject trackedGameObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            TrackedObject trackedObject = new TrackedObject();
            trackedObject.Init(trackedGameObject);
            TrackedObjects.Add(trackedObject);
        }
    }

    public void SetTrackedObjects(int currentFrame)
    {
        List<TrackedObjectData> trackedObjectsData = matchDataReader.MatchData.Frames[currentFrame].trackedObjectData;
        for(int i=0; i<TrackedObjects.Count; i++)
        {
            TrackedObjects[i].Set(trackedObjectsData[i].team,
                                    trackedObjectsData[i].trackingId,
                                    trackedObjectsData[i].playerNumber,
                                    trackedObjectsData[i].positionX,
                                    trackedObjectsData[i].positionY,
                                    trackedObjectsData[i].positionZ);
        }
    }

    public void Update() { }
    public void FixedUpdate() { }
    public void Release() { }
}
