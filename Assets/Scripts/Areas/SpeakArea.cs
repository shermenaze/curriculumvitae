using System;
using UnityEngine;

public class SpeakAreaEntered : ASignal<Area> { }

public class SpeakArea : MonoBehaviour
{
    [SerializeField] private Area _area;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) Signals.Get<SpeakAreaEntered>().Dispatch(_area);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) Destroy(gameObject);
    }
}