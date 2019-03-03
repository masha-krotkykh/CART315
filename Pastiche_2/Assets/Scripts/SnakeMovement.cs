using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnakeMovement : MonoBehaviour
{
    public List<Transform> BodyParts = new List<Transform>();

    public float mindistance = 0.25f;

    public float constY = 0.5f;

    public int beginsize;

    public float beginSpeed = 3;
    float speed;
    public float rotationspeed = 70;

    public float TimeFromLastRetry;

    public GameObject bodyprefab;

    public Text currentScore;
    public Text scoreText;
    public GameObject deathScreen;

    private float dis;
    private Transform curBodyPart;
    private Transform prevBodyPart;

    public bool isAlive;



    // Start is called before the first frame update
    void Start()
    {
        StartLevel();
    }

    public void StartLevel() 
    {
        TimeFromLastRetry = Time.time;

        deathScreen.SetActive(false);

        for (int i = BodyParts.Count - 1; i > 0; i--)
        {
            Destroy(BodyParts[i].gameObject);

            BodyParts.Remove(BodyParts[i]);
        }

        BodyParts[0].position = new Vector3(0, 0.5f, 0);
        BodyParts[0].rotation = Quaternion.identity;
        currentScore.gameObject.SetActive(true);
        currentScore.text = "Score: 0";

        speed = beginSpeed;

        isAlive = true;

        for (int i = 0; i < beginsize; i++)
        {
                AddBodyPart();
        }



        BodyParts[0].position = new Vector3(2, constY, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //if(isAlive)
            Move();

        //if (Input.GetKey(KeyCode.Q))
        //AddBodyPart();
    }

    public void Move()
    {
        float curspeed = speed;

        if(Input.GetKey(KeyCode.W))
            curspeed *= 2;

        if (Input.GetKey(KeyCode.S))
            curspeed /= 2;

        BodyParts[0].Translate(BodyParts[0].forward * curspeed * Time.smoothDeltaTime, Space.World);

        if (Input.GetAxis("Horizontal") != 0)
            BodyParts[0].Rotate(Vector3.up * rotationspeed * Time.deltaTime * Input.GetAxis("Horizontal"));

        for (int i = 1; i < BodyParts.Count; i++)
        {
            curBodyPart = BodyParts[i];
            prevBodyPart = BodyParts[i - 1];

            dis = Vector3.Distance(prevBodyPart.position, curBodyPart.position);

            Vector3 newpos = prevBodyPart.position;

            newpos.y = constY;

            float T = Time.deltaTime * dis / mindistance * curspeed;

            if (T > 0.5f)
                T = 0.5f;
            curBodyPart.position = Vector3.Slerp(curBodyPart.position, newpos, T);
            curBodyPart.rotation = Quaternion.Slerp(curBodyPart.rotation, prevBodyPart.rotation, T);
        }

    }


    public void AddBodyPart() 
    {
        if (isAlive) {
            Transform newpart = (Instantiate(bodyprefab, BodyParts[BodyParts.Count - 1].position, BodyParts[BodyParts.Count - 1].rotation) as GameObject).transform;

            newpart.SetParent(transform);

            BodyParts.Add(newpart);

            currentScore.text = "Score: " + (BodyParts.Count - beginsize - 1).ToString();
        }
        speed += .1f;
    }

    public void die()
    {
        isAlive = false;

        scoreText.text = "Your score is: " + (BodyParts.Count - beginsize).ToString();

        currentScore.gameObject.SetActive(false);

        deathScreen.SetActive(true);

        for (int i = BodyParts.Count - 1; i > beginsize; i--)
        {
            Destroy(BodyParts[i].gameObject);

            BodyParts.Remove(BodyParts[i]);
        }
    }
}
