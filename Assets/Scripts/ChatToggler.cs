using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ChatToggler : MonoBehaviour {
    Toggle toggle;
    public GameObject Panel;
    
    void Awake() {
        toggle = GetComponent<Toggle>();
    }

    void Start() {
        toggle.isOn = ChatManager.Instance.IsVisible;
        
        if(ChatManager.Instance.IsVisible) {
            Panel.transform.localPosition = Vector3.zero;
        }
        else {
            Panel.transform.localPosition = new Vector3(0, -350f, 0);
        }
    }

    public void Toggle() {
        ChatManager.Instance.IsVisible = toggle.isOn;

        if(toggle.isOn) {
            Panel.transform.DOLocalMoveY(0, 1f);
        }
        else {
            Panel.transform.DOLocalMoveY(-350f, 1f);
        }
    }
}