using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class HelpScreenController : MonoBehaviour {

    //vars for the double click scene thing
    bool began = false;
    bool ended = false;

    void Start ()
    {
	
	}

    void FixedUpdate()
    {
        //Double click scene thing and show the kek loading sprite
        if (began && ended)
        {
            SceneManager.LoadScene("Menustate1.0");
            return;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            began = true;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            ended = true;
        }
    }
}
