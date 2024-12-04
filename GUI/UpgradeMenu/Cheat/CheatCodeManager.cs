using UnityEngine;
using TMPro;

public class CheatCodeManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField cheatCodeInput;
    [SerializeField] private CheatCodeEventChannel cheatCodeEventChannel; 

    private void Start()
    {
        cheatCodeInput.text = string.Empty;
        cheatCodeInput.onEndEdit.AddListener(HandleCheatCodeInput);
    }

    private void OnDestroy()
    {
        cheatCodeInput.onEndEdit.RemoveListener(HandleCheatCodeInput);
    }

    private void HandleCheatCodeInput(string input)
    {
        if (!string.IsNullOrWhiteSpace(input))
        {
            cheatCodeEventChannel.RaiseEvent(input.ToLower());
        }

        cheatCodeInput.text = string.Empty;
    }
}