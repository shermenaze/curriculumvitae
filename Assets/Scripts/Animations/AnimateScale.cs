using UnityEngine;
using DG.Tweening;

public class AnimateScale : MonoBehaviour, IAnimate
{
    [SerializeField] private Ease _animIn;
    [SerializeField] private Ease _animOut;
    [SerializeField] private float _animInDuration = 1f;
    [SerializeField] private float _animOutDuration = 0.4f;
    
    public void AnimIn()
    {
        transform.DOScale(Vector3.one, _animInDuration).SetEase(_animIn, 0.2f, 10f);
    }

    public void AnimOut()
    {
        transform.DOScale(Vector3.zero, _animOutDuration).SetEase(_animOut, 0.2f, 10f);
    }
}