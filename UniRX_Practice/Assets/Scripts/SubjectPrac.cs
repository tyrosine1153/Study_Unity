using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class SubjectPrac : MonoBehaviour
{
    void Start()
    {
        Subject<string> subject = new Subject<string>();

        subject.Subscribe(msg => Debug.Log($"New Message Subscribe1 : {msg}"));
        subject.Subscribe(msg => Debug.Log($"New Message Subscribe2 : {msg}"));
        subject.Subscribe(msg => Debug.Log($"New Message Subscribe3 : {msg}"));
        
        subject.OnNext("Hello World!");
        subject.OnNext("Hello World~~~~~~~~");

        Subject<int> subject2 = new Subject<int>();

        subject2.Subscribe(msg => Debug.Log($"asdfasdfasf{msg}"));
        
        subject2.OnNext(125);
    }
}
