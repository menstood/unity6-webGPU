using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.VFX;
using UnityEngine.Video;

public class WebcamDisplay : MonoBehaviour
{
    public RenderTexture renderTexture;
    private WebCamTexture webcamTexture;
    public VisualEffect visualEffect;  
    public VideoPlayer videoPlayer;
    public RawImage rawImage;
    public Texture defaultTexture;  
    public Toggle toggle;  
    private string videoPath = "videofeed.mp4";
    void Start()
    {
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, videoPath);
        toggle.onValueChanged.AddListener((value) =>
          {
              if (value == true)
              {
                  //StartCoroutine(SwithToCamera());
                  SwitchToVideo();
              }
              else
              {
                  SwitchToTexture();
              }
          });
    }

    void SwitchToTexture()
    {
        videoPlayer.Stop();
        rawImage.gameObject.SetActive(false);
        visualEffect.SetTexture("Texture", defaultTexture);
    }

    void SwitchToVideo()
    {
        videoPlayer.Play();
        rawImage.gameObject.SetActive(true);
        visualEffect.SetTexture("Texture", renderTexture);
    }


    IEnumerator SwithToCamera()
    {
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);

        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            if (WebCamTexture.devices.Length > 0)
            {
                if (webcamTexture == null)
                {
                    webcamTexture = new WebCamTexture();
                    webcamTexture.Play();
                }
                visualEffect.SetTexture("Texture", renderTexture);
            }
        }
        else
        {
            Debug.LogWarning("Webcam permission denied.");
        }


    }

    void Update()
    {
        if (webcamTexture != null && renderTexture != null)
        {
            Graphics.Blit(webcamTexture, renderTexture);
        }
    }

    void OnDisable()
    {
        if (webcamTexture != null && webcamTexture.isPlaying)
        {
            webcamTexture.Stop();
        }
    }
}
