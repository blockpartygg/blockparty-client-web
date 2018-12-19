using UnityEngine;

[CreateAssetMenu]
public class APIConfiguration : ScriptableObject {
    public string LocalHostURL = "http://localhost:1337/";
    public string RemoteHostURL = "https://blockparty-server.herokuapp.com/";
    public string HostURL {
        get {
            #if UNITY_EDITOR
            return LocalHostURL;
            #else
            return RemoteHostURL;
            #endif
        }
    }

    public string ConfigRoute = "config";
    public string GameRoute = "game";
    public string ResultsRoute = "results";
    public string ScoreboardRoute = "scoreboard";
    public string LeaderboardsRoute = "leaderboards";
    public string SocketIORoute = "socket.io/";
}