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

    public void Highlight(bool shouldHighlight)
    {
        if (!_meshRenderer) return;
        //TODO: Don't change color if color is set to the correct one.
        _meshRenderer.material.SetColor(RimColor,
            shouldHighlight ? _selectedColor : _naturalColor);
    }

    public void PickUp(Transform parent, out GameObject item)
    {
        var thisItem = Instantiate(_itemSo.Prefab, parent.localPosition, parent.localRotation, parent);
        thisItem.transform.position = parent.position;
        thisItem.transform.rotation = parent.rotation;
        
        item = thisItem;

        Destroy(gameObject);
    }
}

public class HelloKittyItemGun : Item, IPickupable
{
    
}