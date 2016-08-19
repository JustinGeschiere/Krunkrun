using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MaskController : MonoBehaviour {

    //Damn sexy vars for a damn sexy tower with damn sexy physics
    GameObject stick;
    float moveHor;

    //Animator var
    Animator Anim;

    //Vars for phase/stage of the object
    public bool stacked;

    //Audio vars
    public AudioSource pickup;
    public AudioSource destroy;

    //Var to determine the way the object should be destroyed
    public bool violent = false;

    //Var that didnt make it through the playtesting
    //GameObject MaskFinder;

	void Start () {
        //assign values to vars above
        stick = transform.GetChild(0).gameObject;
        //MaskFinder = transform.GetChild(1).gameObject;
        Anim = GetComponent<Animator>();

        //Determine in the Animator if this object should be a BirdMask or FireMask
        int random = Random.Range(0, 2);
        if (random == 0)
        {
            Anim.SetBool("BirdMask", true);
        } else
        {
            Anim.SetBool("BirdMask", false);
        }

    }

    void Update() {
        /*
        //MaskFinder follows the player around but always stays visible for the player
        if (MaskFinder != null)
        {
            MaskFinder.transform.position = new Vector2(transform.position.x, transform.position.y + 0.20f);
            Vector3 pos = Camera.main.WorldToViewportPoint(MaskFinder.transform.position);
            pos.x = Mathf.Clamp01(pos.x);
            pos.y = Mathf.Clamp01(pos.y);
            MaskFinder.transform.position = Camera.main.ViewportToWorldPoint(pos);
        }
        */

        //Check for a parent, if it does not have one it is still in the falling stage
        if (transform.parent == null) 
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -2f);
            if (transform.position.y <= -1.5 && !destroy.isPlaying)
            {
                destroy.Play();
                //Destroy(MaskFinder.gameObject);
                GetComponent<SpriteRenderer>().enabled = false;
                //PlayerPrefs.SetFloat("Score", PlayerPrefs.GetFloat("Score") - 2);
                //PlayerPrefs.Save();
                Destroy(this.gameObject, destroy.clip.length);
            }
        }
        else
        {


            //If it has a parent it is in the collected state in the damn sexy tower with damn sexy physics
            //Destroy(MaskFinder.gameObject);
            Anim.SetBool("HasParent", true);
            transform.position = new Vector2(transform.parent.transform.position.x, transform.parent.transform.position.y);

            //Disable the collider of the player so it doesnt collect more masks instead of this object
            if (transform.parent.tag != "Mask")
            {
                transform.parent.GetComponent<BoxCollider2D>().enabled = false;
            }
            
            //The real magic, Dont know how I got it to work this smooth. But as long as it works.
            transform.Rotate(Vector3.forward * (moveHor/4));
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0,1,0), Time.deltaTime * 0.2f);
        }
        if (stick.transform.childCount == 0)
        {
            //If the child gets destroyed it should go a stage back and be able to pick up a mask again
            stacked = false;
        }

    }

    void FixedUpdate ()
    {
        //FixedUpdate for Input
        if (Application.platform == RuntimePlatform.Android)
        {
            moveHor = Input.acceleration.x;
        }
        else
        {
            moveHor = Input.GetAxis("Horizontal");
        }
    }

    void OnTriggerEnter2D (Collider2D Col)
    {
        //For collecting masks when one gets in range and adding score
        if (Col.transform.parent == null && Col.tag == "Mask" && !stacked)
        {
            Col.gameObject.transform.SetParent(stick.transform);
            stacked = true;
            pickup.Play();
            PlayerPrefs.SetFloat("Score", PlayerPrefs.GetFloat("Score") + 1);
            PlayerPrefs.Save();
        }
    }

    void OnDestroy()
    {
        //When this object gets destroyed by a obstacle it reduces the score
        if (transform.parent != null && violent)
        {
            PlayerPrefs.SetFloat("Score", PlayerPrefs.GetFloat("Score") - 1);
            PlayerPrefs.Save();
        }
    }

}
