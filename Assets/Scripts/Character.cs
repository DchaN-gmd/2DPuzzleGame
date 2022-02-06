using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;

    private PlayerState _playerState;
    
    private float _inertiaOfMove;

    private void Awake()
    {
        _animator = gameObject.GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();

        _playerState = PlayerState.Idle;
    }

    void Update()
    {
        _inertiaOfMove = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        ChangeState();

        if (Input.GetKey(KeyCode.Space) && _playerState != PlayerState.Jump)
        {
            Jump();
        }
    }

    private void MoveLeft()
    {
        _rigidbody2D.velocity = new Vector2(_speed * _inertiaOfMove * Time.deltaTime, _rigidbody2D.velocity.y);
    }

    private void MoveRight()
    {
        _rigidbody2D.velocity = new Vector2(_speed * _inertiaOfMove * Time.deltaTime, _rigidbody2D.velocity.y);
    } 
    
    private void Idle()
    {
        _playerState = PlayerState.Idle;
        _animator.Play("Idle");
    }

    private void Jump()
    {
        if (_playerState != PlayerState.Jump)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpForce * Time.deltaTime);
            _playerState = PlayerState.Jump;
            _animator.Play("Jump");
        }
    }

    private void ChangeState()
    {
        if(_playerState == PlayerState.Jump)
        {
            if(_rigidbody2D.velocity == Vector2.zero)
            {
                Idle();
            }
        }

        if (_inertiaOfMove == 0 && _playerState == PlayerState.Walk)
        {
            Idle();
        }

        if (_inertiaOfMove > 0 && _playerState != PlayerState.Jump)
        {
            _playerState = PlayerState.Walk;
            _spriteRenderer.flipX = false;
            MoveRight();
            _animator.Play("Walk");
        }

        if (_inertiaOfMove < 0 && _playerState != PlayerState.Jump)
        {
            _playerState = PlayerState.Walk;
            _spriteRenderer.flipX = true;
            MoveLeft();
            _animator.Play("Walk");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlatformController platformController) && _playerState == PlayerState.Jump)
        {
            _rigidbody2D.velocity = Vector2.zero;
            Idle();
        }
    }

    private enum PlayerState
    {
        Idle,
        Walk,
        Jump
    }
}
