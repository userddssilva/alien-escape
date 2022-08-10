using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed;

    private Rigidbody2D rig;
    private bool isYAxis = false;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Flip();
    }

    void Flip()
    {
        if(Input.GetKeyDown(KeyCode.C) && !GameController.instance.IsTurning()) 
        {
            Vector3 currentPosition = transform.eulerAngles;
            transform.eulerAngles = currentPosition + new Vector3(0f, 0f, 90f);
            isYAxis = !isYAxis;

            Debug.Log("Flip:" + transform.eulerAngles.z);
        } else if(Input.GetKeyDown(KeyCode.Z) && !GameController.instance.IsTurning()) 
        {
            Vector3 currentPosition = transform.eulerAngles;
            transform.eulerAngles = currentPosition + new Vector3(0f, 0f, -90f); 
            isYAxis = !isYAxis;

            Debug.Log("Flip:" + transform.eulerAngles.z);
        }
        

    }

    void Move()
    {
        if (isYAxis)
        {
            MoveYAxis();
        } 
        else 
        {
            MoveXAxis();
        }
    }

    void MoveYAxis()
    {
        if (!GameController.instance.IsTurning())
        {
            Vector3 movement = new Vector3(0f, Input.GetAxis("Horizontal")*-1, 0f);
            transform.position += movement * Time.deltaTime * Speed;


            float z = transform.eulerAngles.z;
            float y = transform.eulerAngles.y;
            if (Input.GetAxis("Horizontal") > 0f)
            { 
                transform.eulerAngles = new Vector3(0f, y, z);
                Debug.Log("Andando na parede pra direta/baixo");
                Debug.Log(transform.eulerAngles);
            }        
            
            if (Input.GetAxis("Horizontal") < 0f)
            {
                transform.eulerAngles = new Vector3(180f, y, z);
                Debug.Log("Andando na parede pra esquerda/cima");
                Debug.Log(transform.eulerAngles);
            }
        }
    }

    void MoveXAxis() 
    {
        if (!GameController.instance.IsTurning())
        {
            Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
            transform.position += movement * Time.deltaTime * Speed;

            float z = transform.eulerAngles.z;
            float x = transform.eulerAngles.x;
            if (Input.GetAxis("Horizontal") > 0f)
            { 
                transform.eulerAngles = new Vector3(x, 0f , z);
                Debug.Log("Andando no chão pra direta");

            }        
            
            if (Input.GetAxis("Horizontal") < 0f)
            {
                transform.eulerAngles = new Vector3(x, 180f, z);
                Debug.Log("Andando no chão pra esquerda");
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Spike")
        {
           GameController.instance.ShowGameOver();
           Destroy(gameObject);
        }   

        // if(collision.gameObject.layer == 8)
        // {
        //     GameController.instance.ToggleTurning(false);
        // }
    }

    // void StartFalling() {

    // }

    // void StopFalling() {}
}
