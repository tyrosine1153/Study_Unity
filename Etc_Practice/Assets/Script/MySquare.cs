using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MySquare : MonoBehaviour
{
    void Start()
    {
        transform.DOMove(Vector3.right, 5);
        transform.DOScale(Vector3.one * 3, 5);
        transform.DORotate(Vector3.forward, 5);

        var mat = GetComponent<SpriteRenderer>().material;
        mat.DOColor(Color.cyan, 5);

    }
}
