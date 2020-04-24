using UnityEngine;

public class TextReceived : ASignal<TextSO> { }

public class ActivateAreaZone : MonoBehaviour
{
    [SerializeField] private TextSO _textSo;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Signals.Get<TextReceived>().Dispatch(_textSo);
            GetComponent<Collider>().enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) Destroy(gameObject);
    }
}