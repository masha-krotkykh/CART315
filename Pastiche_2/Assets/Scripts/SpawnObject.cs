using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public GameObject foodPrefab;

    public GameObject portalPrefab;

    public Vector3 center;
    public Vector3 size;


    // Start is called before the first frame update
    void Start()
    {
        SpawnFood();
        SpawnPortal();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
            SpawnFood();
    }

    public void SpawnFood()
    {
        Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), 0.3f, Random.Range(-size.z / 2, size.z / 2));

        Instantiate(foodPrefab, pos, Quaternion.identity);
    }

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