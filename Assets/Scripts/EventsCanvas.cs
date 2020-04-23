using UnityEngine;

public class EventsCanvas : MonoBehaviour
{
    [SerializeField] private GameObject _rayBlocker;
    [SerializeField] private GameObject _DarkeningPanel;
    [SerializeField] private Notifications _notifications;

    private void Start()
    {
        Signals.Get<AreaActiveZoneEntered>().AddListener(BlockRays);
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

    public void NotificationDone()
    {
        UnblockRays();
        _DarkeningPanel.SetActive(false);
        _notifications.NotificationDone();
    }
    
    private void OnDisable()
    {
        Signals.Get<AreaActiveZoneEntered>().RemoveListener(BlockRays);
        Signals.Get<TextEndSignal>().RemoveListener(UnblockRays);
    }
}