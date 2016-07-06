using UnityEngine;
using System.Collections;

public class camScript : MonoBehaviour 
{
	private Vector2 playerPos;
	private Vector2 startPos;
	private Vector2 initialPos;

	private float differenceY;

	public float camSpeed;

    public float fWidth = 40.0f;  // Desired width

    public float mouseDownX;
    public float mouseDownY;
    public float mouseUpX;
    public float mouseUpY;

    // Use this for initialization
    void Start () 
	{
        initialPos = transform.localPosition;
		Debug.Log( "the initial pos is " + initialPos.x.ToString() + ", " + initialPos.y.ToString() );
        float fT = fWidth / Screen.width * Screen.height;
        fT = fT / (2.0f * Mathf.Tan(0.5f * Camera.main.fieldOfView * Mathf.Deg2Rad));
        Vector3 v3T = Camera.main.transform.position;
        v3T.z = -fT;
        transform.position = v3T;

    }
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		//get the starting position for the frame
		startPos = transform.localPosition;

		//get where the player moved to
		playerPos = GameObject.FindWithTag ("Player").transform.localPosition;

		differenceY = playerPos.y - startPos.y;


		//transform, but only as low as the players highest position.
		if (startPos.y <= playerPos.y) 
		{
			transform.Translate (0, differenceY, 0, Space.World);
		}
	}

}
