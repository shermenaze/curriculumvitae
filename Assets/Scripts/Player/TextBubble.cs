using UnityEngine;


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
        Signals.Get<SpeakAreaEntered>().AddListener(StartText);
        Signals.Get<TextEndSignal>().AddListener(EndText);
    }

    private void StartText(Area area)
    {
        _animate.AnimIn();
    }

    private void EndText()
    {
        _animate.AnimOut();
    }

    private void OnDestroy()
    {
        Signals.Get<SpeakAreaEntered>().RemoveListener(StartText);
    }
}