using UnityEngine;

public class MemoryArea : Area
{
    [SerializeField] private MemoryGame _memoryGame;
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _buttonPole;
    [SerializeField] private TextSO _gameWonTextSo;

    private void Start()
    {
        _startTextSo.AddEvent(() =>
        {
            _player.GetComponent<PlayerController>().SetState(PlayerStates.Talking);
            AreaEvent();
        });
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
    
    [ContextMenu("RePositionMemoryGame")] //TODO: Remove
    private void RePositionMemoryGame()
    {
        var newPosition = new Vector3(_player.position.x + 4.5f, transform.position.y, _player.position.z + 0.5f);
        _memoryGame.transform.position = newPosition;
        _memoryGame.Init();
    }
}