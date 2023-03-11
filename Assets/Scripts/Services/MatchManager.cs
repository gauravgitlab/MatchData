using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public interface IMatchManager
{
    void SetBallData(BallData ballData);
}

public class MatchManager : IMatchManager, IGameServices
{
    private IBall ball;
    private IMatchDataReader matchDataReader;
    private int currentFrame = 0;

    public void Init()
    {
        ball = GameClient.Get<IBall>();
        matchDataReader = GameClient.Get<IMatchDataReader>();
        currentFrame = 0;
    }

    public void Update() 
    {
        if (matchDataReader == null || matchDataReader.MatchData == null)
            return;

        if (currentFrame > matchDataReader.MatchData.Frames.Count)
            return;


        // ball data
        BallData ballData = matchDataReader.MatchData.Frames[currentFrame].ballData;
        SetBallData(ballData);

        currentFrame++;
    }

    public void SetBallData(BallData ballData)
    {
        ball.SetPosition(ballData.positionX, ballData.positionY, ballData.positionZ);
        ball.SetSpeed(ballData.ballSpeed);
        ball.ToString();
    }

    public void FixedUpdate() { }
    public void Release() { }
}
