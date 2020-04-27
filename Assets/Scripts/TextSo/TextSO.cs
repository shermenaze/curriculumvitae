using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Texts", fileName = "Text")]
public class TextSO : ScriptableObject
{
    [SerializeField] private int[] _eventTextNumbers;

    [TextArea(2, 4)] public string[] _texts;

    private readonly Dictionary<int, Action> _actions = new Dictionary<int, Action>();
    private readonly Queue<int> _numbersQueue = new Queue<int>();

    private void OnEnable()
    {
        foreach (var textNumber in _eventTextNumbers)
            _numbersQueue.Enqueue(textNumber);
    }

    public void AddEvent(Action action)
    {
        CheckEventsCapacity();

        if (!_actions.ContainsKey(_numbersQueue.Peek()))
        {
            var number = _numbersQueue.Dequeue();
            _actions.Add(number, action);
            _numbersQueue.Enqueue(number);
        }
    }

    private void CheckEventsCapacity()
    {
        if (_numbersQueue.Count <= 0) Debug.LogError($"No more space for extra events in this {name}");

        if (_actions.ContainsKey(_eventTextNumbers.Last()))
            Debug.LogError($"No more space for extra events in this {name}");//TODO: Remove
    }

    public void FireEvent(int textEvent)
    {
        if (_numbersQueue.Count <= 0 || _numbersQueue.Peek() != textEvent) return;

        if (_actions.ContainsKey(_numbersQueue.Peek()))
            _actions[_numbersQueue.Dequeue()]?.Invoke();
    }
}