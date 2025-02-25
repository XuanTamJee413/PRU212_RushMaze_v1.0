using UnityEngine;
using UnityEngine.Video;

public class TrailerController : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Play();
        }
    }
}
