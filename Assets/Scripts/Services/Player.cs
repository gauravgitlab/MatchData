using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BSports
{
    public class Player : TrackedObject
    {
        public int Team;
        public int TrackingId;
        public int PlayerNumber;

        public void SetTeam(int team, int trackingId, int playerNumber)
        {
            PlayerNumber = playerNumber;
            Team = team;
            TrackingId = trackingId;
        }
    }
}

