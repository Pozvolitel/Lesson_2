using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    private float _speed = 2f;
    private Animator _animator;
    private float _animationInterpolation = 1f;
    private float _animationZeroX = 0;
    private float _animationZeroY = 0;
    public float CurrentSpeed;
    private Rigidbody _rig;
    private Vector3 _movingVector;
    public Transform Target;

    private void Start()
    {
        _rig = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {          
              Walk();
        }
        else
        {
            _animationZeroX = Mathf.Lerp(_animationZeroX, Input.GetAxis("Horizontal"), Time.deltaTime * 7);
            _animationZeroY = Mathf.Lerp(_animationZeroY, Input.GetAxis("Vertical"), Time.deltaTime * 7);
            _animator.SetFloat("x", _animationZeroX);
            _animator.SetFloat("y", _animationZeroY);
        }

        transform.LookAt(Target);
        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;

        camForward.y = 0;
        camRight.y = 0;
        _movingVector = Vector3.ClampMagnitude(camForward.normalized * Input.GetAxis("Vertical") * CurrentSpeed + camRight.normalized * Input.GetAxis("Horizontal") * CurrentSpeed, CurrentSpeed);

        _rig.velocity = new Vector3(_movingVector.x, _rig.velocity.y, _movingVector.z);
        _rig.angularVelocity = Vector3.zero;
    }
    void Walk()
    {       
        _animationInterpolation = Mathf.Lerp(_animationInterpolation, 1f, Time.deltaTime * 3);
        _animator.SetFloat("x", Input.GetAxis("Horizontal") * _animationInterpolation);
        _animator.SetFloat("y", Input.GetAxis("Vertical") * _animationInterpolation);

        CurrentSpeed = Mathf.Lerp(CurrentSpeed, _speed, Time.deltaTime * 3);
    }
}
