using UnityEngine;

public class SpeakAreaEntered : ASignal { }

public class SpeakArea : MonoBehaviour
{
    [SerializeField] private TextSO _textObject;

    public TextSO TextSo => _textObject;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var textBubble = other.GetComponentInChildren<TextBubble>();
            
            if (textBubble)
            {
                textBubble.SetTextSo(_textObject);
                Signals.Get<SpeakAreaEntered>().Dispatch();
            }
        }
    }
}
