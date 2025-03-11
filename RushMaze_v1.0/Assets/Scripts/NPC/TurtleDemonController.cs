using UnityEngine;
using UnityEngine.UI;

public class TurtleDemonController : MonoBehaviour
{
    public GameObject TurtleDemonPanel;
    public Text dialogueText;
    public Button taskButton, storyButton, exitButton;

    private int currentStep = 0;
    private bool isDialogueActive = false;

    void Start()
    {
        TurtleDemonPanel.SetActive(false);

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
        TurtleDemonPanel.SetActive(true);
        dialogueText.text = "Ngươi là ai? Ngươi có biết lời nguyền nơi này không?";
        isDialogueActive = true;
    }

    void OnTask()
    {
        dialogueText.text = "Nếu muốn sống sót, hãy tìm 'Ngọc Hộ Mệnh' trong mê cung.";
    }

    void OnStory()
    {
        if (currentStep == 0)
        {
            dialogueText.text = "Ta từng là một chiến binh, nhưng bị nguyền rủa vì tham vọng bí thuật.";
        }
        else if (currentStep == 1)
        {
            dialogueText.text = "Bí thuật có thể mang lại sức mạnh vô biên, nhưng cũng có thể huỷ diệt linh hồn ngươi.";
        }
        else if (currentStep == 2)
        {
            dialogueText.text = "Kiếm Khách là người duy nhất từng sử dụng bí thuật mà không bị hủy diệt.";
        }
        else if (currentStep == 3)
        {
            dialogueText.text = "Nếu ngươi thực sự muốn bước vào mê cung, hãy chuẩn bị tinh thần đối mặt với chính bản thân mình.";
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
        TurtleDemonPanel.SetActive(false);
        isDialogueActive = false;
    }
}
