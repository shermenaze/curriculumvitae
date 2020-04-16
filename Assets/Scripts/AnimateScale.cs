using System;
using UnityEngine;
using DG.Tweening;

public class AnimateScale : MonoBehaviour, IAnimate
{
    [SerializeField] private Ease _animIn;
    [SerializeField] private Ease _animOut;
    [SerializeField] private float _duration = 1f;


    public void AnimIn()
    {
        transform.DOScale(Vector3.one, _duration).SetEase(_animIn, 0.2f, 10f);
    }

    public void AnimOut()
    {
        transform.DOScale(Vector3.zero, _duration).SetEase(_animOut, 0.2f, 10f);
    }
}