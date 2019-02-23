using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class _Player : MonoBehaviour
{
    [SerializeField]
    private Animator _legsAnimator;

    [SerializeField]
    private Transform _legsTransform;

    [SerializeField]
    private Transform _topTransform;

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

        #region Kamil
        Vector3 flatMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(flatMousePos + " | " + _topTransform.position);
        flatMousePos.z = 128;
        _topTransform.LookAt(flatMousePos, Vector3.back);
        #endregion
    }
}
