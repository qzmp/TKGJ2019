using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

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
    private float _smoothTime;

    private Vector3 _velocity;

    void Start()
    {
        Assert.IsNotNull(_legsAnimator);
        Assert.IsNotNull(_legsTransform);
        Assert.IsNotNull(_topTransform);
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

        #region Kamil
        #region Rotation
        worldMousePosition.z = 128;
        _topTransform.LookAt(worldMousePosition, Vector3.back);
        #endregion

        //#region Movement
        //if (movementInput.magnitude > 0)
        //{
        //    Vector3 targetPosition = transform.position + Quaternion.Euler(0, 0, movementAngle) * dirUp * _speed;
        //    Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, _smoothTime);
        //    //transform.position += _velocity;
        //    Debug.Log(_velocity);
        //    transform.position += _velocity * Time.deltaTime;
        //}
        //else
        //{
        //    _velocity = Vector3.zero;
        //}
        //#endregion
        #endregion
    }

    private void FixedUpdate()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        Vector3 dirUp = Vector2.up;
        Vector2 movementInput = GetMovementInput();
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float movementAngle = Vector2.SignedAngle(dirUp, movementInput);

        if (GetMovementInput().magnitude > 0)
        {
            rigidbody.AddForce(Quaternion.Euler(0, 0, movementAngle) * dirUp * _speed, ForceMode2D.Force);
        }
    }
}
