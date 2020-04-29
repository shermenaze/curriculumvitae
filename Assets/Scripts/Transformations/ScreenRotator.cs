using DG.Tweening;
using RenderHeads.Media.AVProVideo;
using UnityEngine;

public class ScreenRotator : MonoBehaviour
{
    [SerializeField] private MediaPlayer _mediaPlayer;
    
    public void AnimateAndPlay() 
    {
        transform.DOLocalMove(new Vector3(-2.17f, 1, 3.65f), 1).SetEase(Ease.InOutQuint)
            .OnComplete(()=>transform.DORotate(new Vector3(0, -67, -27.5f), 1.5f).SetEase(Ease.InOutQuint)
                .OnComplete(() => { if (_mediaPlayer) _mediaPlayer.Play(); }));
    }
}
