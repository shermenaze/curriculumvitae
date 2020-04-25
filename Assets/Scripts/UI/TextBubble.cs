﻿using UnityEngine;


public class TextBubble : MonoBehaviour
{
    private TextWriter _textWriter;
    private IAnimate _animate;

    private void Awake()
    {
        _textWriter = GetComponent<TextWriter>();
        _animate = GetComponent<IAnimate>();
    }

    private void Start()
    {
        Signals.Get<TextReceived>().AddListener(StartText);
        Signals.Get<TextEndSignal>().AddListener(EndText);
    }

    private void StartText(TextSO textSo)
    {
        _animate.AnimIn();
    }

    private void EndText()
    {
        _animate.AnimOut();
    }

    private void OnDisable()
    {
        Signals.Get<TextReceived>().RemoveListener(StartText);
    }
}