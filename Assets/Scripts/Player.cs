using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Slider _manaBar;

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

    //[SerializeField]
    public Action UpdateMana;
    [SerializeField]
    private float _mana;
    public float Mana
    {
        set
        {
            _mana = Mathf.Clamp(value, 0.0f, 1.0f);
            if(_manaBar != null)
                _manaBar.value = _mana;
        }
        get
        {
            return _mana;
        }
    }

    [SerializeField]
    private float _manaRegeneration;

    private Vector3 _velocity;
    private bool _doDash;

    public bool canDash = false;

    private Rigidbody2D _rigidbody;
    public static bool IsAlive = true;

    [SerializeField]
    private PlayerAudioController _playerAudioController;

    void Start()
    {
        Assert.IsNotNull(_legsAnimator);
        Assert.IsNotNull(_legsTransform);
        Assert.IsNotNull(_topTransform);
        _rigidbody = GetComponent<Rigidbody2D>();
        _lastDashTime = -_dashCooldown;
        if (this.canDash)
        {
            AbilityDisplayController.Instance.ShowDashDisplay();
        }
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
        if (IsAlive)
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

            if (Input.GetKeyDown(KeyCode.Space) && this.canDash)
            {
                _doDash = true;
            }
            _mana = Mathf.Clamp(_mana + _manaRegeneration * Time.deltaTime, 0.0f, 1.0f);
        }
        Mana = Mathf.Clamp(Mana + _manaRegeneration * Time.deltaTime, 0.0f, 1.0f);
    }

    private void FixedUpdate()
    {
        if (IsAlive)
        {
            Vector3 dirUp = Vector2.up;
            Vector2 movementInput = GetMovementInput();
            Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float movementAngle = Vector2.SignedAngle(dirUp, movementInput);
            _playerAudioController.SetWalkAudio(GetMovementInput());

            try
            {
                _playerAudioController.SetWalkAudio(GetMovementInput());
            }
            catch { }

            if (GetMovementInput().magnitude > 0)
            {
                if (_dashCooldown > 0) //Wariant: cooldown dasha
                {
                    AbilityDisplayController.Instance.SetDashDisplay((Time.time - _lastDashTime) / _dashCooldown);

                    if (_doDash && Time.time < _lastDashTime + _dashCooldown)
                    {
                        _doDash = false;
                    }
                    else if (_doDash)
                    {
                        _lastDashTime = Time.time;
                        AbilityDisplayController.Instance.ActivateDashDisplay();
                        Debug.Log(_lastDashTime);
                    }
                }//if (_dashCooldown > 0) 

                if (_dashCost > 0) //Wariant: mana
                {
                    if (_doDash && Mana < _dashCost)
                    {
                        _doDash = false;
                    }
                    else if (_doDash)
                    {
                        Mana -= _dashCost;
                    }
                }

                this._rigidbody.AddForce(Quaternion.Euler(0, 0, movementAngle) * dirUp * (_doDash ? _dashSpeed : _speed), (_doDash ? ForceMode2D.Impulse : ForceMode2D.Force));
                _doDash = false;

                AbilityDisplayController.Instance.ActivateDashDisplay();
            }//if (GetMovementInput().magnitude > 0)
        }
    }

    public void TeachDash()
    {
        this.canDash = true;
        AbilityDisplayController.Instance.ShowDashDisplay();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            IsAlive = false;
            _playerAudioController.PlayDeathAudioSource();
            _legsTransform.gameObject.SetActive(false);
            _topTransform.gameObject.SetActive(false);
        }
    }
}
