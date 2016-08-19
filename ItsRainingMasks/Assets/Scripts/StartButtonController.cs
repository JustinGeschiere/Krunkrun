using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartButtonController : MonoBehaviour {

    //Vars for loading and tapping
    public GameObject Loading;
    bool began = false;
    bool ended = false;

	void Start () {
        //Check if there is a highscore, and load it if needed
	    if (PlayerPrefs.HasKey("HighScore"))
        {
            //PlayerPrefs.SetFloat("HighScore", 0); //Uncomment to set Highscore to 0
            transform.GetChild(0).GetComponent<TextMesh>().text = "Highscore: " + PlayerPrefs.GetFloat("HighScore");
        } else
        {
            transform.GetChild(0).GetComponent<TextMesh>().text = "Highscore: 0";
        }
	}

    void FixedUpdate()
    {
        //The double click scene switchinator
        if (began && ended)
        {
            Loading.SetActive(true);
            SceneManager.LoadScene("Playstate1.0");
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
