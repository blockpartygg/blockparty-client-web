using UnityEngine;

public class BackgroundMusic : MonoBehaviour {
    public AudioSettings AudioSettings;
    public AudioSource Source;

    void Start() {
        Source.clip = AudioSettings.BackgroundMusic;
        Source.Play();  
    }
}