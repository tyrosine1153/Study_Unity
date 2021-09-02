using System;
using System.Collections;
using UniRx;
using UnityEngine;

public class CoroutineTimerPrac : MonoBehaviour
{
    private void Start()
    {

        Observable.FromCoroutine<int>(observer => CoTimer(observer, 60))
            .Subscribe(t => Debug.Log($"CountDown : {t}"));
    }

    IEnumerator CoTimer(IObserver<int> observer, int countTime)
    {
        int count = countTime;

        while (count > 0)
        {
            observer.OnNext(count--);

            yield return new WaitForSeconds(1);
        }

        observer.OnNext(count);
        observer.OnCompleted();
    }
}
