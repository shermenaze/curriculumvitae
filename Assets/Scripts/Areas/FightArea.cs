﻿using System.Linq;
using UnityEngine;

public class FightArea : Area
{
    #region Fields

    [SerializeField] private Bug[] _bugs;
    [SerializeField] private GameObject _weaponContainer;
    [SerializeField] private Transform _player;
    [SerializeField] private TextSO _fightWonText;

    private Vector3 _offset = new Vector3(3, 0, -1.5f);
    private PlayerController _playerController;
    private static readonly int PopIn = Animator.StringToHash("PopIn");
    private static readonly int Talking = Animator.StringToHash("Talking");
    private const int TimeToDestroy = 2;

    #endregion

    private void Start()
    {
        _playerController = _player.GetComponent<PlayerController>();
        _startTextSo.AddEvent(AreaEvent);
        
        Signals.Get<OnBugDeath>().AddListener(BugKilled);
    }

    public override void AreaEvent()
    {
        if (_bugs.Length >= 0) EnableAndRepositionBugs();
        if (_weaponContainer) EnableAndRepositionWeaponContainer();
        
        if(_playerController) _playerController.SetState(PlayerStates.Shoot); //TODO: Create an event
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
        if (_bugs.Any(bug => bug.IsAlive)) return;
        AreaDoneCleanUp();
    }

    private void AreaDoneCleanUp()
    {
        RemoveWeaponContainer();
        
        PlayerEvents();
    }

    private void RemoveWeaponContainer()
    {
        //Animate Out _weaponContainer and destroy it
        
        _weaponContainer.transform.parent = null;
        _weaponContainer.GetComponent<Animator>().SetBool(PopIn, true);
        Destroy(_weaponContainer.gameObject, TimeToDestroy);
    }

    private void PlayerEvents()
    {
        if (_playerController)
        {
            _playerController.SetState(PlayerStates.Talking);
            _fightWonText.AddEvent(() =>
            {
                _playerController.Animator.SetBool(Talking, false);
                _playerController.SetState(PlayerStates.Walking);
            });
        }

        Signals.Get<TextReceived>().Dispatch(_fightWonText);
    }
}