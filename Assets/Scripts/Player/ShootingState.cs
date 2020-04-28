using UnityEngine;


public class ShootingState : PlayerBaseState
{
    #region Fields

    private readonly IHitProvider _hitProvider;
    private readonly Transform _transform;
    private readonly Animator _animator;
    private readonly PlayerController _playerController;
    private float _timePassed;
    private float _timeBetweenShots = 1f;
    private bool _gunReady;

    #endregion

    #region Animation Hashs

    private static readonly int ReadyToFight = Animator.StringToHash("ReadyToFight");
    private static readonly int ReadyToShoot = Animator.StringToHash("ReadyToShoot");
    private static readonly int Shoot = Animator.StringToHash("Shoot");

    #endregion

    public ShootingState(PlayerController controller) : base(controller)
    {
        _playerController = controller;
        _hitProvider = _controller.HitProvider;
        _transform = _controller.transform;
        _animator = _controller.Animator;
    }

    public override void EnterState()
    {
        _animator.SetBool(ReadyToFight, true);
        _playerController.AbleToShoot = true;
    }

    public override void Update()
    {
        if (_controller.ItemsInteraction.ItemInHand) _animator.SetBool(ReadyToShoot, true);

        RotateTowardsMouse();

        if (Input.GetMouseButtonDown(0)) FireWeapon();
    }

    private void FireWeapon()
    {
        if (Time.time >= _timePassed + _timeBetweenShots && _gunReady)
        {
            _timePassed = Time.time;
            var shootable = _playerController.ItemsInteraction.ItemInHand.GetComponent<IShootable>();
            
            if (_animator.GetBool(ReadyToShoot) && _playerController.AbleToShoot)
            {
                if (_hitProvider.HitAnyLayer(out var hit))
                    shootable?.Shoot(hit.transform.CompareTag("Enemy") ? hit.transform : null);

                _animator.SetTrigger(Shoot);
                AudioManager.Instance.PlaySound(_playerController.ShootSound);
            }
        }
        
        if(_controller.ItemsInteraction.ItemInHand) _gunReady = true;
    }

    private void RotateTowardsMouse()
    {
        //Get the Screen positions of the object
        Vector2 positionOnScreen = _hitProvider.CurrentCamera.WorldToViewportPoint(_transform.position);

        //Get the Screen position of the mouse
        Vector2 mouseOnScreen = _hitProvider.CurrentCamera.ScreenToViewportPoint(Input.mousePosition);

        //Get the angle between the points
        float AngleBetweenTwoPoints(Vector3 a, Vector3 b) => Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
        var angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);

        _transform.rotation = Quaternion.Euler(new Vector3(0f, -angle - 75, 0));
    }

    public override void ExitState()
    {
        if (_animator)
        {
            //_animator.ResetTrigger(Shoot);
            _animator.SetBool(ReadyToShoot, false);
            _animator.SetBool(ReadyToFight, false);
        }

        _controller.ItemsInteraction.DestroyItemInHand();
    }
}