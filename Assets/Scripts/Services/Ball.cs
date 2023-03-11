
using UnityEngine;

public interface IBall
{
    float PositionX { get; }
    float PositionY { get; }
    float PositionZ { get; }
    float BallSpeed { get; }
    void SetPosition(float posX, float posY, float posZ);
    void SetSpeed(float speed);
    void ToString();
}

public class Ball : IBall, IGameServices 
{
    public float PositionX { get; private set; }
    public float PositionY { get; private set; }
    public float PositionZ { get; private set; }
    public float BallSpeed { get; private set; }

    private GameObject ballEntity;

    public void Init()
    {
        ballEntity = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        ballEntity.name = "Ball";
        ballEntity.transform.position = Vector3.zero;
    }

    public void ToString()
    {
        string str = $"the ball pos = {PositionX}, {PositionY}, {PositionZ}, speed = {BallSpeed}";
        Debug.Log(str);
    }

    public void SetPosition(float posX, float posY, float posZ)
    {
        PositionX = posX;
        PositionY = posY;
        PositionZ = posZ;

        ballEntity.transform.position = new Vector3(PositionX, PositionY, PositionZ);
    }

    public void SetSpeed(float speed)
    {
        BallSpeed = speed;
    }

    public void Update() { }
    public void FixedUpdate() { }
    public void Release() { }
}
