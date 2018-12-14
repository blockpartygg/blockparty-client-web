using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;

[Serializable] public class SerializableGameConfiguration {
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

public class GameConfigurationFetcher : MonoBehaviour {
    public GameConfiguration GameConfiguration;
    public APIConfiguration APIConfiguration;
    
    void Start() {
        StartCoroutine(FetchConfig());
    }

    IEnumerator FetchConfig() {
        UnityWebRequest request = UnityWebRequest.Get(APIConfiguration.HostURL + APIConfiguration.ConfigRoute);
        yield return request.SendWebRequest();

        if(request.isNetworkError || request.isHttpError) {
            Debug.Log(request.error);
        }
        else {
            string text = request.downloadHandler.text;
            SerializableGameConfiguration config = JsonUtility.FromJson<SerializableGameConfiguration>(text);
            GameConfiguration.PregameDuration = config.states.pregame.duration;
            GameConfiguration.PreRoundDuration = config.states.preRound.duration;
            GameConfiguration.PreMinigameDuration = config.states.preMinigame.duration;
            GameConfiguration.InMinigameDuration = config.states.inMinigame.duration;
            GameConfiguration.PostMinigameDuration = config.states.postMinigame.duration;
            GameConfiguration.ScoreboardDuration = config.states.scoreboard.duration;
            GameConfiguration.LeaderboardDuration = config.states.leaderboard.duration;
            GameConfiguration.PostgameDuration = config.states.postgame.duration;
            GameConfiguration.RoundCount = config.roundCount;
        }
    }
}