using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _damping;
    [SerializeField] private Vector2 _offset;
    
    [SerializeField]private ChangeCharacter _changeCharacter;
    private Transform _player;
    private Vector3 _cameraTargetPosition;

    private bool _isLeft;
    private float _playerDirection;

    private void Start()
    {
        _changeCharacter = _changeCharacter.GetComponent<ChangeCharacter>();
        _offset = new Vector3(Mathf.Abs(_offset.x), _offset.y);
    }

    
    void Update()
    {
        _playerDirection = Input.GetAxisRaw("Horizontal");

        if (_playerDirection < 0) _isLeft = true;
        if (_playerDirection > 0) _isLeft = false;

        if (!_isLeft)
        {
            _cameraTargetPosition = new Vector3(_player.position.x + _offset.x, _player.position.y + _offset.y, transform.position.z);
        }
        else
        {
            _cameraTargetPosition = new Vector3(_player.position.x - _offset.x, _player.position.y + _offset.y, transform.position.z);
        }
        transform.position = Vector3.Lerp(transform.position, _cameraTargetPosition, _damping * Time.deltaTime );
    }

    private void OnEnable()
    {
        _changeCharacter.characterChanged += FindActiveCharacter;
    }

    private void OnDisable()
    {
        _changeCharacter.characterChanged -= FindActiveCharacter;
    }

    private void FindActiveCharacter(Character character)
    {
        _player = character.transform;
        _isLeft = false;
    }


}
