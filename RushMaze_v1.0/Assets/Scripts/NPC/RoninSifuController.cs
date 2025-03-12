using Assets.Scripts.Model;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoninSifuController : MonoBehaviour
{
    public GameObject RedSifuPanel; // Panel hội thoại
    public Text dialogueText;        // Text hiển thị lời thoại
    public Button lvl1, lvl2, exitButton; 

    private int currentStep = 0;
    private bool isDialogueActive = false;

    void Start()
    {
        RedSifuPanel.SetActive(false);

        // Gán sự kiện cho các nút
        lvl1.onClick.AddListener(LoadLevel1);
        lvl2.onClick.AddListener(LoadLevel2);
        exitButton.onClick.AddListener(CloseDialogue);
    }
    public void LoadLevel1()
    {
        LevelData.SetLevelData(5, 5, 5, 10);
        Debug.Log($"LoadLevel1: {LevelData.MazeWidth}, {LevelData.MazeHeight}");
        SceneManager.LoadScene("EnemyScene");
    }

    public void LoadLevel2()
    {
        LevelData.SetLevelData(20, 20, 10, 20); 
        SceneManager.LoadScene("EnemyScene");
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
