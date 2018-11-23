using UnityEngine;

public class AppBootstrapper : MonoBehaviour {
    void Start() {
        GameManager.Instance.FetchGameAsync();
        ConfigManager.Instance.FetchConfigAsync();
        Clock.Instance.SyncToServerClock();
    }
}