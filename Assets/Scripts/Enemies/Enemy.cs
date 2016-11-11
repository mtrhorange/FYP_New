using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public GameObject eLaser;

	internal bool weakened = false;
	private int HP = 1;

	private bool goLeft = false;

	Camera camera;
	Bounds cameraBounds;

	// Use this for initialization
	void Start () {

		camera = GameObject.Find("MainCamera").GetComponent<Camera>();

		float screenAspect = (float)Screen.width / (float)Screen.height;
		float cameraHeight = camera.orthographicSize * 2;
		cameraBounds = new Bounds (camera.transform.position, new Vector3 (cameraHeight * screenAspect, cameraHeight, 0));
	}

	// Update is called once per frame
	void Update () {
		if (goLeft)
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 0) * Time.deltaTime * 80f;
			if (transform.position.x < cameraBounds.min.x)
			{
				goLeft = false;
			}
		}
		else
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(1, 0) * Time.deltaTime * 80f;
			if (transform.position.x > cameraBounds.max.x)
			{
				goLeft = true;
			}
		}
	}


	//called when this entity receives damage from another source
	internal void GetDamage(int dmg)
	{
		HP -= dmg;
		//check die
		if (HP <= 0)
		{
			Destroy(this.gameObject);
		}
	}
}