using Assets.Scripts.Model;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoninSifuController : MonoBehaviour
{
    public GameObject RedSifuPanel; // Panel hội thoại
    public Text dialogueText;        // Text hiển thị lời thoại
    public Button lvl1, lvl2, lvl3, lvl4, lvl5, exitButton; 

    private int currentStep = 0;
    private bool isDialogueActive = false;

    void Start()
    {
        RedSifuPanel.SetActive(false);

        // Gán sự kiện cho các nút
        lvl1.onClick.AddListener(LoadLevel1);
        lvl2.onClick.AddListener(LoadLevel2);
        lvl3.onClick.AddListener(LoadLevel3);
        lvl4.onClick.AddListener(LoadLevel4);
        lvl5.onClick.AddListener(LoadLevel5);
        exitButton.onClick.AddListener(CloseDialogue);
    }
    public void LoadLevel1()
    {
        LevelData.SetLevelData(5, 5, 1, 5);
        Debug.Log($"LoadLevel1: {LevelData.MazeWidth}, {LevelData.MazeHeight}");
        SceneManager.LoadScene("MazeScene");
    }

    public void LoadLevel2()
    {
        LevelData.SetLevelData(7, 7, 3, 10); 
        SceneManager.LoadScene("MazeScene");
    }

    public void LoadLevel3()
    {
        LevelData.SetLevelData(9, 9, 7, 15);
        SceneManager.LoadScene("MazeScene");
    }

    public void LoadLevel4()
    {
        LevelData.SetLevelData(11, 11, 9, 20);
        SceneManager.LoadScene("MazeScene");
    }

    public void LoadLevel5()
    {
        LevelData.SetLevelData(13, 13, 11, 25);
        SceneManager.LoadScene("MazeScene");
    }

    public void LoadLevelByName(string levelName)
    {
        // Load Scene theo tên
        SceneManager.LoadScene(levelName);
    }

    void OnMouseDown() // Khi nhấp chuột vào NPC
    {
        Debug.Log("Bạn đã nhấn vào sifu!");
        if (!isDialogueActive) OpenDialogue();
    }

    void OpenDialogue()
    {
        RedSifuPanel.SetActive(true); 
        isDialogueActive = true;
    }

    

    void CloseDialogue()
    {
        RedSifuPanel.SetActive(false); 
        isDialogueActive = false;
    }
}
