using UnityEngine;

namespace BSports
{
    public class TrackedObject : MonoBehaviour
    {
        public float PositionX;
        public float PositionY;
        public float PositionZ;

        public void SetPosition(float posX, float posY, float posZ)
        {
            PositionX = posX;
            PositionY = posY;
            PositionZ = posZ;

            transform.position = new Vector3(PositionX / 100f, PositionY / 100f, PositionZ/100f);
        }
    }
}
