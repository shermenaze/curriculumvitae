using UnityEngine;

public class EventsCanvas : MonoBehaviour
{
    [SerializeField] private GameObject _rayBlocker;
    [SerializeField] private Notifications _notifications;

    private void Start()
    {
        Signals.Get<SpeakAreaEntered>().AddListener(BlockRays);
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

    private void SendNotification(NotificationType type)
    {
        BlockRays();
        _notifications.SendNotification(type);
    }
    
    public void NotificationDone()
    {
        UnblockRays();
        _notifications.NotificationDone();
    }
    
    private void OnDestroy()
    {
        Signals.Get<SpeakAreaEntered>().RemoveListener(BlockRays);
        Signals.Get<TextEndSignal>().RemoveListener(UnblockRays);
    }
}