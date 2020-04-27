using System;
using DG.Tweening;
using UnityEngine;

public class WinState : PlayerBaseState
{
    #region Fields

    private Transform _playerTransform;
    private static readonly int Talking = Animator.StringToHash("Talking");
    private static readonly int Victory = Animator.StringToHash("Victory");

    #endregion

    public WinState(PlayerController controller) : base(controller) { }

    public override void EnterState()
    {
        _playerTransform = _controller.transform;
        _controller.Animator.SetBool(Talking, true);
        _controller.Animator.SetTrigger(Victory);
    }

    public override void Update()
    {
        RotateTowardsCamera();
    }
    
    private void RotateTowardsCamera()
    {
        //If the player is facing the camera, don't rotate.
        if (!(Math.Abs(_playerTransform.rotation.eulerAngles.y - 195) > float.Epsilon)) return;

        var eulerAngles = _playerTransform.eulerAngles;
        var rotation = new Vector3(eulerAngles.x, 195, eulerAngles.x);
        _playerTransform.DORotate(rotation, 0.6f);
    }

    public override void ExitState() { }
}