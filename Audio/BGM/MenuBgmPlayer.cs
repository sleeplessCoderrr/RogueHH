using UnityEngine;

public class MenuBgmPlayer : MonoBehaviour
{
    private void Start()
    {
        PlayMenuBGM();
    }
    
    private static void PlayMenuBGM()
    {
        AudioManager.Instance.PlayMenuBgm();
    }
}