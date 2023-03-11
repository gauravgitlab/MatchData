using System.Collections.Generic;

public interface IMatchManager
{
    void SetBallData(BallData ballData);
}

public class MatchManager : IMatchManager, IGameServices
{
    private IBall ball;
    private ITrackedObjectsManager trackedObjectManager;
    private IMatchDataReader matchDataReader;
    private IMatchTimer matchTimer;

    private int currentFrame = 0;

    public void Init()
    {
        ball = GameClient.Get<IBall>();
        trackedObjectManager = GameClient.Get<ITrackedObjectsManager>();
        matchDataReader = GameClient.Get<IMatchDataReader>();
        matchTimer= GameClient.Get<IMatchTimer>();
        currentFrame = 0;
    }

    public void Update() 
    {
        if (matchDataReader == null || matchDataReader.MatchData == null)
            return;

        if (currentFrame >= matchDataReader.MatchData.Frames.Count)
        {
            if(matchTimer.IsTimerRunning)
                matchTimer.StopTimer();

            return;
        }
            

        if(!matchTimer.IsTimerRunning)
            matchTimer.StartTimer(true);

        matchTimer.UpdateTimer((time) =>
        {
            MatchHUD.OnUpdateTime?.Invoke(time);
        });

        // tracked Objects
        trackedObjectManager.SetTrackedObjects(currentFrame);

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
