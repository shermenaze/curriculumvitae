using UnityEngine;

public class MemoryButton : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _iconSpriteRenderer;

    public SpriteRenderer Renderer => _iconSpriteRenderer;
}