using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class TimelinesManager : MonoBehaviour
{
    [SerializeField] private PlayableDirector _timeline;

    public void StartNextTimeline()
    {
        _timeline.Play();
    }
}
