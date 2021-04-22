using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class CustomTrackableEventHandler : DefaultTrackableEventHandler
{

    public bool isVideoEnabled;
    public string url;


    [Space]

    public GameObject videoGO;
    private Button interactionBtn;
    
    private VideoPlayer videoPlayer;
    private RawImage rawImage;

    private void Awake(){
        interactionBtn=videoGO.GetComponent<Button>();
        videoPlayer=videoGO.GetComponent<VideoPlayer>();
        rawImage=videoGO.GetComponent<RawImage>();

    }
    protected override void Start()
    {
        
        base.Start();
        //generar interacción

        interactionBtn.onClick.AddListener(OpenUrl2);
        StartCoroutine(PrepareVideo());

    }

    private void OpenUrl2(){
        Application.OpenURL(url);
    }

    private IEnumerator PrepareVideo()
    {
        videoPlayer.Prepare();

        while (!videoPlayer.isPrepared)
        {
            yield return new WaitForSeconds(.5f);
        }

        rawImage.texture = videoPlayer.texture;

        isVideoEnabled = true;
    }
    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();
        if(isVideoEnabled){
            videoPlayer.Play();
        }
    }

    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();
        if(isVideoEnabled){
            videoPlayer.Pause();
        }
    }

    private void OpenUrl(){
        Application.OpenURL(url);
    }
}