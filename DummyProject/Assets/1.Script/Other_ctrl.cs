using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Other_ctrl : MonoBehaviour
{
    private MeshRenderer _mesh;
    private Material _material;
    private Rigidbody _rigidbody;
    void Start()
    {
        _mesh = GetComponent<MeshRenderer>();
        _material = _mesh.material;
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player"))
            _material.color = Color.cyan;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Jump"))
        {
            _rigidbody.AddForce(Vector3.up, ForceMode.Impulse);
        }
    }
}
