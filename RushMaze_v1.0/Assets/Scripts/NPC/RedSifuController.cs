using UnityEngine;
using UnityEngine.UI;

public class RedSifuController : MonoBehaviour
{
    public GameObject RedSifuPanel; // Panel hội thoại
    public Text dialogueText;        // Text hiển thị lời thoại
    public Button taskButton, storyButton, exitButton; // Các nút

    private int currentStep = 0;
    private bool isDialogueActive = false;

    void Start()
    {
        RedSifuPanel.SetActive(false); // Ẩn hộp thoại lúc đầu

        // Gán sự kiện cho các nút
        taskButton.onClick.AddListener(OnTask);
        storyButton.onClick.AddListener(OnStory);
        exitButton.onClick.AddListener(CloseDialogue);
    }

    void OnMouseDown() // Khi nhấp chuột vào NPC
    {
        Debug.Log("Bạn đã nhấn vào sifu!");
        if (!isDialogueActive) OpenDialogue();
    }

    void OpenDialogue()
    {
        RedSifuPanel.SetActive(true); // Hiện hộp thoại
        dialogueText.text = "Chào con, ta có thể giúp gì cho con?"; // Nội dung mở đầu
        isDialogueActive = true;
    }

    void OnTask()
    {
        dialogueText.text = "Con hãy đến rừng sâu gặp kiếm khách!"; // Nội dung nhiệm vụ
    }

    void OnStory()
    {
        if (currentStep == 0)
        {
            dialogueText.text = "Ngày xưa, ta từng là một kiếm khách lừng danh... ta thu thập những thứ được coi là bí Thuật trên thế gian.";
        }
        else if (currentStep == 1)
        {
            dialogueText.text = "Vì lo sợ bị ám sát và cướp bí thuật, ta đã dùng phương thức tạo mê cung bằng C# script để giấu bí mật này.";
        }
        else if (currentStep == 2)
        {
            dialogueText.text = "Một ngày nọ, ta bị truy sát bởi 10 cao thủ...";
        }
        else if (currentStep == 3)
        {
            dialogueText.text = "Dù kiệt sức, ta vẫn hạ gục bọn chúng bằng một chiêu thức bí truyền, và kịp phân tán bí mật đi khắp các mê cung.";
        }
        else if (currentStep == 4)
        {
            dialogueText.text = "Kể từ đó, ta ẩn cư tại ngôi làng này, truyền thụ võ học cho hậu bối. Đợi một người hữu duyên, thay ta cai quản mê cung.";
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
        RedSifuPanel.SetActive(false); // Đóng hộp thoại
        isDialogueActive = false;
    }
}
