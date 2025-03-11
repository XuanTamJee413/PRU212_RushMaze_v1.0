using UnityEngine;
using UnityEngine.UI;

public class BlacksmithController : MonoBehaviour
{
    public GameObject BlacksmithPanel;
    public Text dialogueText;
    public Button taskButton, storyButton, exitButton;

    private int currentStep = 0;
    private bool isDialogueActive = false;

    void Start()
    {
        BlacksmithPanel.SetActive(false);

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
        BlacksmithPanel.SetActive(true);
        dialogueText.text = "Chào lữ khách! Cần ta rèn vũ khí cho ngươi không?";
        isDialogueActive = true;
    }

    void OnTask()
    {
        dialogueText.text = "Nếu muốn thanh kiếm tốt nhất, hãy tìm cho ta Thiên Thạch và Hỏa Tinh.";
    }

    void OnStory()
    {
        if (currentStep == 0)
        {
            dialogueText.text = "Ta từng rèn một thanh kiếm đặc biệt cho một kiếm khách huyền thoại.";
        }
        else if (currentStep == 1)
        {
            dialogueText.text = "Thanh kiếm đó có thể cắt xuyên mọi thứ, nhưng chủ nhân của nó biến mất sau một trận chiến.";
        }
        else if (currentStep == 2)
        {
            dialogueText.text = "Có lời đồn rằng thanh kiếm vẫn còn trong mê cung mà ông ấy tạo ra.";
        }
        else if (currentStep == 3)
        {
            dialogueText.text = "Nếu ngươi tìm thấy nó, hãy sử dụng nó một cách khôn ngoan.";
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
        BlacksmithPanel.SetActive(false);
        isDialogueActive = false;
    }
}
