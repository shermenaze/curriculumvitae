using System.Collections;
using TMPro;
using UnityEngine;

public class TextEndSignal : ASignal { }

public class TextWriter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    public TextSO TextSo => _textSo;

    private TextSO _textSo;
    private int _currentText;
    private bool _shouldWrite;
    private const float PerCharWait = 0.05f;

    private void Start()
    {
        Signals.Get<AreaActiveZoneEntered>().AddListener(WriteText);
    }

    private void Update()
    {
        if (!_shouldWrite || !Input.GetKeyDown(KeyCode.Space)) return;
        
        if (TextSo && _currentText < TextSo._texts.Length)
        {
            _text.text = string.Empty;
            StartCoroutine(PerCharWriter());
        }
        else
        {
            _text.text = string.Empty;
            _shouldWrite = false;
            _currentText = 0;
            _textSo = null;
            
            Signals.Get<TextEndSignal>().Dispatch();
        }
    }

    private void WriteText(TextSO textSo)
    {
        _textSo = textSo;
        StartCoroutine(PerCharWriter());
    }

    private IEnumerator PerCharWriter()
    {
        _shouldWrite = false;

        if(_currentText == _textSo.EventTextNumber) _textSo.OnTextEvent?.Invoke();
        
        var charList = TextSo._texts[_currentText].ToCharArray();

        foreach (var c in charList)
        {
            _text.text += c;
            yield return new WaitForSeconds(PerCharWait);
        }

        _shouldWrite = true;
        _currentText++;
    }

    private void OnDisable()
    {
        Signals.Get<AreaActiveZoneEntered>().RemoveListener(WriteText);
    }
}