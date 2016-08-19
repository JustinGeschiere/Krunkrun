using UnityEngine;
using System.Collections;

public class ObstacleController : MonoBehaviour {
    
    //random var for rotation and horizontal movement
    float sideVel;

	void Start () {
        //value for the random var
        sideVel = Random.Range(-1.5f, 1.5f);
	}
	
	void Update () {
        //Regulate the movement of the obstacle and determine when it isnt useful for the game anymore
        GetComponent<Rigidbody2D>().velocity = new Vector2(sideVel, -3f);
        if (transform.position.y <= -2)
        {
            Destroy(gameObject);
        }
        transform.Rotate(Vector3.forward * sideVel);
    }

    void OnTriggerEnter2D (Collider2D Col)
    {
        //Check if this object hits a mask that is part of the damn sexy tower with damn sexy physics
        if (Col.tag == "Mask" && Col.transform.parent != null)
        {
            //if this knocks of the first mask of the player, the player is enabled to collect a mask again
            if (Col.transform.parent.tag != "Mask")
            {
                Col.transform.parent.GetComponent<BoxCollider2D>().enabled = true;
            }
            //tells the mask he got rekt by violence
            Col.GetComponent<MaskController>().violent = true;
            //Destroy(Col.gameObject);
            Destroy(Col.gameObject);
            Destroy(gameObject);
        }
    }
}
