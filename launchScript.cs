using UnityEngine;
using System.Collections;

public class launchScript : MonoBehaviour {

    //store positions of the mouse down and mouse up
    private Vector3 downPos;
    private Vector3 upPos;
    private Vector3 direction;
    private Vector2 currentVelocity;

    //shoot speed
    public float speed = 6.0f;

    //speed that rolling slows after bouncing stops
    public float speedDampening = 2.0f;

    //maximum shot direction velocity
    public float maxVelocity = 10.0f;
    public Vector2 maxVector;

    //player
    public GameObject jelly;

    //grounded
    private bool grounded = true;

    // Use this for initialization
    void Start ()
    {
        //jelly = GameObject.FindGameObjectWithTag("player");
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        gameObject.transform.position = jelly.transform.position;
        //grounded = jelly.getGrounded();
	}

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
    } 
}
