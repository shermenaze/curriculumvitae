using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

public class ButtonObject : Item
{
    [SerializeField] private FollowTarget _followTarget;
    [SerializeField] private Transform _screen;
    [SerializeField] private Transform _player; 
    [SerializeField] private UnityEvent _event; 
    
    public override void Interact()
    {
        _screen.position = new Vector3(
            x: _player.position.x, y: _screen.position.y, _player.position.z + 3f);
        _followTarget.AddHeight(2);
        _screen.GetComponentInChildren<VideoPlayer>().Play();
        _event?.Invoke();
    }
}