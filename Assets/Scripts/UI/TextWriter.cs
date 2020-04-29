using System.Collections;
using TMPro;
using UnityEngine;

public class TextEndSignal : ASignal { }

public class TextWriter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    public TextSO TextSo => _textSo;

    #region Fields

    private TextSO _textSo;
    private int _currentText;
    private bool _shouldWrite;
    private float _timer;
    private const float PerCharWait = 0.05f;

    #endregion

    private void Start()
    {
        Signals.Get<TextReceived>().AddListener(WriteText);
    }

    private void Update()
    {
        WriteTextRestrictions();
    }

    /// <summary>
    /// Decide if the next text should be written
    /// </summary>
    private void WriteTextRestrictions()
    {
        _timer += Time.deltaTime;
        bool input = Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0);

        if (_shouldWrite && (input || _timer >= 3))
        {
            _shouldWrite = false;
            _timer = 0;

            if (!TextSo) return;
            if (_currentText < TextSo._texts.Length)
            {
                StopCoroutine(PerCharWriter());//Disgusting, move to Async
                _text.text = string.Empty;
                StartCoroutine(PerCharWriter());
            }
            else
            {
                _shouldWrite = false;
                
                _text.text = string.Empty;
                _currentText = 0;
                _timer = 0;
                _textSo = null;

                Signals.Get<TextEndSignal>().Dispatch();
            }
        }
    }

    /// <summary>
    /// The function to get called when a TextReceived event is raised
    /// </summary>
    /// <param name="textSo"></param>
    private void WriteText(TextSO textSo)
    {
        _timer = 0;
        _textSo = textSo;
        StartCoroutine(PerCharWriter());
    }

    /// <summary>
    /// Writes each char with a small delay
    /// </summary>
    /// <returns></returns>
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

        _currentText++;
        _timer = 0;
        
        _shouldWrite = true;
    }

    private void OnDisable()
    {
        Signals.Get<TextReceived>().RemoveListener(WriteText);
    }
}