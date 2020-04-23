using UnityEngine;

public class ItemsInteraction : MonoBehaviour
{
    [SerializeField] private Transform _parent;
    [SerializeField] private LayerMask _layerMask;

    public GameObject ItemInHand { get; private set; }
    
    private IHitProvider _hitProvider;
    private IInteractable _interact;

    private void Awake()
    {
        _hitProvider = GetComponent<IHitProvider>();
    }

    void Update()
    {
        Interact();
    }

    private void Interact()
    {
        if (_hitProvider.HitByCustomLayer(_layerMask, out var hit))
        {
            var pickUp = hit.transform.GetComponent<IPickupable>();
            _interact = hit.transform.GetComponent<IInteractable>();

            _interact?.Highlight(true);

            if (Input.GetMouseButtonDown(0))
            {
                _interact?.Interact();
                //TODO: Should I get an itemSO return type and instantiate it here?
                if (pickUp != null)
                {
                    pickUp.PickUp(_parent, out var itemGo);
                    ItemInHand = itemGo;
                }
            }
        }
        else
        {
            _interact?.Highlight(false);
            _interact = null;
        }
    }

    public void DestroyItemInHand()
    {
        Destroy(ItemInHand);
        ItemInHand = null;
    }
}
