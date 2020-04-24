using UnityEngine;
using DG.Tweening;

public class AnimatePosition : MonoBehaviour, IAnimate
{
    [SerializeField] private Ease _easeIn;
    [SerializeField] private Ease _easeOut;
    [SerializeField] private float _duration = 1f;
    [SerializeField] private bool _verticalAnimation = true;
    [SerializeField] private float _animInEndPosition;
    [SerializeField] private float _animOutEndPosition;

    private Vector3 AnimInVector
    {
        get
        {
            var localPosition = transform.localPosition;
            
            var animInVector = new Vector3 {
                x = _verticalAnimation ? localPosition.x : _animInEndPosition,
                y = _verticalAnimation ? _animInEndPosition : localPosition.y,
                z = localPosition.z
            };
            return animInVector;
        }
    }
    private Vector3 AnimOutVector
    {
        get
        {
            var localPosition = transform.localPosition;
            
            var animOutVector = new Vector3 {
                x = _verticalAnimation ? localPosition.x : _animOutEndPosition,
                y = _verticalAnimation ? _animOutEndPosition : localPosition.y,
                z = localPosition.z
            };
            return animOutVector;
        }
    }

    [ContextMenu("AnimIn")]
    public void AnimIn()
    {
        transform.DOLocalMove(AnimInVector, _duration).SetEase(_easeIn, 0.2f, 1f);
    }

    [ContextMenu("AnimOut")]
    public void AnimOut()
    {
        transform.DOLocalMove(AnimOutVector, _duration).SetEase(_easeOut, 0.2f, 1f);
    }
}