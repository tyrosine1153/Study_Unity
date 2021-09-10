using System;
using UniRx;
using UnityEngine;

public class StringErrorPrac : MonoBehaviour
{
    private Subject<string> _subject = new Subject<string>();

    private void Start()
    {
        _subject.Select(str => int.Parse(str))
            .OnErrorRetry((FormatException ex) =>
            {
                Debug.Log("FormatException으로 Stream 재구축");
            })
            .Subscribe(x => Debug.Log($"성공 : {x}"), 
                x => Debug.Log($"실패 : {x}"));  // FormatException일 때, 실패 부분은 실행되지 않음
        
        _subject.OnNext("1");
        _subject.OnNext("2");
        _subject.OnNext("Hello!");
        _subject.OnNext("4");
        _subject.OnNext("5");
    }
}
