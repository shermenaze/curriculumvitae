using System;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    #region Properties

    public Animator Animator => _animator;
    public NavMeshAgent NavMeshAgentAgent => _navMeshAgent;
    public IHitProvider HitProvider => _hitByRayProvider;
    public ItemsInteraction ItemsInteraction => _itemsInteraction;

    #endregion
    
    #region Fields
    
    private IHitProvider _hitByRayProvider;
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private PlayerBaseState _currentState;
    private ItemsInteraction _itemsInteraction;
    
    #endregion

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _hitByRayProvider = GetComponent<IHitProvider>();
        _animator = GetComponent<Animator>();
        _itemsInteraction = GetComponent<ItemsInteraction>();
        
        SetState(PlayerStates.Walking);
    }

    private void Start()
    {
        Signals.Get<TextReceived>().AddListener(StopMoving);
        Signals.Get<TextEndSignal>().AddListener(StartMoving);
    }

    /// <summary>
    /// Sets the state of the Player
    /// </summary>
    /// <param name="state"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public void SetState(PlayerStates state)
    {
        PlayerBaseState playerState;
        
        switch (state)
        {
            case PlayerStates.Shoot:
                playerState = new ShootingState(this);
                break;
            case PlayerStates.Walking:
                playerState = new WalkingState(this);
                break;
            case PlayerStates.Talking:
                playerState = new TalkingState(this);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
        
        _currentState?.ExitState();

        _currentState = playerState;

        _currentState.EnterState();
    }

    private void Update()
    {
        _currentState?.Update();
    }

    private void StartMoving()
    {
        _navMeshAgent.isStopped = false;
    }

    private void StopMoving(TextSO textSo)
    {
        _navMeshAgent.isStopped = true;
    }
 
    public void UnParent()
    {
        transform.SetParent(null, true);
    }

    public void ParentTo(Transform newParent = null, bool worldPositionStays = true)
    {
        transform.SetParent(newParent, worldPositionStays);
    }
}