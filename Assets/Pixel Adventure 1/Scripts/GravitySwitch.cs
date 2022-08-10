using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySwitch : MonoBehaviour
{
    private float g = 9.81f;
    private int angle = 0;
    // Update is called once per frame
    void Update()
    {
        Vector2 currentGravity = Physics2D.gravity;
        if(Input.GetKeyDown(KeyCode.C) && !GameController.instance.IsTurning()) 
        {
            switch (angle)
            {
                case 0: 
                    Physics2D.gravity = new Vector2(currentGravity.x + g, currentGravity.y + g);
                    break;
                case 90:
                    Physics2D.gravity = new Vector2(currentGravity.x - g, currentGravity.y + g);
                    break;
                case 180:
                    Physics2D.gravity = new Vector2(currentGravity.x - g, currentGravity.y - g);
                    break;
                case 270:
                    Physics2D.gravity = new Vector2(currentGravity.x + g, currentGravity.y - g);
                    break;
            }
            // GameController.instance.ToggleTurning(true);

            angle = (angle + 90) % 360;
            
            // Debug.Log(Physics2D.gravity);
            // Debug.Log(angle);
        } 
        else if (Input.GetKeyDown(KeyCode.Z) && !GameController.instance.IsTurning())
        {   
            switch (angle)
            {
                case 0: 
                    Physics2D.gravity = new Vector2(currentGravity.x - g, currentGravity.y + g);
                    break;
                case 270:
                    Physics2D.gravity = new Vector2(currentGravity.x + g, currentGravity.y + g);
                    break;
                case 180:
                    Physics2D.gravity = new Vector2(currentGravity.x + g, currentGravity.y - g);
                    break;
                case 90:
                    Physics2D.gravity = new Vector2(currentGravity.x - g, currentGravity.y - g);
                    break;
            }

            // GameController.instance.ToggleTurning(true);

            if (angle - 90 < 0)
            {
                angle = 270;
            }
            else 
            {
                angle = angle - 90;
            }
            // Debug.Log(Physics2D.gravity);
            // Debug.Log(angle);
        }
    }
}
