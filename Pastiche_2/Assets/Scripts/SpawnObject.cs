// Script to spawn all necessary game object such as food and portals

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public GameObject foodPrefab;

    public GameObject portalPrefab;

    public int maxFood = 10;

    // variables to define spawning area
    public Vector3 center;
    public Vector3 size;


    // Start is called before the first frame update
    void Start()
    {
        // At start creating first instances of food and portal
        for (int i = 0; i < maxFood; i++)
        {
            SpawnFood();
        }
            
        SpawnPortal();
    }

    // Update is called once per frame
    void Update()
    {
        // A cheat code to spawn food
        if (Input.GetKey(KeyCode.Q))
            SpawnFood();
    }

    // Defining random position for every instance of food and instantiating it
    public void SpawnFood()
    {
    
         Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), 0, Random.Range(-size.z / 2, size.z / 2));
         Instantiate(foodPrefab, pos, Quaternion.identity);

    }

    // Defining random position for every instance of portal and instantiating it
    public void SpawnPortal()
    {
        Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), 0, Random.Range(-size.z / 2, size.z / 2));

        Instantiate(portalPrefab, pos, Quaternion.identity);

    }

    // area to which spawning will be limited
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0,0,1,0.3f);
        Gizmos.DrawCube(center, size);
    }

}