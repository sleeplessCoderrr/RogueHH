using UnityEngine;

public class StateText : MonoBehaviour
{
    public UnityEngine.UI.Text indicatorText;
    [SerializeField] private EnemyStateChangeEventChannel stateChangeEventChannel;

    private void OnEnable()
    {
        stateChangeEventChannel.OnStateChanged += UpdateIndicator;
    }

    private void OnDisable()
    {
        stateChangeEventChannel.OnStateChanged -= UpdateIndicator;
    }

    private void UpdateIndicator(EnemyState state)
    {
        Debug.Log("Request Sent State: " + state);
        switch (state)
        {
            case EnemyState.Alert:
                indicatorText.text = "??";
                indicatorText.color = Color.yellow;
                break;
            case EnemyState.Aggro:
                indicatorText.text = "!!!";
                indicatorText.color = Color.red;
                break;
            case EnemyState.Idle:
                indicatorText.text = "";
                break;
            case EnemyState.Attack:
                indicatorText.text = "";
                break; 
        }
        Canvas.ForceUpdateCanvases();
    }
}