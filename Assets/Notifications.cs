using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class NotificationSent : ASignal<NotificationType> { }
public class NotificationDone : ASignal { }

public class Notifications : MonoBehaviour
{
    [SerializeField] private Button _approveButton;
    [SerializeField] private TextMeshProUGUI _mainText;
    [SerializeField] private TextMeshProUGUI _buttonText;

    private Dictionary<NotificationType, ANotification> _notifications;
    private IAnimate _animate;

    private void Awake()
    {
        _notifications = new Dictionary<NotificationType, ANotification>
        {
            {
                NotificationType.ClickToContinue,
                new ANotification("Click anywhere to continue.", "I knew that")
            },
            {
                NotificationType.Error,
                new ANotification("An error has occured.", "Ok")
            }
        };
        
        _animate = GetComponent<IAnimate>();
    }

    private void Start()
    {
        Signals.Get<NotificationSent>().AddListener(SendNotification);
        Signals.Get<NotificationDone>().AddListener(NotificationDone);
    }

    public void SendNotification(NotificationType type)
    {
        if (_notifications.TryGetValue(type, out var notification))
        {
            _mainText.text = notification.MainText;
            _buttonText.text = notification.ButtonText;

            _animate.AnimIn();
        }
    }

    public void NotificationDone()
    {
        _animate.AnimOut();
    }
}

internal class ANotification
{
    public string MainText => _mainText;
    public string ButtonText => _buttonText;
    
    private readonly string _mainText;
    private readonly string _buttonText;
    
    public ANotification(string mainText, string buttonText)
    {
        _mainText = mainText;
        _buttonText = buttonText;
    }
}