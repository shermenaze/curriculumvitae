using System;
using System.Linq;
using UnityEngine;

public class FightArea : Area
{
    [SerializeField] private Bug[] _bugs;
    [SerializeField] private GameObject _weaponContainer;
    [SerializeField] private Transform _player;
    
    private Vector3 _offset = new Vector3(3, 0, -1.5f);
    private PlayerController _playerController;
    
    private static readonly int PopIn = Animator.StringToHash("PopIn");

    private void Start()
    {
        Signals.Get<OnBugDeath>().AddListener(BugKilled);
    }

    public override void AreaEvent()
    {
        if (_bugs.Length >= 0) EnableAndRepositionBugs();
        if (_weaponContainer) EnableAndRepositionWeaponContainer();
        
        _playerController = _player.GetComponent<PlayerController>();
        _playerController.SetState(PlayerStates.Shoot); //TODO: Create an event
    }

    private void EnableAndRepositionWeaponContainer()
    {
        _weaponContainer.transform.position = _player.position + new Vector3(-0.5f, 0, 1.3f);
        _weaponContainer.SetActive(true);
    }

    private void EnableAndRepositionBugs()
    {
        foreach (var bug in _bugs)
        {
            bug.transform.position = _player.position + _offset;
            bug.gameObject.SetActive(true);
            _offset.z += 3;
        }
    }

    private void BugKilled()
    {
        Debug.Log(_playerController.transform.name);
        if (_bugs.Any(bug => bug.IsAlive)) return;

        _weaponContainer.transform.parent = null;
        _weaponContainer.GetComponent<Animator>().SetBool(PopIn, true);
        Destroy(_weaponContainer.gameObject, 2);

        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        Debug.Log($"{name}, was disabled - player to walking state");
        if(_playerController)
            _playerController.SetState(PlayerStates.Walking);
    }
}