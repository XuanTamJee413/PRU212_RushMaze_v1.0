using UnityEngine;
using UnityEngine.UI;

public class WitchController : MonoBehaviour
{
    public GameObject WitchPanel;
    public Text dialogueText;
    public Button taskButton, storyButton, exitButton;

    private int currentStep = 0;
    private bool isDialogueActive = false;

    void Start()
    {
        WitchPanel.SetActive(false);

        taskButton.onClick.AddListener(OnTask);
        storyButton.onClick.AddListener(OnStory);
        exitButton.onClick.AddListener(CloseDialogue);
    }

    void OnMouseDown()
    {
        if (!isDialogueActive) OpenDialogue();
    }

    void OpenDialogue()
    {
        WitchPanel.SetActive(true);
        dialogueText.text = "Ngươi đến tìm hiểu về bí thuật sao? Liệu ngươi có đủ bản lĩnh?";
        isDialogueActive = true;
    }

    void OnTask()
    {
        dialogueText.text = "Hãy tìm 'Bùa Giải Phong Ấn' nếu muốn tiến sâu hơn vào mê cung.";
    }

    void OnStory()
    {
        if (currentStep == 0)
        {
            dialogueText.text = "Kiếm Khách từng đến gặp ta, nhờ ta phong ấn bí thuật để ngăn kẻ xấu.";
        }
        else if (currentStep == 1)
        {
            dialogueText.text = "Chúng ta đã giấu chúng vào các mê cung và tạo ra những lời nguyền bảo vệ.";
        }
        else if (currentStep == 2)
        {
            dialogueText.text = "Nhưng đã có những kẻ cố phá phong ấn... không ai sống sót để kể lại.";
        }
        else if (currentStep == 3)
        {
            dialogueText.text = "Nếu ngươi thực sự muốn tìm bí thuật, hãy nhớ: một khi vào mê cung, không có đường lui.";
        }
        else
        {
            currentStep = -1;
            CloseDialogue();
        }

        currentStep++;
    }

    void CloseDialogue()
    {
        WitchPanel.SetActive(false);
        isDialogueActive = false;
    }
}
