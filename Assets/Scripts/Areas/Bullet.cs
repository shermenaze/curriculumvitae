using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;

    private void Awake()
    {
        Destroy(gameObject, 4f);
    }

    void Update()
    {
        var step = Time.deltaTime * _speed;
        transform.Translate(step * Vector3.forward);
    }
}