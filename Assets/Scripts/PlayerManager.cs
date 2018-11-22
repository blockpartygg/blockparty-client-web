using UnityEngine;

public class PlayerManager : Singleton<PlayerManager> {
    public string Name;

    public void SetName(string name) {
        Name = name;
    }
}