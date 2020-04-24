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
    private float _timer;
    private const float PerCharWait = 0.05f;

    private void Start()
    {
        Signals.Get<TextReceived>().AddListener(WriteText);
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        bool input = Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0);
        
        if (_shouldWrite && input || _timer >= 3)
        {
            _timer = 0;

            if (!TextSo) return;
            if (_currentText < TextSo._texts.Length)
            {
                _text.text = string.Empty;
                StartCoroutine(PerCharWriter());
            }
            else
            {
                _text.text = string.Empty;
                _shouldWrite = false;
                _currentText = 0;
                _timer = 0;
                _textSo = null;

                Signals.Get<TextEndSignal>().Dispatch();
            }
        }
    }

    private void WriteText(TextSO textSo)
    {
        _timer = 0;
        _textSo = textSo;
        StartCoroutine(PerCharWriter());
    }

    private IEnumerator PerCharWriter()
    {
        _shouldWrite = false;

        _textSo.FireEvent(_currentText);
        
        var charList = TextSo._texts[_currentText].ToCharArray();

        foreach (var c in charList)
        {
            _text.text += c;
            yield return new WaitForSeconds(PerCharWait);
        }

        _shouldWrite = true;
        _currentText++;
        _timer = 0;
    }

    private void OnDisable()
    {
        Signals.Get<TextReceived>().RemoveListener(WriteText);
    }
}