using UnityEngine;
using System.Collections;

public class CloudController : MonoBehaviour {

	
	void Start () {
	
	}
	
	void Update () {
        //Controls the clouds and makes them cycle through the game
        GetComponent<Rigidbody2D>().velocity = new Vector2(0.5f, GetComponent<Rigidbody2D>().velocity.y);
        if (transform.position.x >= 7)
        {
            transform.position = new Vector2(-7, Random.Range(3, 8));
        }
	}
}
