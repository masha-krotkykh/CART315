// Script for the snake "engine"

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : MonoBehaviour
{
    public SnakeMovement movement;
    public SpawnObject SO;

    // Define behaviour on different collisions
    // ON COLLISION WITH FOOD OBJECT

    void OnCollisionEnter(Collision collision)
    {
        // Make sure to have the correct tag 
        if (collision.gameObject.tag == "Food")
        {
            // Adding a body part to the snake
            movement.AddBodyPart();

            // make food object disappear
            Destroy(collision.gameObject);

            // Call SpawnFood method to create another instance of food object
            SO.SpawnFood();
        }

        // ON COLLISION WITH PORTAL OBJECT
        else if (collision.gameObject.tag == "Portal")
        {
            // Move snake "engine" to random positio within spaen area
            this.transform.position = new Vector3(Random.Range(-SO.size.x / 2, SO.size.x / 2), 0.5f, Random.Range(-SO.size.z / 2, SO.size.z / 2));

            // Destroy portal instance
            Destroy(collision.gameObject);

            // Destroy all snake  body parts and all achieved progress
            for (int i = movement.BodyParts.Count - 1; i > 1; i--)
            {
                Destroy(movement.BodyParts[i].gameObject);

                movement.BodyParts.Remove(movement.BodyParts[i]);
            }
            movement.currentScore.text = "Score: 0";

            // Spawn another instance of portal object
            SO.SpawnPortal();
        }

        // ON COLLISION WITH OTHER OBJECTS (including walls and own tail)
        // Call die method
        // Destroy all snake body parts and all achieved progress, end game and activate death screen
        else if (collision.transform != movement.BodyParts[1] && movement.isAlive && collision.gameObject.tag != "Safe" && collision.gameObject.tag != "Portal")
        {
            if (Time.time - movement.TimeFromLastRetry > 5)
                movement.die();
        }
    }
}