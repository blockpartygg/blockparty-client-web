using UnityEngine;
using TMPro;

public class InputFieldActivator : MonoBehaviour {
    void Start() {
            TMP_InputField inputField = GetComponent<TMP_InputField>();
            inputField.ActivateInputField();
    }
}