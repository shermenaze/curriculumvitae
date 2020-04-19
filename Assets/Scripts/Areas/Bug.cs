using System;
using DG.Tweening;
using UnityEngine;

public class Bug : MonoBehaviour
{
    [SerializeField] private GameObject _digoutParticlesPrefab;
    [SerializeField] private Transform _floor;
    
    private Animator _animator;
    private Vector3 _localInstantiatePoint;
    private Vector3 _globalInstantiatePoint;
    
    private void Awake()
    {
        var localPosition = transform.localPosition;
        _animator = GetComponent<Animator>();
        _localInstantiatePoint = new Vector3(localPosition.x, 0, localPosition.z);
    }

    private void Start()
    {
        transform.DOLocalMove(_localInstantiatePoint, 0.6f);
        CreateParticleSystem();
    }

    private void CreateParticleSystem()
    {
        var particles = Instantiate(_digoutParticlesPrefab, transform, false);

        var particlesSystem = particles.GetComponentsInChildren<ParticleSystem>();
        foreach (var system in particlesSystem)
        {
            var collision = system.collision;
            collision.type = ParticleSystemCollisionType.Planes;
            collision.SetPlane(0, _floor);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Debug.Log($"{name}, was hit!");
        }
    }
}
