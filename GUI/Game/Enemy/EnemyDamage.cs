using UnityEngine;
using UnityEngine.UI;

public class EnemyDamage : MonoBehaviour
{
    public static EnemyDamage Instance;

    [Header("Damage Text Settings")]
    public Text damageText;
    public Color criticalDamageColor = new Color(1f, 0.5f, 0f);
    public int criticalFontSize = 40;
    public int normalFontSize = 30;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            damageText.gameObject.SetActive(false);
        }
        else
        {
            Destroy(this);
        }
    }

    public void TakeDamage(int damage, bool isCritical)
    {
        ShowDamageText(damage, isCritical);
    }

    private void ShowDamageText(int damage, bool isCritical)
    {
        damageText.gameObject.SetActive(true);
        damageText.text = damage.ToString();
        
        if (isCritical)
        {
            damageText.color = criticalDamageColor;
            damageText.fontSize = criticalFontSize;
        }
        else
        {
            damageText.color = Color.white;
            damageText.fontSize = normalFontSize;
        }

        Invoke("HideDamageText", 1f); 
    }

    private void HideDamageText()
    {
        damageText.gameObject.SetActive(false);
    }
}