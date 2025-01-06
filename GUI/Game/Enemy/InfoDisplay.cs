using UnityEngine;
using UnityEngine.UI;

public class InfoDisplay : MonoBehaviour
{
    public Text nameText; 
    public Slider hpSlider;  
    
    private void OnEnable()
    {
        nameText.text = ""; 
        hpSlider.value = 0;
    }

    public void SetName(string name)
    {
        nameText.text = name;
    }

    public void UpdateInfo(float currentHp, float maxHp)
    {
        if (hpSlider != null)
        {
            hpSlider.value = currentHp / maxHp; 
        }
    }
}