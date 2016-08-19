using UnityEngine;
using System.Collections;

public class BalanceIndiController : MonoBehaviour {

    public GameObject Target;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = new Vector2(Target.transform.position.x, -2.57f);
	}
}
