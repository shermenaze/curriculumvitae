using UnityEngine;

public class HelloKittyGunItem : Item, IPickupable
{
    [SerializeField] private ItemSO _itemSo;

    public void PickUp(Transform parent, out GameObject item)
    {
        var thisItem = Instantiate(_itemSo.Prefab, parent.localPosition, parent.localRotation, parent);
        thisItem.transform.position = parent.position;
        thisItem.transform.rotation = parent.rotation;
        
        item = thisItem;

        Destroy(gameObject);
    }
}