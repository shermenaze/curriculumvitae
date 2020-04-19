using UnityEngine;

public class HitByRayProvider : MonoBehaviour, IHitProvider
{
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _layerMask;

    private Ray Ray()
    { return _camera.ScreenPointToRay(Input.mousePosition); }

    public bool HitAnyLayer(out RaycastHit hit)
    { return Physics.Raycast(Ray(), out hit); }

    public bool HitByLayer(out RaycastHit hit)
    { return Physics.Raycast(Ray(), out hit, float.MaxValue, _layerMask); }
}