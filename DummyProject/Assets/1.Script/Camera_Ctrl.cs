using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Camera_Ctrl : MonoBehaviour
{
    public GameObject objectToFollow;
    public float moveSpeed = 3.0f;
    private Vector3 _thisPosition;
    private Transform _objTransform;
    private Vector3 _objPosition;
    private Vector3 vec;

    void Start()
    {
        _objTransform = objectToFollow.GetComponent<Transform>();
    }

    void Update()
    {
        _thisPosition = transform.position;
        _objPosition = _objTransform.position;
        
        vec = new Vector3(_objPosition.x, _thisPosition.y, _objPosition.z - 4f);
        transform.position = Vector3.Lerp(_thisPosition, vec, moveSpeed * Time.deltaTime);
    }
}
