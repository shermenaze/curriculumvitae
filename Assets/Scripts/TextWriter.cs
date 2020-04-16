using System.Collections;
using TMPro;
using UnityEngine;

public class TextEndSignal : ASignal { }

public class TextWriter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    
    public TextSO TextSo { get; set; }
    
    private int _currentText;

    private void Start()
    {
        Signals.Get<SpeakAreaEntered>().AddListener(WriteText);
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Space)) return;

        if (TextSo && _currentText < TextSo._texts.Length)
        {
            _text.text = string.Empty;
            StartCoroutine(PerCharWriter());
        }
        else
        {
            Signals.Get<TextEndSignal>().Dispatch();
            TextSo = null;
        }
    }

    private void WriteText()
    {
        StartCoroutine(PerCharWriter());
    }

    private IEnumerator PerCharWriter()
    {
        var charList = TextSo._texts[_currentText].ToCharArray();

        foreach (var c in charList)
        {
            _text.text += c;
            yield return new WaitForSeconds(0.05f);
        }
        
        _currentText++;
    }

    private void OnDestroy()
    {
        Signals.Get<SpeakAreaEntered>().RemoveListener(WriteText);
    }
}