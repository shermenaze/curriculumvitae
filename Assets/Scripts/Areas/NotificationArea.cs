using UnityEngine;

public class NotificationArea : MonoBehaviour
{
    [SerializeField] private NotificationSO _notificationSo;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) Signals.Get<NotificationSent>().Dispatch(_notificationSo);
    }
}
