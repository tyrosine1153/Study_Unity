using System;
using UniRx;
using UnityEngine;

public class ReactivePropertyPrac : MonoBehaviour
{
    
    private void Start()
    {
        ReactiveProperty<int> rp = new ReactiveProperty<int>();

        rp.Value = 5;

        var curValue = rp.Value;
        Debug.Log($"curValue : {curValue}");

        rp.Subscribe(value => Debug.Log($"rp value changed : {value}"));

        rp.Value = 10;
        
    }
}
