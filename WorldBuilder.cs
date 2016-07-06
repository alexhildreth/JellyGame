using UnityEngine;
using System.Collections;

public class WorldBuilder : MonoBehaviour
{
    //variables for the current player height and the currently built height
    private float currentHeight = 0.0f;
    private float buildHeight = 0.0f;
    private float maxHeight = 0.0f;

    //int for if platform should be spawned on left side of screen (1 for left, -1 for right)
    private int leftSide = 1;

    //load prefabs
    public GameObject floorUnit;
    public GameObject wallUnit;
    public GameObject platform1;
    public GameObject jelly;
    public GameObject BgUnit;
    public GameObject cam;
    public GameObject circle;

    //camera position
    private float camHeight;

    //random variables for platform position


    // Use this for initialization
    void Start ()
    {
        //build initial screen and instantate player

        //floor
        Instantiate(floorUnit, new Vector3(0, buildHeight, 0), Quaternion.identity);
        //player
        Instantiate(jelly, new Vector3(5, buildHeight + 1, -1), Quaternion.identity);

        Instantiate(circle, new Vector3(5, buildHeight + 1, -2), Quaternion.identity);
        //walls, platforms, and bg
        createPlatforms();
        createWalls();

        //set score to 0
        maxHeight = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        //get current height
        currentHeight = GameObject.FindGameObjectWithTag("Player").transform.position.y;

        if (currentHeight > maxHeight)
        {
            maxHeight = currentHeight;
        }
       

        //if close to end of draw, draw more
        if (maxHeight > (buildHeight - 20))
        {
            createPlatforms();
            createWalls();
        }
	}

    //create walls:
    //Creates a set of walls and background 3 units of 1000px each high
    void createWalls()
    {
        //left wall
        Instantiate(wallUnit, new Vector3(-10, buildHeight + 5, 0), Quaternion.identity);
        Instantiate(wallUnit, new Vector3(-10, buildHeight + 15, 0), Quaternion.identity);
        Instantiate(wallUnit, new Vector3(-10, buildHeight + 25, 0), Quaternion.identity);
        Instantiate(wallUnit, new Vector3(-10, buildHeight + 35, 0), Quaternion.identity);
        //right wall
        Instantiate(wallUnit, new Vector3(10, buildHeight + 5, 0), Quaternion.identity);
        Instantiate(wallUnit, new Vector3(10, buildHeight + 15, 0), Quaternion.identity);
        Instantiate(wallUnit, new Vector3(10, buildHeight + 25, 0), Quaternion.identity);
        Instantiate(wallUnit, new Vector3(10, buildHeight + 35, 0), Quaternion.identity);
        //background
        Instantiate(BgUnit, new Vector3(0, buildHeight + 5, 10), Quaternion.identity);
        Instantiate(BgUnit, new Vector3(0, buildHeight + 15, 10), Quaternion.identity);
        Instantiate(BgUnit, new Vector3(0, buildHeight + 25, 10), Quaternion.identity);
        Instantiate(BgUnit, new Vector3(0, buildHeight + 35, 10), Quaternion.identity);

        //increase build height
        buildHeight = buildHeight + 40;
    }


    //create platforms
    //creates different difficulties of platforms based on total height
    void createPlatforms()
    {
        //set a local build height modifier
        int localBuildHeight = 5;

        //create 3 platforms
        for (int i = 1; i <= 4; i++)
        {
            //left side platform
            if (leftSide == 1)
            {
                Instantiate(platform1, new Vector3(-5.5f + Random.Range(-1.5f, 1.5f), buildHeight + localBuildHeight + Random.Range(-1.5f, 1.5f), -5), Quaternion.identity);
            }
            else
            {
                Instantiate(platform1, new Vector3(5.5f + Random.Range(-1.5f, 1.5f), buildHeight + localBuildHeight + Random.Range(-1.5f, 1.5f), -5), Quaternion.identity);
            }

            //switch side and increase modifier
            leftSide = -leftSide;
            localBuildHeight = localBuildHeight + 10;
        }

    }

    //print score on screen
    void OnGUI()
    {
        GUI.color = Color.black;
        GUI.Label(new Rect(20, 0, 50, 50), "Score: ");
        GUI.Label(new Rect(60, 0, 600, 600), maxHeight.ToString());
    }
}
