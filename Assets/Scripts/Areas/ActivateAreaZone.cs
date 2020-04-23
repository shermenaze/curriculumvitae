using UnityEngine;

public class AreaActiveZoneEntered : ASignal<TextSO> { }

public class ActivateAreaZone : MonoBehaviour
{
    [SerializeField] private TextSO _textSo;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Signals.Get<AreaActiveZoneEntered>().Dispatch(_textSo);
            GetComponent<Collider>().enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) Destroy(gameObject);
    }
}