using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsInteraction : MonoBehaviour
{
    [SerializeField] private Transform _parent;

    private IHitProvider _hitProvider;

    private void Awake()
    {
        _hitProvider = GetComponent<IHitProvider>();
    }

    void Update()
    {
        if (!_hitProvider.HitAnyLayer(out var hit)) return;

        var pickUp = hit.transform.GetComponent<IPickupable>();
        var interact = hit.transform.GetComponent<IInteractable>();

        interact?.Highlight();

        if (Input.GetMouseButtonDown(0))
        {
            interact?.Interact();
            pickUp?.PickUp(_parent);
        }
    }
}
