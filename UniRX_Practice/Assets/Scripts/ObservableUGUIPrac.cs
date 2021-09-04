using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class ObservableUGUIPrac : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private InputField inputField;
    [SerializeField] private Slider slider;
    [SerializeField] private Text text1;
    [SerializeField] private Text text2;
    
    void Start()
    {
        button.OnClickAsObservable().Subscribe();
        

        inputField.OnValueChangedAsObservable()./*Where(msg => msg == "정답").*/Subscribe(msg => text1.text = msg);
        inputField.OnEndEditAsObservable().Subscribe();

        slider.OnValueChangedAsObservable()./*Where(f => f <= 0.7 && f >= 0.4).*/Subscribe(f=>text2.text = $"{f}");
    }
}
