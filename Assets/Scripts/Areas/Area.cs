using UnityEngine;

public abstract class Area : MonoBehaviour
{
    [SerializeField] protected TextSO _textSo;
    
    public TextSO TextSo => _textSo;
    
    public abstract void AreaEvent();
}