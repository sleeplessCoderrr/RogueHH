using UnityEngine;
using UnityEngine.EventSystems;

public class MenuSFXPlayer : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.Instance.PlayUISfx(1);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.Instance.PlayUISfx(0);
    }
}