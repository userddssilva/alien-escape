using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private Rigidbody2D _m_rigidBody2D;

    private BoxCollider2D _m_bottomBoxCollider;

    private Transform _m_transform;

    private bool lastChangingStatus = false;

    // Start is called before the first frame update
    void Start()
    {
        _m_rigidBody2D = GetComponent<Rigidbody2D>();
        _m_bottomBoxCollider = GetComponents<BoxCollider2D>()[1];
        _m_transform = GetComponent<Transform>();
        DisableRigidBody();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.IsGravityChanging() != lastChangingStatus)
        {
            if (GameController.instance.IsGravityChanging())
            {
                Flip(GameController.instance.lastAngle);
                EnableRigidBody();
            }
            lastChangingStatus = GameController.instance.IsGravityChanging();
        }
    }

    private void EnableRigidBody()
    {
        Debug.Log("enable box rigidbody, should fall");
        Flip(GameController.instance.lastAngle);
        _m_rigidBody2D.constraints = RigidbodyConstraints2D.None;
        _m_rigidBody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        _m_rigidBody2D.isKinematic = false;
    }

    private void DisableRigidBody()
    {
        Debug.Log("disable box rigidbody, should be static");
        _m_rigidBody2D.constraints = RigidbodyConstraints2D.FreezePosition;
        _m_rigidBody2D.isKinematic = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (_m_bottomBoxCollider.IsTouching(collision.collider))
        {
            Debug.Log("box collision from the bottom!!");
            DisableRigidBody();
        }
    }

    private void Flip(float angle)
    {
        // Debug.Log("flip");
        Vector3 currentPosition = _m_transform.eulerAngles;
        _m_transform.eulerAngles =
            currentPosition + new Vector3(0f, 0f, -1 * angle);
    }
}
