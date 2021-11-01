using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MyText : MonoBehaviour
{
    void Start()
    {
        var rt = GetComponent<RectTransform>();
        rt.DOAnchorPosY(0, 1).SetDelay(1.5f).SetEase(Ease.InCubic);
    }
}
