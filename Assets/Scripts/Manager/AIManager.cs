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
    private List<GameObject> rotatingEnemies;

    //spawning variables
    public float spawnTimer = 5f;
    private float spawnX;
    private bool leftLane = true, spawningAlien = false;
    private int pattern = 0, alienCount= 0, alienLimit;

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
        rotatingEnemies = new List<GameObject>();
	}
	
    //Awake
    void Awake()
    {
        instance = this;
    }

	//Update
	void Update()
    {
        if (spawningAlien && spawnTimer <= 0)
        {
            spawnAlien();
        }
        else if (!spawningAlien && spawnTimer <= 0)
        {
            int spon = Random.Range(0, 3);
            if (spon == 0)
            {
                spawnBasic();
            }
            else if (spon == 1)
            {
                spawnRotating();
            }
            else if (spon == 2)
            {
                spawningAlien = true;
                //set pattern
                pattern = Random.Range(1, 4);
                alienLimit = Random.Range(4, 11);
            }
            spawnBiEnemy();
            spawnTimer = 3f;
        }

        spawnTimer -= Time.deltaTime;
	}

    //spawn basic enemies
    private void spawnBasic()
    {
        if (basicEnemies.Count < 6)
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
        if (biEnemy.Count < 8)
        {
            biEnemy.Add((GameObject)Instantiate(enemyPrefabs[1], new Vector2(cameraBounds.center.x, cameraBounds.min.y), Quaternion.identity));
        }
    }

    //spawn rotating enemy
    private void spawnRotating()
    {
        if (rotatingEnemies.Count < 6)
        {
            if (leftLane)
            {
                spawnX = cameraBounds.center.x - 1f;
            }
            else
            {
                spawnX = cameraBounds.center.x + 1f;
            }

            rotatingEnemies.Add((GameObject)Instantiate(enemyPrefabs[2], new Vector2(spawnX, cameraBounds.min.y), Quaternion.identity));

            leftLane = !leftLane;
        }
    }

    //spawn alien
    private void spawnAlien()
    {
        if (pattern == 1)
        {
            //for player 1 side
            GameObject g = (GameObject)Instantiate(enemyPrefabs[3], new Vector2(cameraBounds.center.x - 2f, cameraBounds.max.y), Quaternion.identity);
            g.GetComponent<AlienEnemy>().target = new Vector2(cameraBounds.min.x, cameraBounds.center.y - 2f);
            g.transform.Rotate(Vector3.forward, 180f);

            //for player 2 side
            g = (GameObject)Instantiate(enemyPrefabs[3], new Vector2(cameraBounds.center.x + 2f, cameraBounds.min.y), Quaternion.identity);
            g.GetComponent<AlienEnemy>().target = new Vector2(cameraBounds.max.x, cameraBounds.center.y + 2f);
        }
        else if (pattern == 2)
        {
            //for player 1 side
            GameObject g = (GameObject)Instantiate(enemyPrefabs[3], new Vector2(cameraBounds.center.x - 2f, cameraBounds.min.y), Quaternion.identity);
            g.GetComponent<AlienEnemy>().target = new Vector2(cameraBounds.min.x, cameraBounds.center.y + 2f);

            //for player 2 side
            g = (GameObject)Instantiate(enemyPrefabs[3], new Vector2(cameraBounds.center.x + 2f, cameraBounds.max.y), Quaternion.identity);
            g.GetComponent<AlienEnemy>().target = new Vector2(cameraBounds.max.x, cameraBounds.center.y - 2f);
            g.transform.Rotate(Vector3.forward, 180f);
        }
        else if (pattern == 3)
        {
            //for player 1 side
            GameObject g = (GameObject)Instantiate(enemyPrefabs[3], new Vector2(cameraBounds.center.x - 2f, cameraBounds.max.y), Quaternion.identity);
            g.GetComponent<AlienEnemy>().target = new Vector2(cameraBounds.min.x, cameraBounds.center.y - 2f);
            g.transform.Rotate(Vector3.forward, 180f);
            g = (GameObject)Instantiate(enemyPrefabs[3], new Vector2(cameraBounds.center.x - 2f, cameraBounds.min.y), Quaternion.identity);
            g.GetComponent<AlienEnemy>().target = new Vector2(cameraBounds.min.x, cameraBounds.center.y + 2f);

            //for player 2 side
            g = (GameObject)Instantiate(enemyPrefabs[3], new Vector2(cameraBounds.center.x + 2f, cameraBounds.min.y), Quaternion.identity);
            g.GetComponent<AlienEnemy>().target = new Vector2(cameraBounds.max.x, cameraBounds.center.y + 2f);
            g = (GameObject)Instantiate(enemyPrefabs[3], new Vector2(cameraBounds.center.x + 2f, cameraBounds.max.y), Quaternion.identity);
            g.GetComponent<AlienEnemy>().target = new Vector2(cameraBounds.max.x, cameraBounds.center.y - 2f);
            g.transform.Rotate(Vector3.forward, 180f);
        }
        alienCount++;
        spawnTimer = 0.5f;

        if (alienCount >= alienLimit)
        {
            spawningAlien = false;
            spawnTimer = 4f;
            alienCount = 0;
        }
    }
}
