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

    //rotating enemies
    //bi enemies

    private float spawnTimer = 5f;

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
            spawnTimer = 5f;
        }

        spawnTimer -= Time.deltaTime;
	}

    //spawn basic enemies
    private void spawnBasic()
    {
        if (basicEnemies.Count < 5)
        {
            basicEnemies.Add((GameObject)Instantiate(enemyPrefabs[0], new Vector2(cameraBounds.center.x, cameraBounds.min.y), Quaternion.identity));
            basicEnemies[basicEnemies.Count-1].GetComponent<SpriteRenderer>().sprite = basicEnemySprites[Random.Range(0, 4)];
        }
    }
}
