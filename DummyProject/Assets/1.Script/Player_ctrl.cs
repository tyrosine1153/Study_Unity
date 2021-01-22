using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_ctrl : MonoBehaviour
{    
    public int itemCount = 0;
    private Rigidbody _rigidbody;
    private AudioSource _audioSource;
    private GameManager _gameManager;
    private Vector3 _vecForMove;
    private Vector3 _vecForJump;
    private bool _canJump = false;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
        _gameManager = GetComponent<GameManager>();
        transform.Translate(Vector3.right * 3 * Time.deltaTime);
    }
    
    void FixedUpdate()
    {
        _vecForMove = new Vector3(Input.GetAxisRaw("Horizontal"), 0,Input.GetAxisRaw("Vertical"));
        _vecForJump = new Vector3(0,Input.GetAxisRaw("Jump") * 20,0);
        _rigidbody.AddForce(_vecForMove, ForceMode.Impulse);
        if (_canJump)
        {
            _rigidbody.AddForce(_vecForJump, ForceMode.Impulse);
            _canJump = false;
        }

        if (transform.position.y < -10)
        {
            SceneManager.LoadScene(_gameManager.stage);
        }
    }

    public void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _canJump = true;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Jump"))
        {
            _rigidbody.AddForce(Vector3.up * 5, ForceMode.Impulse);
        }
        
        if (other.gameObject.CompareTag("Item"))
        {
            _audioSource.Play();
            itemCount++;
            _gameManager.GetItem(itemCount);
            other.gameObject.SetActive(false);
        }
        
        if (other.gameObject.CompareTag("Finish"))
        {
            if (itemCount >= _gameManager.goalItemCount)
            {
                SceneManager.LoadScene(_gameManager.stage+1);
            }
            else
            {
                SceneManager.LoadScene(_gameManager.stage);
            }
        }
    }
}
