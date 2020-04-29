using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class MemoryButton : Item
{
    [SerializeField] private SpriteRenderer _iconSpriteRenderer;
    [SerializeField] private Transform _buttonRotator;

    #region Properties

    public SpriteRenderer Renderer => _iconSpriteRenderer;
    public int ButtonNumber => _buttonNumber;
    public bool Enabled { set => _collider.enabled = value; }
    public AnimatePosition Animate => _animate;

    #endregion

    #region Fields

    private Collider _collider;
    private MemoryGame _memoryGame;
    private AnimatePosition _animate;
    private bool _disabled;
    private int _buttonNumber;
    private const float ShowRotation = 0;
    private const float HideRotation = 180;

    #endregion

    private void Awake()
    {
        _animate = GetComponent<AnimatePosition>();
        _collider = GetComponent<Collider>();
        Enabled = false;
    }

    public void PreInit(MemoryGame memoryGame, int buttonNumber)
    {
        _memoryGame = memoryGame;
        _buttonNumber = buttonNumber;
    }

    public void Init(float waitForAnimOut)
    {
        StartCoroutine(InitialRotation(waitForAnimOut));
    }

    public override void Interact()
    {
        _memoryGame.CheckMatch(this);
    }

    /// <summary>
    /// Rotate the button with an optional delay delay
    /// </summary>
    /// <param name="enabledWhenDone">Should this button be clickable when done rotating</param>
    /// <param name="delay">Optional pre-rotation delay</param>
    public void RotateButton(bool enabledWhenDone, float delay = 0)
    {
        Enabled = false;
        
        //If button's X rotation is 0, turn 180 and vice verse
        var rotationVector = Math.Abs(_buttonRotator.eulerAngles.x) < float.Epsilon
            ? ShowRotation : HideRotation;
        
        var rotation = new Vector3(rotationVector, 0,0);

        _buttonRotator.DORotate(rotation, 0.4f, RotateMode.LocalAxisAdd)
            .SetEase(Ease.InOutQuint).SetDelay(delay).OnComplete(() => Enabled = enabledWhenDone);
    }
    
    /// <summary>
    /// Let player watch the symbols for a set duration and then turn all buttons
    /// </summary>
    /// <param name="waitForAnimIn"></param>
    /// <returns></returns>
    private IEnumerator InitialRotation(float waitForAnimIn)
    {
        yield return new WaitForSeconds(waitForAnimIn);
        _animate.AnimIn();

        yield return new WaitForSeconds(4);
        RotateButton(true);
    }

    public void GameWon()
    {
        var rotation = new Vector3(1080, 0,0);
        _buttonRotator.DORotate(rotation, 2.5f, RotateMode.LocalAxisAdd)
            .OnComplete(() => _animate.AnimOut());
    }
}