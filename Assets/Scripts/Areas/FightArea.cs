﻿using System.Linq;
using UnityEngine;

public class FightArea : Area
{
    #region Fields

    [SerializeField] private Bug[] _bugs;
    [SerializeField] private GameObject _weaponContainer;
    [SerializeField] private Transform _buttonPole;
    [SerializeField] private Transform _player;
    [SerializeField] private TextSO _fightWonText;

    private Vector3 _offset = new Vector3(3, 0, -1.5f);
    private PlayerController _playerController;
    private static readonly int PopIn = Animator.StringToHash("PopIn");
    private static readonly int Interact = Animator.StringToHash("Interact");
    private const int TimeToDestroy = 2;

    #endregion

    private void Start()
    {
        Signals.Get<OnBugDeath>().AddListener(BugKilled);

        _startTextSo.AddEvent(AreaEvent);
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
        if (_bugs.Any(bug => bug.IsAlive)) return;
        AreaDoneCleanUp();
    }

    private void AreaDoneCleanUp()
    {
        //Animate Out _weaponContainer and destroy it
        EndWeaponContainer();

        //Move _buttonPole to the proper position and AnimateIn
        InitiateButtonPole();

        PlayerEvents();
    }

    private void EndWeaponContainer()
    {
        _weaponContainer.transform.parent = null;
        _weaponContainer.GetComponent<Animator>().SetBool(PopIn, true);
        Destroy(_weaponContainer.gameObject, TimeToDestroy);
    }

    private void InitiateButtonPole()
    {
        _buttonPole.position = new Vector3(
            x: _player.position.x, y: _buttonPole.position.y, _player.position.z - 0.5f);
        var animate = _buttonPole.GetComponent<IAnimate>();
        animate?.AnimIn();
    }

    private void PlayerEvents()
    {
        if (_playerController) _playerController.SetState(PlayerStates.Talking);
        
        Signals.Get<TextReceived>().Dispatch(_fightWonText);
    }
}