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

    public void SetName(string name, int idx)
    {
        nameText.text = name;
        switch (idx)
        {
            case 0:
                nameText.color = Color.white;
                break;
            case 1:
                nameText.color = Color.yellow;
                break;
            case 2:
                nameText.color = Color.red;
                break;
        }
    }

    public void UpdateInfo(float currentHp, float maxHp)
    {
        if (hpSlider != null)
        {
            hpSlider.value = currentHp / maxHp; 
        }
    }
}