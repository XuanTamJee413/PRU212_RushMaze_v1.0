using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class SetttingMenu : MonoBehaviour
{
    public AudioMixer mainMixer;
    public void SetVolume(float volume)
    {
        mainMixer.SetFloat("volume", volume);
    }
   public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;

    }

    public void SetQuality( int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
