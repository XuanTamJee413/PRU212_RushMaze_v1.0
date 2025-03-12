﻿using Assets.Scripts.Model;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TurtleDemonController : MonoBehaviour
{
    public GameObject TurtleDemonPanel; // Panel hội thoại
    public Text dialogueText;        // Text hiển thị lời thoại
    public Button lvl1, lvl2, lvl3, lvl4, lvl5, exitButton;

    private int currentStep = 0;
    private bool isDialogueActive = false;

    void Start()
    {
        TurtleDemonPanel.SetActive(false);

        // Gán sự kiện cho các nút
        lvl1.onClick.AddListener(LoadLevel1);
        lvl2.onClick.AddListener(LoadLevel2);
        lvl3.onClick.AddListener(LoadLevel2);
        lvl4.onClick.AddListener(LoadLevel2);
        lvl5.onClick.AddListener(LoadLevel2);
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
        Debug.Log("Bạn đã nhấn vào TurtleDemon!");
        if (!isDialogueActive) OpenDialogue();
    }

    void OpenDialogue()
    {
        TurtleDemonPanel.SetActive(true);
        isDialogueActive = true;
    }



    void CloseDialogue()
    {
        TurtleDemonPanel.SetActive(false);
        isDialogueActive = false;
    }
}
