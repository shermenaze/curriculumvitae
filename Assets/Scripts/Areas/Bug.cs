﻿using System.Collections;
using DG.Tweening;
using UnityEngine;

public class OnBugDeath : ASignal { }

public class Bug : MonoBehaviour
{
    [SerializeField] private GameObject _digoutParticlesPrefab;
    [SerializeField] private Transform _floor;
    
    public bool IsAlive => _isAlive;

    private Animator _animator;
    private Vector3 _localInstantiatePoint;
    private Vector3 _globalInstantiatePoint;
    private int _life = 2;
    private bool _isAlive = true;
    private const float TimeToWait = 1.8f;

    private static readonly int TakeDamage = Animator.StringToHash("TakeDamage");
    private static readonly int Die = Animator.StringToHash("Die");
    private static readonly int DissolveAmount = Shader.PropertyToID("_DissolveAmount");


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
            //_life--;
            _animator.SetTrigger(TakeDamage);
            
            if (--_life <= 0) Kill();
        }
    }

    private void Kill()
    {
        transform.parent = null;
        _isAlive = false;
        _animator.SetTrigger(Die);
        Signals.Get<OnBugDeath>().Dispatch();
        StartCoroutine(Dissolve());
        Destroy(gameObject, 3f);
    }

    private IEnumerator Dissolve()
    {
        var material = GetComponentInChildren<SkinnedMeshRenderer>().material;
        float timer = 0;

        while (timer < TimeToWait)
        {
            timer += Time.deltaTime;
            material.SetFloat(DissolveAmount, timer.Remap(0,TimeToWait,0,1));
            yield return null;
        }
    }
}