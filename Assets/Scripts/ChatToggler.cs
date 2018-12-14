using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ChatToggler : MonoBehaviour {
    public ChatManager ChatManager;
    Toggle toggle;
    public GameObject Panel;
    
    void Awake() {
        toggle = GetComponent<Toggle>();
    }

    void Start() {
        toggle.isOn = ChatManager.IsVisible;
        
        if(ChatManager.IsVisible) {
            Panel.transform.localPosition = Vector3.zero;
        }
        else {
            Panel.transform.localPosition = new Vector3(0, -350f, 0);
        }
    }

    public void Toggle() {
        ChatManager.IsVisible = toggle.isOn;

        if(toggle.isOn) {
            Panel.transform.DOLocalMoveY(0, 0.5f);
        }
        else {
            Panel.transform.DOLocalMoveY(-350f, 0.5f);
        }
    }
}