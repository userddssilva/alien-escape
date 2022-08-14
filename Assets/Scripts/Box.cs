using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private Rigidbody2D _m_rigidBody2D;
    private Transform _m_transform;

    // Start is called before the first frame update
    void Start()
    {
        RotationManager.OnGravityChange += ApplyRotation;
        _m_rigidBody2D = GetComponent<Rigidbody2D>();
        _m_transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}

    private void ApplyRotation(bool isGravityChanging, float angle)
    {
        if (isGravityChanging)
            DisableRigidBody(angle);
        else
            EnableRigidBody();
    }

    private void EnableRigidBody()
    {
        Debug.Log("enable rigidbody");
        _m_rigidBody2D.constraints = RigidbodyConstraints2D.None;
        _m_rigidBody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        _m_rigidBody2D.isKinematic = false;
    }

    private void DisableRigidBody(float angle)
    {
        Debug.Log("disable rigidbody");
        _m_rigidBody2D.constraints = RigidbodyConstraints2D.FreezePosition;
        _m_rigidBody2D.isKinematic = true;
        //Flip(angle);
    }

    //private void Flip(float angle)
    //{
    //    Vector3 currentPosition = _m_transform.eulerAngles;
    //    _m_transform.eulerAngles = currentPosition + new Vector3(0f, 0f, -1 * angle);
    //}
}
