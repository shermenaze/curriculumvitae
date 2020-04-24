using UnityEngine;

public abstract class Area : MonoBehaviour
{
    [SerializeField] protected TextSO _startTextSo;
    
    public TextSO StartTextSo => _startTextSo;
    
    public abstract void AreaEvent();
}