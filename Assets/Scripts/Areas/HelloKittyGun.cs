using UnityEngine;

public class HelloKittyGun : MonoBehaviour, IShootable
{
    [SerializeField] private ItemSO _bullet;
    [SerializeField] private Transform _fireFrom;
    
    private ParticleSystem _particles;

    private void Awake()
    {
        _particles = GetComponentInChildren<ParticleSystem>();
    }

    [ContextMenu("Start")]
    private void Start()
    {
        _particles.Play();
    }

    public void Shoot(Transform target)
    {
        var bullet = Instantiate(_bullet.Prefab, _fireFrom.position, _fireFrom.rotation);
        if(target != null) bullet.transform.LookAt(target);
    }

    private void OnDestroy()
    {
        _particles.Play();
        _particles.transform.parent = null;
        Destroy(_particles.gameObject, 1f);
    }
}