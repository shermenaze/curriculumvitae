using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    private MeshRenderer _meshRenderer;
    private readonly Color _selectedColor = Color.green;
    private readonly Color _naturalColor = Color.black;

    private static readonly int RimColor = Shader.PropertyToID("_RimColor");

    private void Awake()
    {
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
        if(_meshRenderer) _meshRenderer.material.SetColor(RimColor, _naturalColor);
    }

    public virtual void Interact()
    {
        Debug.Log($"No interaction added{name}");
    }

    public void Highlight(bool shouldHighlight)
    {
        if (!_meshRenderer) return;
        
        //TODO: Don't change color if color is set to the correct one.
        _meshRenderer.material.SetColor(RimColor,
            shouldHighlight ? _selectedColor : _naturalColor);
    }
}