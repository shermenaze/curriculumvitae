using System;
using System.Linq;
using UnityEngine;

public class FightArea : Area
{
    #region Fields

    [SerializeField] private Bug[] _bugs;
    [SerializeField] private GameObject _weaponContainer;
    [SerializeField] private AudioClip _bugEmergeSound;
    [SerializeField] private Transform _player;
    [SerializeField] private TextSO _fightWonText;

    private Vector3 _offset = new Vector3(3, 0, -3f);
    private PlayerController _playerController;
    private static readonly int PopIn = Animator.StringToHash("PopIn");
    private static readonly int Talking = Animator.StringToHash("Talking");
    private const int TimeToDestroy = 2;

    #endregion

    private void Start()
    {
        _playerController = _player.GetComponent<PlayerController>();
        if(_playerController) _startTextSo.AddEvent(() => _playerController.SetState(PlayerStates.Talking));
        
        _startTextSo.AddEvent(AreaEvent);
        _startTextSo.AddEvent(() =>
        {
            Debug.Log(_weaponContainer);
            if (_weaponContainer) EnableAndRepositionWeaponContainer();
        });
        
        Signals.Get<OnBugDeath>().AddListener(BugKilled);
    }

    public override void AreaEvent()
    {
        if (_bugs.Length >= 0) EnableAndRepositionBugs();
        
        if(_playerController) _playerController.SetState(PlayerStates.Shoot); //TODO: Create an event
    }

    /// <summary>
    /// Reposition weapon container next to player
    /// </summary>
    private void EnableAndRepositionWeaponContainer()
    {
        _weaponContainer.transform.position = _player.position + new Vector3(-0.5f, 0, 1.3f);
        _weaponContainer.SetActive(true);
    }

    /// <summary>
    /// Reposition bugs close to player
    /// </summary>
    private void EnableAndRepositionBugs()
    {
        foreach (var bug in _bugs)
        {
            bug.transform.position = _player.position + _offset;
            bug.gameObject.SetActive(true);
            _offset.z += 4;
        }
        
        AudioManager.Instance.PlaySound(_bugEmergeSound, 1f);
    }

    private void BugKilled()
    {
        if (_bugs.Any(bug => bug.IsAlive)) return;
        
        _playerController.AbleToShoot = false;
        
        AreaDone();
    }

    /// <summary>
    /// When the are is won Remove unused items and associate player events
    /// </summary>
    private void AreaDone()
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
            _fightWonText.AddEvent(() => _playerController.SetState(PlayerStates.Talking));
            _fightWonText.AddEvent(() =>
            {
                _playerController.Animator.SetBool(Talking, false);
                _playerController.SetState(PlayerStates.Walking);
            });
        }

        Signals.Get<TextReceived>().Dispatch(_fightWonText);
    }

    private void OnDisable()
    {
        Signals.Get<OnBugDeath>().RemoveListener(BugKilled);
    }
}