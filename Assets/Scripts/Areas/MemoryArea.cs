using UnityEngine;

public class MemoryArea : Area
{
    [SerializeField] private MemoryGame _memoryGame;
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _buttonPole;

    private void Start()
    {
        _startTextSo.AddEvent(AreaEvent);
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
        var newPosition = new Vector3(_player.position.x + 8, transform.position.y, _player.position.z + 2);
        _memoryGame.transform.position = newPosition;
        _memoryGame.Init();
    }
}