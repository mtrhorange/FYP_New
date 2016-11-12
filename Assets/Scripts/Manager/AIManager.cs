using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIManager : MonoBehaviour {

    public static AIManager instance;
    //enemy prefabs
    public GameObject[] enemyPrefabs;
    public GameObject bossPrefab;
    private GameObject bos = null;
    private bool bosSpawned = false;
    //basic enemies
    public List<GameObject> basicEnemies;
    public Sprite[] basicEnemySprites;
    //bi enemies
    public List<GameObject> biEnemies;
    //rotating enemies
    public List<GameObject> rotatingEnemies;

    //spawning variables
    public float spawnTimer = 5f;
    private float scrubMobsTimer = 60f;
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
        biEnemies = new List<GameObject>();
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
        //during mobs wave
        if (scrubMobsTimer > 0)
        {
            scrubMobsTimer -= Time.deltaTime;
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
                spawnTimer = 2.5f;
            }
            spawnTimer -= Time.deltaTime;
        }
        //else if timer is over but enemies still remain
        else if (!bosSpawned && basicEnemies.Count == 0 && rotatingEnemies.Count == 0 && biEnemies.Count == 0)
        {
            bosSpawned = true;
            bos = (GameObject)Instantiate(bossPrefab, new Vector2(cameraBounds.center.x, -10.2f), bossPrefab.transform.rotation);
            bos.GetComponent<Rigidbody2D>().velocity = transform.up * Time.deltaTime * 170f;
        }
        //else its boss fight
        else
        {
            if (bosSpawned)
            {
                if (bos.transform.position.y >= 0)
                {
                    bos.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    bos.GetComponent<Rigidbody2D>().isKinematic = true;
                    bos.transform.position = new Vector2(0, 0);
                }
            }

            //spawn the aliens thing also
            if (spawningAlien && spawnTimer <= 0)
            {
                spawnAlien();
            }
            else if (!spawningAlien && spawnTimer <= 0)
            {
                spawningAlien = true;
                //set pattern
                pattern = Random.Range(1, 4);
                alienLimit = Random.Range(4, 11);
                spawnTimer = 2.5f;
            }
            spawnTimer -= Time.deltaTime;
        }
    }

    //spawn basic enemies
    private void spawnBasic()
    {
        if (basicEnemies.Count < 12)
        {
            if (leftLane)
            {
                spawnX = cameraBounds.center.x - 1.2f;
            }
            else
            {
                spawnX = cameraBounds.center.x + 1.2f;
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
        if (biEnemies.Count < 4)
        {
            biEnemies.Add((GameObject)Instantiate(enemyPrefabs[1], new Vector2(cameraBounds.center.x, cameraBounds.min.y), Quaternion.identity));
        }
    }

    //spawn rotating enemy
    private void spawnRotating()
    {
        if (rotatingEnemies.Count < 10)
        {
            if (leftLane)
            {
                spawnX = cameraBounds.center.x - 1.2f;
            }
            else
            {
                spawnX = cameraBounds.center.x + 1.2f;
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
