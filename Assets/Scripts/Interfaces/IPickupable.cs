using UnityEngine;

public interface IPickupable
{
    void PickUp(Transform parent, out GameObject item);
}