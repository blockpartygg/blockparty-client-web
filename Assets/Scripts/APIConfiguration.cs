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
    public string ScoreRoute = "score";
    public string ScoreboardRoute = "scoreboard";
    public string LeaderboardRoute = "leaderboard";
    public string PersistentLeaderboardsRoute = "persistentleaderboards";
    public string SocketIORoute = "socket.io/";
}