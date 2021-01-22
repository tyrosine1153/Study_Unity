using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DinoCtrl : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Vector2 _frontVec;
    private Animator _animator;
    private SpriteRenderer _renderer;
    private float _nextThinkTime;
    private BoxCollider2D _collider; 
    [SerializeField] 
    private int _nextMove;
    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<BoxCollider2D>();
        Invoke(nameof(Think),5);
    }

    void FixedUpdate()
    {
        // Move
        _rigidbody.velocity = new Vector2(_nextMove, _rigidbody.velocity.y);
        
        // Platform Check 바닥 확인
        _frontVec = new Vector2(transform.position.x + _nextMove * 0.5f, transform.position.y);
        Debug.DrawRay(_frontVec,Vector3.down);
        RaycastHit2D rayHit = Physics2D.Raycast(_frontVec, Vector2.down, 2, LayerMask.GetMask("Platform"));
        if (rayHit.collider == null)
        {
            Turn();
        }
    }

    void Think()
    {
        // 방향 설정
        _nextMove = Random.Range(-1, 2);

        // 애니메이터 매개변수 설정
        _animator.SetInteger("walkSpeed", _nextMove);

        // Sprite Direction
        if (_nextMove != 0)
            _renderer.flipX = _nextMove == 1;

        // 다음 호출
        _nextThinkTime = Random.Range(2f, 5f);
        Invoke(nameof(Think), _nextThinkTime);
    }

    void Turn()
    {
        // 이동, 애니메이션 방향 설정
        _nextMove *= -1;
        _renderer.flipX = !_renderer.flipX;
        
        CancelInvoke();
        Invoke(nameof(Think),5);
    }

    public void OnDamaged()
    {
        // Sprite Alpha
        _renderer.color = new Color(0, 0, 0, 0.4f);
        // Sprite Flip Y
        _renderer.flipY = true;
        // Collider
        _collider.isTrigger = true;
        // Die Effect Jump
        _rigidbody.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        // Destroy
        Invoke(nameof(DeActive), 3f);
    }

    private void DeActive()
    {
        gameObject.SetActive(false); 
    }
}
