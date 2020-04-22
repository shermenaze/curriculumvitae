using UnityEngine;
using UnityEngine.EventSystems;

public class WalkingState : PlayerBaseState
{
    private static readonly int Idle = Animator.StringToHash("Idle");

    public WalkingState(PlayerController controller) : base(controller) { }

    public override void EnterState()
    {
        //_controller.Animator.SetTrigger(Idle);
    }

    public override void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        
        if (Input.GetMouseButton(0))
        {
            if (_controller.HitProvider.HitByPreDefinedLayer(out var hit))
                _controller.NavMeshAgentAgent.destination = hit.point;
        }
    }

    public override void ExitState()
    {
        _controller.NavMeshAgentAgent.SetDestination(_controller.transform.position); //TODO: Try nextPosition
    }
}