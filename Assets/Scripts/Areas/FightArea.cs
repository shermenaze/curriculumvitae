using UnityEngine;

public class FightArea : Area
{
    [SerializeField] private GameObject[] _bugs;
    [SerializeField] private GameObject _weaponContainer;
    [SerializeField] private Transform _player;
    
    private Vector3 _offset = new Vector3(3, 0, -1.5f);
    
    [ContextMenu("AreaEvent")]
    public override void AreaEvent()
    {
        if (_bugs.Length <= 0) return;
        
        foreach (var bug in _bugs)
        {
            bug.transform.position = _player.position + _offset;
            bug.SetActive(true);
            _offset.z += 3;
        }

        if (_weaponContainer)
        {
            _weaponContainer.transform.position = _player.position + new Vector3(-0.5f, 0, 1);
            _weaponContainer.SetActive(true);
        }
    }
}