using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIManager : MonoBehaviour {

    public static AIManager instance;

    public GameObject[] enemyPrefabs;

    private List<GameObject> basicEnemies;

    private float spawnTimer = 5f;

    //camera
    Camera camera;
    public Bounds cameraBounds;


	//Start
	void Start()
    {
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
            if (basicEnemies.Count < 5)
            {
                Instantiate(enemyPrefabs[0], new Vector2(cameraBounds.min.x, cameraBounds.center.y), Quaternion.identity);
            }
            spawnTimer = 5f;
        }

        spawnTimer -= Time.deltaTime;
	}
}
