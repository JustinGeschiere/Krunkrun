using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

    //var for regulating the balance
    float moveHor =  0f;

    //vars to show the remaining time
    public GameObject TimeText;
    float GameTime = 60f;
    float GameTimer = 0f;

    //var for the balance
    float Balance = 0f;
    GameObject BalanceBar;
    GameObject BalanceInd;

    //var for checking if the update should keep running 100%
    bool running = true;

    //Animator
    Animator Anim;

	void Start ()
    {
        //Assigning value to the vars above
        PlayerPrefs.SetFloat("Score", 0);
        Anim = GetComponent<Animator>();
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        TimeText = transform.GetChild(1).gameObject;
        BalanceBar = transform.GetChild(0).gameObject;
        BalanceInd = BalanceBar.transform.GetChild(0).gameObject;
	}
	
	void Update () {
        //check if the update should be run (not a clean fix)
        if (!running)
        {
            //stop movement
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            return;
        }
        //Communicate the absolute value of Balance to the Animator
        Anim.SetFloat("Balance", Mathf.Abs(Balance));
        //Checks if the player has fallen
        if (Balance > 85 || Balance < -85)
        {
            //Check if childs have to be removed
            if (transform.childCount > 3)
            {
                transform.GetChild(3).gameObject.SetActive(false);
            }
            //Stop the updates play a sound and go to the next scene after 2 sec
            running = false;
            GetComponent<AudioSource>().Play();
            Invoke("EndGame", 2);
        }
        //Add deltaTime to the counter and check if the game should continue
        GameTimer += Time.deltaTime;
        TimeText.GetComponent<TextMesh>().text = "Time: " + (int)(GameTime - GameTimer);
        if (GameTimer > GameTime)
        {
            SceneManager.LoadScene("ScoreState1.0");
        }
        //Some crazy movement calculations als for the Balance
        GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Clamp(Balance/70, -1, 1) * 2 , GetComponent<Rigidbody2D>().velocity.y);
        this.transform.position = new Vector2(Mathf.Clamp(transform.position.x, -2.92f, 2.92f), transform.position.y);
        BalanceInd.transform.position = new Vector2(transform.position.x + 2 * (Balance / 100), BalanceInd.transform.position.y);
        if (moveHor != 0)
        {
            Balance += 2 * moveHor + Balance / 100;
            Balance = Mathf.Clamp(Balance, -90, 90);
        }
        if (Balance > 0)
        {
            Balance -= 0.3f - (0.1f * Balance/80);
        } else if (Balance < 0)
        {
            Balance += 0.3f + (0.1f * Balance / 80);
        }
        //Check if the prite is still facing the right direction
        if (GetComponent<Rigidbody2D>().velocity.x > 0.1)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        } else if (GetComponent<Rigidbody2D>().velocity.x < -0.1)
        {
            GetComponent<SpriteRenderer>().flipX = true;
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
        //Check if the Trigger has touched a collectable mask
        if (Col.tag == "Mask")
        {
            Col.gameObject.transform.SetParent(this.transform);
            PlayerPrefs.SetFloat("Score", PlayerPrefs.GetFloat("Score") + 1);
            PlayerPrefs.Save();
        }
    }

    void EndGame ()
    {
        //Next scene routine when the player has fallen
        PlayerPrefs.SetFloat("Score", 0);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Scorestate1.0");
    }
}
