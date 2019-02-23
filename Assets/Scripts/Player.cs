using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Animator _legsAnimator;

    [SerializeField]
    private Transform _legsTransform;

    [SerializeField]
    private Transform _topTransform;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _dashSpeed;

    [SerializeField]
    private float _dashCooldown;
    private float _lastDashTime;

    [SerializeField]
    private float _dashCost;

    [SerializeField]
    public float mana;

    [SerializeField]
    private float _manaRegeneration;

    private Vector3 _velocity;
    private bool _doDash;

    public Action UpdateMana;

    [SerializeField] private PlayerAudioController _playerAudioController;
    void Start()
    {
        Assert.IsNotNull(_legsAnimator);
        Assert.IsNotNull(_legsTransform);
        Assert.IsNotNull(_topTransform);

        _lastDashTime = -_dashCooldown;
    }

    Vector2 GetDirection()
    {
        return Vector2.zero;
    }

    Vector2 GetMovementInput()
    {
        return new Vector2(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical"));
    }

    void Update()
    {
        Vector3 dirUp = Vector2.up;
        Vector2 movementInput = GetMovementInput();
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float movementAngle = Vector2.SignedAngle(dirUp, movementInput);

        _legsAnimator.SetBool("is_walking", movementInput.magnitude > 0);

        if (Mathf.Abs(movementAngle) > 90f)
        {
            _legsAnimator.SetFloat("walking_speed", -movementInput.magnitude);
            _legsTransform.localRotation = Quaternion.Euler(0, 0, 180.0f + movementAngle);
        }
        else
        {
            _legsAnimator.SetFloat("walking_speed", movementInput.magnitude);
            _legsTransform.localRotation = Quaternion.Euler(0, 0, movementAngle);
        }

        worldMousePosition.z = 128;
        _topTransform.LookAt(worldMousePosition, Vector3.back);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _doDash = true;
        }

        mana = Mathf.Clamp(mana + _manaRegeneration * Time.deltaTime, 0.0f, 1.0f);
    }

    private void FixedUpdate()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        Vector3 dirUp = Vector2.up;
        Vector2 movementInput = GetMovementInput();
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float movementAngle = Vector2.SignedAngle(dirUp, movementInput);
        _playerAudioController.SetWalkAudio(GetMovementInput());
        if (GetMovementInput().magnitude > 0)
        {
            if (_dashCooldown > 0) //Wariant: cooldown dasha
            {
                if (_doDash && Time.time < _lastDashTime + _dashCooldown)
                {
                    _doDash = false;
                }
                else if (_doDash)
                {
                    _lastDashTime = Time.time;
                    Debug.Log(_lastDashTime);
                }
            }

            if(_dashCost > 0) //Wariant: mana
            {
                if (_doDash && mana < _dashCost)
                    _doDash = false;
                else if (_doDash)
                    mana -= _dashCost;
            }

            rigidbody.AddForce(Quaternion.Euler(0, 0, movementAngle) * dirUp * (_doDash ? _dashSpeed : _speed), (_doDash ? ForceMode2D.Impulse : ForceMode2D.Force));
            _doDash = false;
        }
    }
}
