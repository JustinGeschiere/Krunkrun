using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SpawnerController : MonoBehaviour {

    //vars for spawning Masks
    float SpawnTime = 4f;
    float SpawnTimer = 0f;
    public GameObject Mask;

    //vars for spawning Obstacles
    float ObTime = 7f;
    float ObTimer = 0f;
    public GameObject Obstacle;

    //var to regulate Layerorder so the objects are shown in the right order
    int LayerOrder = 1;

	void Start () {
	
	}
	
	void Update () {
        //Update the timers and trigger the spawn function when needed
        SpawnTimer += Time.deltaTime;
        ObTimer += Time.deltaTime;
        if (SpawnTimer > SpawnTime)
        {
            Spawn();
            LayerOrder++;
            SpawnTimer = 0f;
        }

        if (ObTimer > ObTime)
        {
            SpawnOb();
            LayerOrder++;
            ObTimer = 0f;
        }
	}

    void Spawn()
    {
        //Spawn a Mask
        GameObject MaskClone;
        float randomX = Random.Range(-3.5f, 3.5f);

        MaskClone = Instantiate(Mask, new Vector2(randomX, 10), Quaternion.AngleAxis(0, Vector3.forward)) as GameObject;
        MaskClone.GetComponent<SpriteRenderer>().sortingOrder = LayerOrder;

    }

    void SpawnOb()
    {
        //Spawn an Obstacle
        GameObject ObClone;
        float randomX = Random.Range(-3.5f, 3.5f);

        ObClone = Instantiate(Obstacle, new Vector2(randomX, 10), Quaternion.AngleAxis(0, Vector3.forward)) as GameObject;
        ObClone.GetComponent<SpriteRenderer>().sortingOrder = LayerOrder;
    }
}
