using System;
using UnityEngine;
using UnityEngine.Video;

public class VideoLoader : MonoBehaviour
{
    [SerializeField] private VideoPlayer _player;

    private const string _uriPath = "https://dl.dropbox.com/s/9cn6qg89hq4p47l/Video1H264.mp4";

    private void Awake()
    {
        if (!_player) _player = GetComponentInChildren<VideoPlayer>();
        
        _player.playOnAwake = false;
        
        var result = Uri.TryCreate(_uriPath, UriKind.Absolute, out var uriResult)
                      && uriResult.Scheme == Uri.UriSchemeHttps;
        
        if (_player && result) _player.url = _uriPath;
    }

    private void Start()
    {
        _player.Prepare();
    }

    [ContextMenu("PlayVideo")]
    public void PlayVideo()
    {
        if(_player.isPrepared) _player.Play();
    }
}
