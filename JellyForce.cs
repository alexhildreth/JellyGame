using UnityEngine;
using System.Collections;


public class JellyForce : MonoBehaviour 
{
	//store positions of the mouse down and mouse up
	private Vector3 downPos;
	private Vector3 upPos;
	private Vector3 direction;
	private Vector2 currentVelocity;
    private Vector3 zeroVector;
    

	//shoot speed
	public float speed = 6.0f;

	//speed that rolling slows after bouncing stops
	public float speedDampening = 2.0f;

	//maximum shot direction velocity and current power
	public float maxVelocity = 100000.0f;
	public Vector2 maxVector;
    private float currPower;
    private float percentPower;
    private Vector3 currPosition;
    private Vector3 currDirection;
    private Vector3 currLineDirection;

    //player
    public GameObject jelly;
    public GameObject lineRender;

    //grounded
    public bool grounded = true;

	// Use this for initialization
	void Start () 
	{
		Debug.Log ("This is a test of the log.");
        zeroVector.x = 0;
        zeroVector.y = 0;
        zeroVector.z = 0;

    }

    // Update is called once per frame
    void Update()
    {
        LineRenderer lineRenderer = lineRender.GetComponent<LineRenderer>();

        if (grounded)
        { 
            //store mouse pos on mouse down
            if (Input.GetMouseButtonDown(0))
            {
                downPos = Input.mousePosition;
                Debug.Log("the mouse down pos is " + downPos.x.ToString() + ", " + downPos.y.ToString());
                downPos.z = 0;
            }

            //calculate power and direction (display below in gui function)
            //true while mouse is held down
            if (Input.GetMouseButton(0) && currentVelocity.x == 0)
            {
                currPosition = Input.mousePosition;
                currDirection = downPos - currPosition;
                currPower = Mathf.Pow(currDirection.x, 2) + Mathf.Pow(currDirection.y, 2);

                if (currPower <= maxVelocity)
                {
                    percentPower = (currPower / maxVelocity) * 100;
                }
                else
                {
                    percentPower = 100.0f;
                }

                //render direction line
                currLineDirection = currDirection;
                currLineDirection.x *= 0.02f;
                currLineDirection.y *= 0.02f;

                //set beginning on jelly since using world coordinates
                //lineRenderer.SetPosition(0, gameObject.transform.position);
                lineRenderer.SetPosition(1, currLineDirection);
            }

            
            //store mouse pos on mouse up. create the resulting vector
            if (Input.GetMouseButtonUp(0) && currentVelocity.x == 0)
            {
                //lineRenderer.SetPosition(0, zeroVector);
                lineRenderer.SetPosition(1, zeroVector);

                upPos = Input.mousePosition;
                Debug.Log("the mouse up pos is " + upPos.x.ToString() + ", " + upPos.y.ToString());
                upPos.z = 0;

                direction = downPos - upPos;

                //direction.Normalize ();

                //if direction vector is greater than a set maximum, divide down incrimentally until at max
                //otherwise, fire as normal.
                if (Mathf.Pow(direction.x, 2) + Mathf.Pow(direction.y, 2) > maxVelocity)
                {
                    while (Mathf.Pow(direction.x, 2) + Mathf.Pow(direction.y, 2) > maxVelocity)
                    {
                        direction.x *= 0.95f;
                        direction.y *= 0.95f;
                    }

                    GetComponent<Rigidbody2D>().AddForce(direction * speed);
                    grounded = false;
                }

                else
                {
                    GetComponent<Rigidbody2D>().AddForce(direction * speed);
                    grounded = false;
                }
            } 
        } 

		//if stops bouncing, gradually stop x direction movement, then make player face up
		currentVelocity = GetComponent<Rigidbody2D>().velocity;

		if ( currentVelocity.y < 0.2 && currentVelocity.y > -0.2) 
		{
			GetComponent<Rigidbody2D>().AddForce (-speedDampening * currentVelocity);

            if(currentVelocity.x < 0.02)
            {
                currentVelocity.x = 0;
            }
		}
        if(currentVelocity.x == 0)
        {
            jelly.transform.rotation = Quaternion.identity;
        }

        //cheat mode for testing
        if (Input.GetKey("w"))
        {
            GameObject.FindGameObjectWithTag("Player").transform.Translate(0, 0.5f, 0);
        }
        if (Input.GetKey("s"))
        {
            GameObject.FindGameObjectWithTag("Player").transform.Translate(0, -0.5f, 0);
        }

    }

    //ground check
    void OnTriggerEnter2D(Collider2D other)
    {
        grounded = true;
    }
    void OnTriggerExit2D(Collider2D other)
    {
        grounded = false;
    }

    //getters
    public bool getGrounded()
    {
        return grounded;
    }
    /*
    void OnMouseDown()
    {
        downPos = Input.mousePosition;
        Debug.Log("the mouse down pos is " + downPos.x.ToString() + ", " + downPos.y.ToString());
        downPos.z = 0;
    }

    void OnMouseUp()
    {
        if (grounded)
        {
            upPos = Input.mousePosition;
            Debug.Log("the mouse up pos is " + upPos.x.ToString() + ", " + upPos.y.ToString());
            upPos.z = 0;

            direction = downPos - upPos;

            //direction.Normalize ();

            //if direction vector is greater than a set maximum, divide down incrimentally until at max
            //otherwise, fire as normal.
            if (Mathf.Pow(direction.x, 2) + Mathf.Pow(direction.y, 2) > maxVelocity)
            {
                while (Mathf.Pow(direction.x, 2) + Mathf.Pow(direction.y, 2) > maxVelocity)
                {
                    direction.x *= 0.95f;
                    direction.y *= 0.95f;
                }

                GetComponent<Rigidbody2D>().AddForce(direction * speed);
                grounded = false;
            }

            else
            {
                GetComponent<Rigidbody2D>().AddForce(direction * speed);
                grounded = false;
            }
        }
    } */

    /*
    void OnMouseDown()
    {
        downPos = Input.mousePosition;
        Debug.Log( "the mouse down pos is " + downPos.y.ToString() );
        downPos.z = 0;
    }

    void OnMouseUp()
    {
        upPos = Input.mousePosition;
        Debug.Log( "the mouse up pos is " + upPos.y.ToString() );
        upPos.z = 0;

        var direction = downPos - upPos;

        direction.Normalize ();

        rigidbody2D.AddForce (direction * speed);
    }
    */

    //display power
    void OnGUI()
    {
        GUI.color = Color.black;
        GUI.Label(new Rect(150, 0, 100, 100), "Power: ");
        GUI.Label(new Rect(210, 0, 100, 100), percentPower.ToString());
    }


}
