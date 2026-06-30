using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Video;

public class MyVideoPlayer : MonoBehaviour
{
    [SerializeField] public VideoPlayer videoPlayer;

    public  string video;

    void Start()
    {
        videoPlayer.source = VideoSource.Url;
        videoPlayer.url = Application.streamingAssetsPath + "/" + video;
        videoPlayer.Play();
    }

}



