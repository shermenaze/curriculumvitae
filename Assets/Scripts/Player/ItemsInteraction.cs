using UnityEngine;

public class ItemsInteraction : MonoBehaviour
{
    [SerializeField] private Transform _parent;
    [SerializeField] private LayerMask _layerMask;

    public GameObject ItemInHand { get; private set; }

    #region Fields

    private IHitProvider _hitProvider;
    private IInteractable _interact;
    private IInteractable _previousInteract;

    #endregion

    private void Awake()
    {
        _hitProvider = GetComponent<IHitProvider>();
    }

    void Update()
    {
        Interact();
    }

    /// <summary>
    /// Highlight the object being pointed at and interacted with
    /// </summary>
    private void Interact()
    {
        if (_hitProvider.HitByCustomLayer(_layerMask, out var hit))
        {
            _interact = hit.transform.GetComponent<IInteractable>();

            UnHighlightLastInteract(_interact);


            _interact?.Highlight(true);

            if (Input.GetMouseButtonDown(0))
            {
                _interact?.Interact();
                Pickup(hit);
            }
        }
        else _interact?.Highlight(false);
    }

    /// <summary>
    /// Remove Highlight from last interactable object, in case ray cast was moved from one IInteractable object to the next
    /// </summary>
    /// <param name="interact">The new interactable object</param>
    private void UnHighlightLastInteract(IInteractable interact)
    {
        if (_previousInteract == null)
        {
            _previousInteract = interact;
            return;
        }
        
        if (interact != _previousInteract) _previousInteract.Highlight(false);

        _previousInteract = interact;
    }

    /// <summary>
    /// Pick up the object
    /// </summary>
    /// <param name="hit"></param>
    private void Pickup(RaycastHit hit)
    {
        var pickUp = hit.transform.GetComponent<IPickupable>();

        if (pickUp != null)
        {
            pickUp.PickUp(_parent, out var itemGo);
            ItemInHand = itemGo;
        }
    }
    
    public void DestroyItemInHand()
    {
        Destroy(ItemInHand);
        ItemInHand = null;
    }
}
