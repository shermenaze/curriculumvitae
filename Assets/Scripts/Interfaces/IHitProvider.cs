using UnityEngine;

public interface IHitProvider
{
    bool HitByLayer(out RaycastHit hit);
    bool HitAnyLayer(out RaycastHit hit);
}