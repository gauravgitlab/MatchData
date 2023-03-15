using System.Collections.Generic;
using UnityEngine;
using BSports;

namespace BSports
{
    public class TrackedObjectsManager : MonoBehaviour
    {
        public List<Player> TrackedObjects = new List<Player>();
        public Ball ball;

        public void SetPlayersOnFrame(List<TrackedObjectData> playersData)
        {
            foreach (var playerData in playersData)
            {
                Player player = null;
                var playerIndex = TrackedObjects.FindIndex(p => p.TrackingId == playerData.trackingId);
                if (playerIndex == -1)
                {
                    GameObject trackedGameObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                    player = trackedGameObject.AddComponent<Player>();
                    TrackedObjects.Add(player);
                }
                else
                {
                    player = TrackedObjects[playerIndex].GetComponent<Player>();
                }

                player.SetTeam(playerData.team, playerData.trackingId, playerData.playerNumber);
                player.SetPosition(playerData.positionX, playerData.positionY, playerData.positionZ);

            }
        }

        public void SetBallOnFrame(BallData ballData)
        {
            if (ball == null)
            {
                var ballGameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                ball = ballGameObject.AddComponent<Ball>();
            }

            ball.SetPosition(ballData.positionX, ballData.positionY, ballData.positionZ);
            ball.SetSpeed(ballData.ballSpeed);
        }
    }
}
