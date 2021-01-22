using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerCtrl : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;
    public GameManager gameManager;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _renderer;
    private Animator _animator;
    private BoxCollider2D _collider;
    private float _h;
    private int _jumpCount;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        // Jump
        if (Input.GetButtonDown("Jump") && (!_animator.GetBool("isJump") || _jumpCount < 2)) // canJump 변수 역할을 대신 함
        {
            _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            _animator.SetBool("isJump", true);
            _jumpCount++;
        }

        // Stop Speed 키보드에서 손을 떼면 속도가 줄어듬
        if (Input.GetButtonUp("Horizontal"))
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.normalized.x * 0.5f, _rigidbody.velocity.y);

        // Sprite Direction 캐릭터가 보는 방향
        if (Input.GetButton("Horizontal"))
            _renderer.flipX = Input.GetAxisRaw("Horizontal") == -1;

        // 애니메이터 매개변수 isWalk설정
        if (Mathf.Abs(_rigidbody.velocity.x) < 0.3f) // Mathf.Abs 절댓값
            _animator.SetBool("isWalk", false);
        else
            _animator.SetBool("isWalk", true);

        if (transform.position.y < -5f)
        {
            if (gameManager.healthPoint > 1)
            {
                _rigidbody.velocity = Vector2.zero;
                transform.position = new Vector3(-1.5f, 0, 0);
            }
            HealthDown();
        }
    }

    void FixedUpdate()
    {
        // Move Speed
        _h = Input.GetAxisRaw("Horizontal");
        _rigidbody.AddForce(Vector2.right * _h, ForceMode2D.Impulse);

        // Max Speed 양방향 모두 최대 속도보다 커지지 않도록 함
        if (_rigidbody.velocity.x > maxSpeed)
            _rigidbody.velocity = new Vector2(maxSpeed, _rigidbody.velocity.y);
        else if (_rigidbody.velocity.x < maxSpeed * -1)
            _rigidbody.velocity = new Vector2(maxSpeed * -1, _rigidbody.velocity.y);

        // RayCast : 오브젝트 검색을 위해 Ray를 쏘는 방식
        // landing Platform
        if (_rigidbody.velocity.y < 0)
        {
            RaycastHit2D rayHit = Physics2D.Raycast(transform.position, Vector2.down, 1, LayerMask.GetMask("Platform"));
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.5f)
                {
                    _animator.SetBool("isJump", false);
                    _jumpCount = 0;
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 9)
        {
            //Attack
            if (_rigidbody.velocity.y < 0 && transform.position.y > other.transform.position.y)
                OnAttacked(other.transform);
            //Damage
            else{
                OnDamaged(other.transform.position); // 충돌한 위치 전송
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Item
        if (other.gameObject.layer == 12)
        {
            bool isBronze = other.gameObject.name.Contains("1");
            bool isSilver = other.gameObject.name.Contains("2");
            bool isGold = other.gameObject.name.Contains("3");
            if (isBronze)
            {
                gameManager.stagePoint += 50;
            }
            else if (isSilver)
            {
                gameManager.stagePoint += 100;
            }
            else if (isGold)
            {
                gameManager.stagePoint += 150;
            }
            other.gameObject.SetActive(false);
        }
        // Next Stage
        else if (other.gameObject.layer == 13)
        {
            gameManager.ChangeToNextStage();
        }
    }

    void OnDamaged(Vector3 targetPos)
    {
        HealthDown();
        
        // Change layer
        gameObject.layer = 11;
        
        // View alpha
        _renderer.color = new Color(1,1,1,0.4f);
        
        // Reaction Force
        int dir = transform.position.x - targetPos.x > 0 ? 1 : -1; // 장애물의 반대 방향 = 현재 x값 - 충돌위치의 x값 > 0
        _rigidbody.AddForce(new Vector2(dir,1) * 7,ForceMode2D.Impulse);
        
        // Animation
        _animator.SetTrigger("isDamaged");
        
        Invoke(nameof(OffDamaged),3f);
    }

    void OffDamaged()
    {
        gameObject.layer = 10;
        _renderer.color = new Color(1,1,1,1);
    }

    void OnAttacked(Transform enemy)
    {
        //Point
        gameManager.stagePoint += 100;
        // Reaction
        _rigidbody.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        //Enemy Die
        DinoCtrl dinoCtrl = enemy.GetComponent<DinoCtrl>();
        dinoCtrl.OnDamaged();
    }

    void HealthDown()
    {
        if (gameManager.healthPoint > 1)
        {
            gameManager.healthPoint--;
        }
        else
        {
            // Sprite Alpha
            _renderer.color = new Color(0, 0, 0, 0.4f);
            // Sprite Flip Y
            _renderer.flipY = true;
            // Collider
            _collider.enabled = false;
            // Die Effect Jump
            _rigidbody.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

            gameManager.OnDie();
        }
    }
}
