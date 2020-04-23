using UnityEngine;

public class AreaActiveZoneEntered : ASignal<Area> { }

public class ActivateAreaZone : MonoBehaviour
{
    [SerializeField] private Area _area;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Signals.Get<AreaActiveZoneEntered>().Dispatch(_area);
            GetComponent<Collider>().enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) Destroy(gameObject);
    }
}