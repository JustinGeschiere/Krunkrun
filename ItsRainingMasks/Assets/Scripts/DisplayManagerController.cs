using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DisplayManagerController : MonoBehaviour {

    //Vars for tracking score
    float PlayScore;
    float HighScore;

    //Vars for the double tap
    bool began = false;
    bool ended = false;

    //Some vars used for visuals
    GameObject ScoreText;
    GameObject HighScoreText;
    GameObject Medal;
    public GameObject Loading;

    //Vars for the medal
    public Sprite medalTry;
    public Sprite medalBronze;
    public Sprite medalSilver;
    public Sprite medalGold;


    void Start () {
        //Check if a Highscore exists and update it if needed
        PlayScore = PlayerPrefs.GetFloat("Score");
        if (PlayerPrefs.HasKey("HighScore"))
        {
            HighScore = PlayerPrefs.GetFloat("HighScore");
            if (PlayScore > HighScore)
            {
                HighScore = PlayScore;
                PlayerPrefs.SetFloat("HighScore", HighScore);
                PlayerPrefs.Save();
            }
        } else
        {
            HighScore = PlayScore;
            PlayerPrefs.SetFloat("HighScore", HighScore);
            PlayerPrefs.Save();
        }

        //Put the updated scores in textboxes visible for the player
        ScoreText = transform.GetChild(0).gameObject;
        HighScoreText = transform.GetChild(1).gameObject;
        Medal = transform.GetChild(2).gameObject;
        ScoreText.GetComponent<TextMesh>().text = "Score: " + PlayScore;
        HighScoreText.GetComponent<TextMesh>().text = "Highscore: " + HighScore;

        //Determine which medal must be shown to the player
        if (PlayScore == 0)
        {
            Medal.GetComponent<SpriteRenderer>().sprite = medalTry;
        } else if (PlayScore >= 1 && PlayScore <=5 )
        {
            Medal.GetComponent<SpriteRenderer>().sprite = medalBronze;
        } else if (PlayScore >= 6 && PlayScore <=9 )
        {
            Medal.GetComponent<SpriteRenderer>().sprite = medalSilver;
        } else
        {
            Medal.GetComponent<SpriteRenderer>().sprite = medalGold;
        }
    }

    void FixedUpdate()
    {
        //Only way I could get the tap to go to the next scene to work decently
        //A double tap is required
        if (began && ended)
        {
            Loading.SetActive(true);
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
