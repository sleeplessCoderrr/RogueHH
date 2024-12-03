using UnityEngine;

public class UpgradeMenuPlayer : MonoBehaviour
{
    private void Start()
    {
        PlayMenuBGM();
    }
    
    private static void PlayMenuBGM()
    {
        AudioManager.Instance.PlayUpgradeBgm();
    } 
}