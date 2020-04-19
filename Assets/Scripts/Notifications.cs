using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class NotificationSent : ASignal<NotificationSO> { }

public class NotificationDone : ASignal
{
}

public class Notifications : MonoBehaviour
{
    [SerializeField] private Button _approveButton;
    [SerializeField] private TextMeshProUGUI _mainText;
    [SerializeField] private TextMeshProUGUI _buttonText;

    private IAnimate _animate;
    private NotificationSO _notificationObj;

    private void Awake()
    {
        _animate = GetComponent<IAnimate>();
    }

    private void Start()
    {
        Signals.Get<NotificationDone>().AddListener(NotificationDone);
    }

    public void SetNotification(NotificationSO notificationSo)
    {
        if (notificationSo == null) return;
        
        _mainText.text = notificationSo.MainText;
        _buttonText.text = notificationSo.ButtonText;
        
        _animate.AnimIn();
    }

    public void NotificationDone()
    {
        _animate.AnimOut();
    }
}