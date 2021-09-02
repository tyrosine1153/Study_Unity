using System;
using System.Collections;
using UniRx;
using UnityEngine;

public class TimerEventPrac : MonoBehaviour
{
    public Subject<int> eventTimer = new Subject<int>();

    void Start()
    {
        StartCoroutine(TimerCoroutine());
    }
    IEnumerator TimerCoroutine()
    {
        int time = 1;
        while (time <= 100)
        {
            eventTimer.OnNext(time++);

            yield return new WaitForSeconds(1);
        }
    }
}
