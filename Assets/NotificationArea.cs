using UnityEngine;

public class NotificationArea : MonoBehaviour
{
    [SerializeField] private NotificationType type;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) Signals.Get<NotificationSent>().Dispatch(type);
    }
}
