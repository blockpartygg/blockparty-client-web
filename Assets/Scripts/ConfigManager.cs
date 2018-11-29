using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;

[Serializable] public class SerializableConfig {
    [Serializable] public class SerializableStates {
        [Serializable] public class SerializablePregame {
            public string id;
            public int duration;
        }

        [Serializable] public class SerializablePreRound {
            public string id;
            public int duration;
        }

        [Serializable] public class SerializablePreMinigame {
            public string id;
            public int duration;
        }

        [Serializable] public class SerializableInMinigame {
            public string id;
            public int duration;
        }

        [Serializable] public class SerializablePostMinigame {
            public string id;
            public int duration;
        }

        [Serializable] public class SerializableScoreboard {
            public string id;
            public int duration;
        }

        [Serializable] public class SerializableLeaderboard {
            public string id;
            public int duration;
        }

        [Serializable] public class SerializablePostgame {
            public string id;
            public int duration;
        }

        public SerializablePregame pregame;
        public SerializablePreRound preRound;
        public SerializablePreMinigame preMinigame;
        public SerializableInMinigame inMinigame;
        public SerializablePostMinigame postMinigame;
        public SerializableScoreboard scoreboard;
        public SerializableLeaderboard leaderboard;
        public SerializablePostgame postgame;
    }

    [Serializable] public class SerializableModes {
        public string timeAttack;
        public string survival;
    }

    public SerializableStates states;
    public SerializableModes modes;
    public int roundCount;
}
public class ConfigManager : Singleton<ConfigManager> {
    public int PregameDuration;
    public int PreRoundDuration;
    public int PreMinigameDuration;
    public int InMinigameDuration;
    public int PostMinigameDuration;
    public int ScoreboardDuration;
    public int LeaderboardDuration;
    public int PostgameDuration;
    public int RoundCount;

    void Start() {
        FetchConfigAsync();
    }

    public void FetchConfigAsync() {
        StartCoroutine(FetchConfig());
    }

    IEnumerator FetchConfig() {
        UnityWebRequest request = UnityWebRequest.Get(APIManager.Instance.HostURL + APIManager.Instance.ConfigRoute);
        yield return request.SendWebRequest();

        if(request.isNetworkError || request.isHttpError) {
            Debug.Log(request.error);
        }
        else {
            string text = request.downloadHandler.text;
            SerializableConfig config = JsonUtility.FromJson<SerializableConfig>(text);
            PregameDuration = config.states.pregame.duration;
            PreRoundDuration = config.states.preRound.duration;
            PreMinigameDuration = config.states.preMinigame.duration;
            InMinigameDuration = config.states.inMinigame.duration;
            PostMinigameDuration = config.states.postMinigame.duration;
            ScoreboardDuration = config.states.scoreboard.duration;
            LeaderboardDuration = config.states.leaderboard.duration;
            PostgameDuration = config.states.postgame.duration;
            RoundCount = config.roundCount;
        }
    }
}