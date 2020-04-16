using UnityEngine;

[CreateAssetMenu(menuName = "Texts", fileName = "Speak")]
public class TextSO : ScriptableObject
{
    [TextArea(2,4)]
    public string[] _texts;
}