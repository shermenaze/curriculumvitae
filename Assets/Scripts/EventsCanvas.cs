using UnityEngine;

public class EventsCanvas : MonoBehaviour
{
    [SerializeField] private GameObject _rayBlocker;
    [SerializeField] private GameObject _DarkeningPanel;
    [SerializeField] private Notifications _notifications;

    private void Start()
    {
        Signals.Get<AreaActiveZoneEntered>().AddListener(StartText);
        Signals.Get<TextEndSignal>().AddListener(UnblockRays);
        
        Signals.Get<NotificationSent>().AddListener(SendNotification);
        Signals.Get<NotificationDone>().AddListener(NotificationDone);
    }

    private void BlockRays()
    {
        _rayBlocker.SetActive(true);
    }

    private void UnblockRays()
    {
        _rayBlocker.SetActive(false);
    }

    private void StartText(Area area)
    {
        BlockRays();
    }
    
    private void SendNotification(NotificationSO notificationSo)
    {
        BlockRays();
        _DarkeningPanel.SetActive(true);
        _notifications.SetNotification(notificationSo);
    }
    
    public void NotificationDone()
    {
        UnblockRays();
        _DarkeningPanel.SetActive(false);
        _notifications.NotificationDone();
    }
    
    private void OnDestroy()
    {
        Signals.Get<AreaActiveZoneEntered>().RemoveListener(StartText);
        Signals.Get<TextEndSignal>().RemoveListener(UnblockRays);
    }
}