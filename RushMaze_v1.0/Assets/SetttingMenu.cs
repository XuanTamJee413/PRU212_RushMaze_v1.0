using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetttingMenu : MonoBehaviour
{
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
