using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    private float _speed = 2f;
    private Animator animator;
    private float animationInterpolation = 1f;
    private float animationZeroX = 0;
    private float animationZeroY = 0;
    public float currentSpeed;
    private Rigidbody rig;
    private Vector3 movingVector;
    public Transform target;

    private void Start()
    {
        rig = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {          
              Walk();
        }
        else
        {
            animationZeroX = Mathf.Lerp(animationZeroX, Input.GetAxis("Horizontal"), Time.deltaTime * 7);
            animationZeroY = Mathf.Lerp(animationZeroY, Input.GetAxis("Vertical"), Time.deltaTime * 7);
            animator.SetFloat("x", animationZeroX);
            animator.SetFloat("y", animationZeroY);
        }

        transform.LookAt(target);
        Vector3 camF = Camera.main.transform.forward;
        Vector3 camR = Camera.main.transform.right;

        camF.y = 0;
        camR.y = 0;
        movingVector = Vector3.ClampMagnitude(camF.normalized * Input.GetAxis("Vertical") * currentSpeed + camR.normalized * Input.GetAxis("Horizontal") * currentSpeed, currentSpeed);

        rig.velocity = new Vector3(movingVector.x, rig.velocity.y, movingVector.z);
        rig.angularVelocity = Vector3.zero;
    }
    void Walk()
    {       
        animationInterpolation = Mathf.Lerp(animationInterpolation, 1f, Time.deltaTime * 3);
        animator.SetFloat("x", Input.GetAxis("Horizontal") * animationInterpolation);
        animator.SetFloat("y", Input.GetAxis("Vertical") * animationInterpolation);

        currentSpeed = Mathf.Lerp(currentSpeed, _speed, Time.deltaTime * 3);
    }
}
