using UnityEngine;
using DG.Tweening;

public class AnimatePosition : MonoBehaviour, IAnimate
{
    [SerializeField] private Ease _animIn;
    [SerializeField] private Ease _animOut;
    [SerializeField] private float _duration = 1f;

    private RectTransform _rect;

    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
    }

    public void AnimIn()
    {
        _rect.DOLocalMove(new Vector3(0, 457), _duration).SetEase(_animIn, 0.2f, 10f);
    }

    public void AnimOut()
    {
        _rect.DOLocalMove(new Vector3(0, 631), _duration).SetEase(_animOut, 0.2f, 10f);
    }
}