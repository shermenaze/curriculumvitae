using System;
using RenderHeads.Media.AVProVideo;
using UnityEngine;

public class VideoLoader : MonoBehaviour
{
    [SerializeField] private MediaPlayer _mediaPlayer;
    [SerializeField] private RenderTexture _renderTexture;
    [SerializeField] private string _uriPath;
    [SerializeField] private Replay _replay;
    [SerializeField] private bool _playOnAwake;

    private void Awake()
    {
        var result = Uri.TryCreate(_uriPath, UriKind.Absolute, out var uriResult)
                      && uriResult.Scheme == Uri.UriSchemeHttps;
        
        if(_mediaPlayer && result) _mediaPlayer.m_VideoPath = _uriPath;

        _mediaPlayer.Events.AddListener(VideoEvents);
    }

    private void Start()
    {
        _mediaPlayer.OpenVideoFromFile(MediaPlayer.FileLocation.AbsolutePathOrURL, _uriPath, false);
        _renderTexture.Release();
    }
    
    private void VideoEvents(MediaPlayer mp, MediaPlayerEvent.EventType et, ErrorCode error)
    {
        switch (et)
            {
                case MediaPlayerEvent.EventType.FirstFrameReady:
                    if(_playOnAwake) _mediaPlayer.Play();
                    break;
                case MediaPlayerEvent.EventType.FinishedPlaying:
                    if(_replay) _replay.gameObject.SetActive(true);
                    break;
            }
    }
}
