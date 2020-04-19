using System;
using DG.Tweening;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable, IPickupable
{
    [SerializeField] private ItemSO _itemSo;
    
    private MeshRenderer _meshRenderer;
    private readonly Color _selectedColor = Color.green;
    private readonly Color _naturalColor = Color.black;
    
    private static readonly int RimColor = Shader.PropertyToID("_RimColor");

    private void Awake()
    {
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    public void Interact()
    {
        //TODO: Add interaction
    }

    public void Highlight()
    {
        if (!_meshRenderer.material) return;
        _meshRenderer.material.SetColor(RimColor, _selectedColor);
    }

    public void DoneInteracting()
    {
        if (!_meshRenderer.material) return;
        _meshRenderer.material.SetColor(RimColor, _naturalColor);
    }

    public void PickUp(Transform parent)
    {
        var item = Instantiate(_itemSo.Prefab, parent.localPosition, parent.localRotation, parent);
        item.transform.position = parent.position;
        item.transform.rotation = parent.rotation;
        
        Destroy(gameObject);
    }
}