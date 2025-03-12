using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoryController : MonoBehaviour
{
    public GameObject StoryPanel; // Panel chứa hội thoại
    public Text dialogueText; // Text hiển thị nội dung câu chuyện
    public Button nextButton, exitButton; // Nút Next và Exit

    private int currentStep = -1;
    private string[] storyTexts =
    {
        // Chương 1
        "Chương 1: Truyền Thuyết Kiếm Khách\n"+
        "Ngày xưa giang hồ vang danh một kiếm khách lừng danh mang tên Red Sifu."+
        "Ông không chỉ tinh thông võ nghệ mà còn thu thập và nghiên cứu những bí thuật thất truyền trên thế gian."+
        "Thế nhưng sức mạnh đi kèm nguy hiểm."+
        "Những kẻ tham lam đã truy sát ông để đoạt lấy bí thuật."+
        "Để bảo vệ bí kíp khỏi rơi vào tay kẻ xấu Red Sifu đã dùng thuật trận pháp để giấu tất cả bí mật vào một mê cung kỳ bí.",

        // Chương 2
        "Chương 2: Trận Chiến Sinh Tử\n"+
        "Tuy đã giấu kín bí mật nhưng điều đó không ngăn được sự truy đuổi."+
        "Một ngày nọ Red Sifu bị phục kích bởi 10 cao thủ mạnh nhất võ lâm."+
        "Cuộc chiến kéo dài nhiều ngày máu đổ khắp nơi."+
        "Tuy bị thương nặng nhưng nhờ vào bí thuật tối thượng Red Sifu đã hạ gục tất cả địch thủ chỉ với một chiêu thức tối thượng."+
        "Tuy nhiên ông biết rằng sẽ không thể giữ bí mật mãi mãi."+
        "Vì vậy trước khi hoàn toàn biến mất khỏi giang hồ ông đã phân tán 5 mảnh ghép bí thuật vào sâu trong mê cung.",

        // Chương 3
        "Chương 3: Người Được Chọn\n"+
        "Hơn 20 năm trôi qua Red Sifu đã trở thành một lão nhân ẩn cư trong một ngôi làng nhỏ."+
        "Ông âm thầm quan sát hậu bối tìm kiếm người có khả năng kế thừa di sản của mình."+
        "Một ngày nọ ông nhìn thấy ánh mắt của bạn – một kiếm sĩ trẻ đầy nhiệt huyết."+
        "Ngươi có sẵn sàng bước vào mê cung và chứng minh bản thân mình xứng đáng với bí thuật này không?",

        // Chương 4
        "Chương 4: Hành Trình Vào Mê Cung\n"+
        "Red Sifu giao cho bạn một nhiệm vụ: thu thập 5 mảnh ghép bí thuật để mở cánh cổng cuối cùng trong mê cung."+
        "Nhưng bạn không đơn độc – những kẻ săn lùng bí thuật vẫn đang ẩn nấp bên trong sẵn sàng tiêu diệt bất kỳ ai bước vào."+
        "Bạn phải lựa chọn: dấn thân vào con đường nguy hiểm này hoặc quay lưng lại với số phận?",

        // Chương 5
        "Chương 5: Lựa Chọn Cuối Cùng \n"+
        "Bạn đã vượt qua vô số thử thách đối mặt với những kẻ thù đáng sợ và tìm ra được bí mật cuối cùng của Red Sifu."+
        "Nhưng trước khi rời khỏi mê cung một giọng nói vang lên:"+
        "Hãy quyết định: sẽ dùng bí thuật này để bảo vệ chính nghĩa hay để thống trị thiên hạ?"+
        "Hành trình của bạn đã đến hồi kết nhưng lựa chọn của bạn sẽ quyết định vận mệnh võ lâm..."+
        "Câu chuyện của bạn bắt đầu từ đây..."
    };

    void Start()
    {

        nextButton.onClick.AddListener(OnNext);
        exitButton.onClick.AddListener(OnExit);
    }

    public void StartStory()
    {
        StoryPanel.SetActive(true);
        currentStep = -1;
        dialogueText.text = storyTexts[currentStep]; // Hiển thị đoạn đầu tiên
    }

    void OnNext()
    {
        currentStep++;

        if (currentStep < storyTexts.Length)
        {
            dialogueText.text = storyTexts[currentStep]; // Hiển thị đoạn tiếp theo
        }
        else
        {
            OnExit(); // Kết thúc câu chuyện -> Quay về menu
        }
    }

    void OnExit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
