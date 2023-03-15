using UnityEngine;

namespace BSports
{
    public class Ball : TrackedObject
    {
        public float BallSpeed { get; private set; }

        public void SetSpeed(float speed)
        {
            BallSpeed = speed;
        }
    }
}