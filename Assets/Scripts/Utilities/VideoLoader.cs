using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Video;

public class VideoLoader : MonoBehaviour
{
    [SerializeField] private VideoPlayer _videoPlayer;
    [SerializeField] private RenderTexture _renderTexture;
    [SerializeField] private string _uriPath;
    [SerializeField] private bool _playOnStart;

    private void Awake()
    {
        if (!_videoPlayer) _videoPlayer = GetComponentInChildren<VideoPlayer>();
        
        _videoPlayer.playOnAwake = false;
        
        var result = Uri.TryCreate(_uriPath, UriKind.Absolute, out var uriResult)
                      && uriResult.Scheme == Uri.UriSchemeHttps;
        
        if (_videoPlayer && result) _videoPlayer.url = _uriPath;
    }

    private void Start()
    {
        _renderTexture.Release();
        
        _videoPlayer.Prepare();
        
        if(_playOnStart) _videoPlayer.prepareCompleted += source => _videoPlayer.Play();
    }

    [ContextMenu("AnimateAndPlay")]
    public void AnimateAndPlay() 
    {
        transform.DOLocalMove(new Vector3(-2.17f, 1, 3.65f), 1).SetEase(Ease.InOutQuint)
            .OnComplete(()=>transform.DORotate(new Vector3(0, -67, -27.5f), 1.5f).SetEase(Ease.InOutQuint)
                .OnComplete(() => { if (_videoPlayer.isPrepared) _videoPlayer.Play(); }));
    }
}
