using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIManager : MonoBehaviour {

    public static AIManager instance;
    //enemy prefabs
    public GameObject[] enemyPrefabs;
    //basic enemies
    private List<GameObject> basicEnemies;
    public Sprite[] basicEnemySprites;
    //bi enemies
    private List<GameObject> biEnemy;

    //rotating enemies

    //spawning variables
    public float spawnTimer = 5f, spawnX;
    private bool leftLane = true;

    //camera
    Camera camera;
    public Bounds cameraBounds;


	//Start
	void Start()
    {
        //camera bounds
        camera = GameObject.Find("MainCamera").GetComponent<Camera>();
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = camera.orthographicSize * 2;
        cameraBounds = new Bounds(camera.transform.position, new Vector3(cameraHeight * screenAspect, cameraHeight, 0));

        basicEnemies = new List<GameObject>();
        biEnemy = new List<GameObject>();
	}
	
    //Awake
    void Awake()
    {
        instance = this;
    }

	//Update
	void Update()
    {
        if (spawnTimer <= 0)
        {
            spawnBasic();
            spawnBiEnemy();
            spawnTimer = 4f;
        }

        spawnTimer -= Time.deltaTime;
	}

    //spawn basic enemies
    private void spawnBasic()
    {
        if (basicEnemies.Count < 8)
        {
            if (leftLane)
            {
                spawnX = cameraBounds.center.x - 1f;
            }
            else
            {
                spawnX = cameraBounds.center.x + 1f;
            }

            basicEnemies.Add((GameObject)Instantiate(enemyPrefabs[0], new Vector2(spawnX, cameraBounds.min.y), Quaternion.identity));
            basicEnemies[basicEnemies.Count-1].GetComponent<SpriteRenderer>().sprite = basicEnemySprites[Random.Range(0, 4)];
            basicEnemies[basicEnemies.Count-1].GetComponent<Enemy>().faceLeft = !leftLane;

            leftLane = !leftLane;
        }
    }

    //spawn bi enemy
    private void spawnBiEnemy()
    {
        if (biEnemy.Count < 6)
        {
            biEnemy.Add((GameObject)Instantiate(enemyPrefabs[1], new Vector2(cameraBounds.center.x, cameraBounds.min.y), Quaternion.identity));
        }
    }

}
