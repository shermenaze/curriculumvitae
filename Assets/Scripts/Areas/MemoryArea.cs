using System;
using UnityEngine;

public class MemoryArea : Area
{
    [SerializeField] private MemoryGame _memoryGame;
    [SerializeField] private Transform _screenLookAtPosition;
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _buttonPole;
    [SerializeField] private TextSO _gameWonTextSo;
    [SerializeField] private FollowTarget _followTarget;
    [SerializeField] private VideoLoader _videoLoader;

    private static readonly int Talking = Animator.StringToHash("Talking");
    private static readonly int Dance = Animator.StringToHash("Dance");

    private void Start()
    {
        var playerController = _player.GetComponent<PlayerController>();

        if (playerController)
        {
            _startTextSo.AddEvent(() => playerController.SetState(PlayerStates.Talking));
            _startTextSo.AddEvent(() =>
            {
                AreaEvent();
                playerController.Animator.SetBool(Talking, false);
            });
            
            Signals.Get<OnMemoryGameWon>().AddListener(() => GameWonEvents(playerController));
        }
    }

    private void GameWonEvents(PlayerController playerController)
    {
        _gameWonTextSo.AddEvent(() => RepositionScreen(3.5f));
        _gameWonTextSo.AddEvent(() =>
        {
            playerController.Animator.SetBool(Dance, true);
            _videoLoader.AnimateAndPlay();
        });
        
        Signals.Get<TextReceived>().Dispatch(_gameWonTextSo);
    }

    private void RepositionScreen(float prepareTime)
    {
        if(_followTarget) _followTarget.SetTarget(_screenLookAtPosition, prepareTime);
    }

    public override void AreaEvent()
    {
        RePositionMemoryGame();
        
        //InitiateButtonPole();
        //_playerController.Animator.SetTrigger(Interact);
    }
    
    private void InitiateButtonPole()
    {
        //Move _buttonPole to the proper position and AnimateIn

        _buttonPole.position = new Vector3(
            x: _player.position.x - 0.3f, y: _buttonPole.position.y, _player.position.z - 0.3f);
        var animate = _buttonPole.GetComponent<IAnimate>();
        animate?.AnimIn();
    }
    
    private void RePositionMemoryGame()
    {
        var newPosition = new Vector3(_player.position.x + 2f, -0.5f, _player.position.z + 0.5f);
        _memoryGame.transform.position = newPosition;
        _memoryGame.Init();
    }
}