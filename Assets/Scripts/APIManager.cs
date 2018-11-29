using UnityEngine;

public class APIManager : Singleton<APIManager> {
    #if UNITY_EDITOR
    public string HostURL = "http://localhost:1337/";
    #else
    public string HostURL = "https://blockparty-server.herokuapp.com/";
    #endif

    public string ConfigRoute = "config";
    public string GameRoute = "game";
    public string ScoreRoute = "score";
    public string ScoreboardRoute = "scoreboard";
    public string LeaderboardRoute = "leaderboard";
}