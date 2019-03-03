using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : MonoBehaviour
{
    public SnakeMovement movement;

    public SpawnObject SO;

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Food")
        {
            movement.AddBodyPart();

            Destroy(collision.gameObject);

            SO.SpawnFood();
        }

        else if(collision.gameObject.tag == "Portal")
        {
            this.transform.position = new Vector3(0, 0.5f, 0);

            Destroy(collision.gameObject);

            SO.SpawnPortal();
        }

        else if(collision.transform != movement.BodyParts[1] && movement.isAlive && collision.gameObject.tag != "Safe" && collision.gameObject.tag != "Portal") 
        {
            if (Time.time - movement.TimeFromLastRetry > 5)
                movement.die();
        }
    }

}
