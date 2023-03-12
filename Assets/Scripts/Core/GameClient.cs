
public class GameClient : GameServiceBase
{
    private static readonly object sync = new object();

    private static GameClient _instance;

    public static GameClient Instance
    {
        get
        {
            {
                if (_instance == null)
                    lock (sync)
                    {
                        _instance = new GameClient();
                    }
            }

            return _instance;
        }
    }

    internal GameClient()
    {
        AddService<IFrameRate>(new FrameRate());
        AddService<IMatchTimer>(new MatchTimer());

        AddService<IMatchDataReader>(new MatchDataReader());
        AddService<ITrackedObjectsManager>(new TrackedObjectsManager());
        AddService<IBall>(new Ball());
        
        AddService<IMatchManager>(new MatchManager());
    }

    public static T Get<T>()
    {
        return Instance.GetService<T>();
    }
}
