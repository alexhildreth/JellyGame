using UnityEngine;
using System.Collections;

public class gameOver : MonoBehaviour {

    //max height variable
    //the camera will always be at the max height
    private float maxHeight = 0;
    private float currentHeight = 0;

    private float deathTimer = 0.0f;

    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        /*
        //get current height and make max height if more
        currentHeight = gameObject.transform.position.y;
        if(currentHeight > maxHeight)
        {
            maxHeight = currentHeight;
        }

        //if current height is 
        */
	}

    public void OnBecameInvisible()
    {
        Destroy(gameObject);
        Application.LoadLevel(Application.loadedLevel);
    }
}
