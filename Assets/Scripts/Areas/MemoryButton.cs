using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MemoryButton : Item
{
    [SerializeField] private SpriteRenderer _iconSpriteRenderer;
    [SerializeField] private Transform _buttonRotator;
    
    public SpriteRenderer Renderer => _iconSpriteRenderer;
    
    private AnimatePosition _animate;
    private const float VisibleRotation = 0;
    private const float HiddenRotation = 180;
    public int _buttonNumber;
    private MemoryGame _memoryGame;

    private void Awake()
    {
        _animate = GetComponent<AnimatePosition>();
    }

    public void PreInit(float waitForAnimOut)
    {
        StartCoroutine(InitialRotation(waitForAnimOut));
    }

    public void Init(MemoryGame memoryGame, int buttonNumber)
    {
        _memoryGame = memoryGame;
        _buttonNumber = buttonNumber;
    }

    public override void Interact()
    {
        _memoryGame.CheckMatch(_buttonNumber);
        RotateButton();
    }

    private void RotateButton()
    {
        var rotationVector = Math.Abs(_buttonRotator.eulerAngles.x) < float.Epsilon
            ? VisibleRotation : HiddenRotation;
        var rotation = new Vector3(rotationVector, 0,0);
        
        _buttonRotator.DORotate(rotation, 0.4f, RotateMode.LocalAxisAdd).SetEase(Ease.InOutQuint);
    }

    private IEnumerator InitialRotation(float waitForAnimOut)
    {
        yield return new WaitForSeconds(waitForAnimOut);
        _animate.AnimIn();
        yield return new WaitForSeconds(4);
        RotateButton();
    }
}