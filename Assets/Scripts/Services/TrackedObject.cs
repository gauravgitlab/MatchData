using UnityEngine;

public class TrackedObject 
{
    public int Team;
    public int TrackingId;
    public int PlayerNumber;
    public float PositionX;
    public float PositionY;
    public float PositionZ;

    public GameObject gameObject;

    public void Init(GameObject obj)
    {
        gameObject = obj;
    }

    public void Set(int team, int trackingId, int playerNumber, float posX, float posY, float posZ)
    {
        Team = team;
        TrackingId = trackingId;
        PlayerNumber = playerNumber;
        PositionX = posX;
        PositionY = posY;
        PositionZ = posZ;

        gameObject.transform.position = new Vector3(PositionX, PositionY, PositionZ);
    }
}
