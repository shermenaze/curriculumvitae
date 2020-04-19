using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(IHitProvider))]
public class ClickToMove : MonoBehaviour
{
    private NavMeshAgent agent;
    private IHitProvider _hitByRayProvider;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        _hitByRayProvider = GetComponent<IHitProvider>();
    }

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        
        if (Input.GetMouseButton(0))
        {
            if (_hitByRayProvider.HitByLayer(out var hit))
                agent.destination = hit.point;
        }
    }
}