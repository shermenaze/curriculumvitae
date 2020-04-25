using UnityEngine;

public class EventsCanvas : MonoBehaviour
{
    [SerializeField] private GameObject _rayBlocker;
    [SerializeField] private GameObject _DarkeningPanel;

    private void Start()
    {
        Signals.Get<TextReceived>().AddListener(BlockRays);
        Signals.Get<TextEndSignal>().AddListener(UnblockRays);
    }

    private void BlockRays(TextSO textSo)
    {
        _rayBlocker.SetActive(true);
    }

    private void UnblockRays()
    {
        _rayBlocker.SetActive(false);
    }

    private void OnDisable()
    {
        Signals.Get<TextReceived>().RemoveListener(BlockRays);
        Signals.Get<TextEndSignal>().RemoveListener(UnblockRays);
    }
}