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

    void Start()
    {
        Assert.IsNotNull(_legsAnimator);
        Assert.IsNotNull(_legsTransform);
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
        Vector2 direction = Vector2.up;
        Vector2 movementInput = GetMovementInput();

        _legsAnimator.SetBool("is_walking", movementInput.magnitude > 0);

        float angle = Vector2.SignedAngle(direction, movementInput);

        if (Mathf.Abs(angle) > 90f)
        {
            _legsAnimator.SetFloat("walking_speed", -movementInput.magnitude);
            _legsTransform.localRotation = Quaternion.Euler(0, 0, 180.0f + angle);
        }
        else
        {
            _legsAnimator.SetFloat("walking_speed", movementInput.magnitude);
            _legsTransform.localRotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
