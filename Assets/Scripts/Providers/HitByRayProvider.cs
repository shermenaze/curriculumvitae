using UnityEngine;

public class HitByRayProvider : MonoBehaviour, IHitProvider
{
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _layerMask;

    public Camera CurrentCamera => _camera;
    
    private Ray Ray()
    { return _camera.ScreenPointToRay(Input.mousePosition); }

    public bool HitAnyLayer(out RaycastHit hit)
    { return Physics.Raycast(Ray(), out hit); }

    public bool HitByPreDefinedLayer(out RaycastHit hit)
    { return Physics.Raycast(Ray(), out hit, float.MaxValue, _layerMask); }

    public bool HitByCustomLayer(LayerMask layerMask, out RaycastHit hit)
    { return Physics.Raycast(Ray(), out hit, float.MaxValue, layerMask); }
}