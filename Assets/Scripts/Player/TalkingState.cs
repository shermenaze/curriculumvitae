using System;
using DG.Tweening;
using UnityEngine;

public class TalkingState : PlayerBaseState
{
    public TalkingState(PlayerController controller) : base(controller) { }

    private Transform _playerTransform;
    private static readonly int Talking = Animator.StringToHash("Talking");

    public override void EnterState()
    {
        _playerTransform = _controller.transform;
        _controller.Animator.SetBool(Talking, true);
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

    public override void ExitState()
    {
        _controller.Animator.SetBool(Talking, false);
    }
}