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

        [Serializable] public class SerializableInGame {
            public string id;
            public int duration;
        }

        [Serializable] public class SerializablePostgame {
            public string id;
            public int duration;
        }

        [Serializable] public class SerializableScoreboard {
            public string id;
            public int duration;
        }

        public SerializablePregame pregame;
        public SerializableInGame inGame;
        public SerializablePostgame postgame;
        public SerializableScoreboard scoreboard;
    }

    [Serializable] public class SerializableModes {
        public string timeAttack;
        public string survival;
    }

    public SerializableStates states;
    public SerializableModes modes;
}
public class ConfigManager : Singleton<ConfigManager> {
    public int PregameDuration;
    public int InGameDuration;
    public int PostgameDuration;
    public int ScoreboardDuration;

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
            InGameDuration = config.states.inGame.duration;
            PostgameDuration = config.states.postgame.duration;
            ScoreboardDuration = config.states.scoreboard.duration;
        }
    }
}