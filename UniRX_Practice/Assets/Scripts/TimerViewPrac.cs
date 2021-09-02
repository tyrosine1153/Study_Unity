using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class TimerViewPrac : MonoBehaviour
{
    [SerializeField] private TimerEventPrac timerEventPrac;

    void Start()
    {
        timerEventPrac.eventTimer.Subscribe(time => Debug.Log($"time : {time}"));
    }
}
