using UnityEngine;
using UnityEngine.SceneManagement;

public class AcceptSetting : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Apply()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
