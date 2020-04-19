using System;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent _navMesh;

    private void Awake()
    {
        _navMesh = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        Signals.Get<SpeakAreaEntered>().AddListener(StopMoving);
        Signals.Get<TextEndSignal>().AddListener(StartMoving);
    }

    private void StartMoving()
    {
        _navMesh.isStopped = false;
    }

    private void StopMoving(Area obj)
    {
        _navMesh.isStopped = true;
    }

    public void UnParent()
    {
        transform.SetParent(null, true);
    }

    public void ParentTo(Transform newParent = null, bool worldPositionStays = true)
    {
        transform.SetParent(newParent, worldPositionStays);
    }
}