using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    public float Speed;
    public CapsuleCollider2D m_playerFeet;
    public TilemapCollider2D m_scenery;

    private bool _isStopPlayer = false;
    private Rigidbody2D _m_rigidBody2D;
    private Transform _m_transform;

    void Start()
    {
        RotationManager.OnGravityChange += ApplyRotation;
        _m_rigidBody2D = GetComponent<Rigidbody2D>();
        _m_transform = GetComponent<Transform>();
    }

    void Update()
    {
        Move();
    }

    private void ApplyRotation(bool isGravityChanging, float angle)
    {
        if (isGravityChanging)
        {
            DisableRigidBody(angle);
        }
        else
        {
            EnableRigidBody();
        }
    }

    private void EnableRigidBody()
    {
        Debug.Log("enable rigidbody");
        _isStopPlayer = false;
        _m_rigidBody2D.constraints = RigidbodyConstraints2D.None;
        _m_rigidBody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        _m_rigidBody2D.isKinematic = false;
    }

    private void DisableRigidBody(float angle)
    {
        Debug.Log("disable rigidbody");
        _isStopPlayer = true;
        _m_rigidBody2D.constraints = RigidbodyConstraints2D.FreezePosition;
        _m_rigidBody2D.isKinematic = true;
        Flip(angle);
    }

    private void Flip(float angle)
    {
        Vector3 currentPosition = _m_transform.eulerAngles;
        _m_transform.eulerAngles = currentPosition + new Vector3(0f, 0f, -1 * angle);
    }

    private void Move()
    {
        if (!GameController.instance.IsTurning() && m_playerFeet.IsTouching(m_scenery))
        {
            Vector3 movement = new(Input.GetAxis("Horizontal"), 0f, 0f);
            transform.position += Speed * Time.deltaTime * movement;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "TouchOnFloor")
        {
            //Debug.Log("Game is touch the floor");
            //GameController.instance.ShowGameOver();
            //Destroy(gameObject);
        }

        // if(collision.gameObject.layer == 8)
        // {
        //     GameController.instance.ToggleTurning(false);
        // }
    }
}
