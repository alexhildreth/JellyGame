using UnityEngine;
using System.Collections;

public class destroyWhenInvisible : MonoBehaviour
{
    //find camera to get its position
    public GameObject player;

    private float playerPos = 0.0f;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        //destroy object if off screen
        playerPos = player.transform.position.y;

        if (gameObject.transform.position.y < playerPos - 30)
        {
            Destroy(gameObject);
        }
    }

}
