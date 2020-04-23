using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Texts", fileName = "Text")]
public class TextSO : ScriptableObject
{
    [SerializeField] private int _eventTextNumber;

    public int EventTextNumber => _eventTextNumber;

    [TextArea(2,4)]
    public string[] _texts;

    public Action OnTextEvent;
}