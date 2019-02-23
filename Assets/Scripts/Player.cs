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




    //[SerializeField]
    public Action UpdateMana;
    [SerializeField]
    private float _mana;
    public float Mana
    {
        set
        {
            _mana = Mathf.Clamp(value, 0.0f, 1.0f);
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


    private Rigidbody2D _rigidbody;
    public static bool IsAlive = true;

    [SerializeField]
    private PlayerAudioController _playerAudioController;

    void Start()
    {
        Assert.IsNotNull(_legsAnimator);
        Assert.IsNotNull(_legsTransform);
        Assert.IsNotNull(_topTransform);
        this._rigidbody = GetComponent<Rigidbody2D>();
    }

    Vector2 GetDirection()
    {
        return Vector2.zero;
    }

    public Vector2 GetMovementInput()
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
                this._rigidbody.AddForce(Quaternion.Euler(0, 0, movementAngle) * dirUp * _speed, ForceMode2D.Force);
            }
        }
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
