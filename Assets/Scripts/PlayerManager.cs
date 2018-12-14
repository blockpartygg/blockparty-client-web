using UnityEngine;

[CreateAssetMenu]
public class PlayerManager : ScriptableObject {
    public string Name;

    public void SetName(string name) {
        Name = name;
    }
}