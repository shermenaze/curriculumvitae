using UnityEngine;

public class MemoryArea : Area
{
    private void Start()
    {
        _startTextSo.AddEvent(AreaEvent);
    }

    public override void AreaEvent()
    {
        Debug.Log($"Area event {name}");
    }
}