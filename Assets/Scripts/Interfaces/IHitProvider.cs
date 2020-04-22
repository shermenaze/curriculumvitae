using UnityEngine;

public interface IHitProvider
{
    Camera CurrentCamera { get; }
    
    bool HitByPreDefinedLayer(out RaycastHit hit);
    
    bool HitAnyLayer(out RaycastHit hit);

    bool HitByCustomLayer(LayerMask layerMask, out RaycastHit hit);
}