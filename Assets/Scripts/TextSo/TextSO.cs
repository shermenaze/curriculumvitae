using UnityEngine;

[CreateAssetMenu(menuName = "Texts", fileName = "Speak")]
public class TextSO : ScriptableObject
{
    [SerializeField] private int _eventText;
    public int EventText => _eventText;
    
    [TextArea(2,4)]
    public string[] _texts;
}