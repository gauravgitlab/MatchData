using UnityEngine;

public class TrackedObject : MonoBehaviour 
{
    public int Team;
    public int TrackingId;
    public int PlayerNumber;
    public float PositionX;
    public float PositionY;
    public float PositionZ;

    public void Set(int team, int trackingId, int playerNumber, float posX, float posY, float posZ)
    {
        Team = team;
        TrackingId = trackingId;
        PlayerNumber = playerNumber;
        PositionX = posX;
        PositionY = posY;
        PositionZ = posZ;

        transform.position = new Vector3(PositionX, PositionY, PositionZ);
    }
}
