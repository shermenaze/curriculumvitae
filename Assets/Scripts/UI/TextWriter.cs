using System.Collections;
using TMPro;
using UnityEngine;

public class TextEndSignal : ASignal { }

public class TextWriter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    
    private TextSO _textSo;
    private int _currentText;
    private bool _shouldWrite;
    private float _timer;
    private const float PerCharWait = 0.04f;

    private void Start()
    {
        Signals.Get<TextReceived>().AddListener(WriteText);
    }

    private void Update()
    {
        if (!_textSo) return;

        WritingPermissions();
    }

    /// <summary>
    /// Decides if the current paragraph needs to be removed and start the new one 
    /// </summary>
    private void WritingPermissions()
    {
        _timer += Time.deltaTime;
        bool input = Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0);

        if (_shouldWrite && (input || _timer >= 3))
        {
            _timer = 0;

            if (_currentText < _textSo._texts.Length)
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

    /// <summary>
    /// Accepts a TextSO obj, which starts the text writer system
    /// </summary>
    /// <param name="textSo"></param>
    private void WriteText(TextSO textSo)
    {
        _timer = 0;
        _textSo = textSo;
        StartCoroutine(PerCharWriter());
    }

    /// <summary>
    /// Adds each char into the text field with a short delay between each
    /// </summary>
    /// <returns></returns>
    private IEnumerator PerCharWriter()
    {
        _shouldWrite = false;

        _textSo.FireEvent(_currentText);
        
        var charList = _textSo._texts[_currentText].ToCharArray();

        foreach (var chr in charList)
        {
            _text.text += chr;
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