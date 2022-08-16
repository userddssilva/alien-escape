using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    public float Speed;
    public string levelName;

    public CapsuleCollider2D m_playerFeet;
    public TilemapCollider2D m_scenery;
    public CapsuleCollider2D m_playerHead;
    public CapsuleCollider2D m_boxCapsule;
    public BoxCollider2D m_boxBox;
    public CapsuleCollider2D m_boxCapsule_2;
    public BoxCollider2D m_boxBox_2;
    
    private Rigidbody2D _m_rigidBody2D; 
    private Transform _m_transform;
    

    void Start()
    {
        _m_rigidBody2D = GetComponent<Rigidbody2D>();
        _m_transform = GetComponent<Transform>();
        RotationManager.OnGravityChange += ApplyRotation;
    }

    void Update()
    {
        Move();
        CheckDie();
    }

    private void CheckDie()
    {
        if(m_playerHead.IsTouching(m_boxCapsule) || m_playerHead.IsTouching(m_boxBox) || m_playerHead.IsTouching(m_boxCapsule_2) || m_playerHead.IsTouching(m_boxBox_2))
        {
           StartCoroutine(Die(0.75f));
        }
    }

    public void ApplyRotation(bool isGravityChanging, float angle)
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
        // Debug.Log("enable rigidbody");
        _m_rigidBody2D.constraints = RigidbodyConstraints2D.None;
        _m_rigidBody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        _m_rigidBody2D.isKinematic = false;
        GameController.instance.ToggleGravityChanging();
    }

    private void DisableRigidBody(float angle)
    {
        GameController.instance.ToggleGravityChanging();
        // Debug.Log("disable rigidbody");
        
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
        if ((m_playerFeet.IsTouching(m_scenery) ||
             m_playerFeet.IsTouching(m_boxCapsule) ||
              m_playerFeet.IsTouching(m_boxBox) ||
              m_playerFeet.IsTouching(m_boxCapsule_2) ||
              m_playerFeet.IsTouching(m_boxBox_2)))
        {
            Vector3 movement = new(Input.GetAxis("Horizontal"), 0f, 0f);
            transform.position += Speed * Time.deltaTime * movement;
        }
    }

    private IEnumerator Die(float delay) 
    {
        // GameController.instance.ShowGameOver();
        RotationManager.OnGravityChange -= ApplyRotation;
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(levelName);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Spikes")
        {
            StartCoroutine(Die(0.35f));
        }
    }
}
