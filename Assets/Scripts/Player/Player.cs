using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	Camera camera;
	public int playerNo = 1;

	float moveSpeed = 1f;
	int health = 10;

	Bounds cameraBounds;
	Sprite sprite;
	// Use this for initialization
	void Start () {
		camera = GameObject.Find ("MainCamera").GetComponent<Camera> ();
		float screenAspect = (float)Screen.width / (float)Screen.height;
		float cameraHeight = camera.orthographicSize * 2;
		cameraBounds = new Bounds (camera.transform.position, new Vector3 (cameraHeight * screenAspect, cameraHeight, 0));
		sprite = GetComponent<SpriteRenderer> ().sprite;
	}
	
	// Update is called once per frame
	void Update () {
		MovementInput ();
	}

	void MovementInput() {
		if (playerNo == 1) {
			if (Input.GetButton ("LeftP1") && transform.position.x + sprite.bounds.min.x > cameraBounds.min.x) {
				transform.position += transform.right * -1f * moveSpeed * 0.1f;
			}
			if (Input.GetButton("RightP1") && transform.position.x + sprite.bounds.max.x < cameraBounds.max.x) {
				transform.position += transform.right * moveSpeed * 0.1f;
			}
		} else {
			if (Input.GetButton ("LeftP1") && transform.position.x + sprite.bounds.min.x > cameraBounds.min.x) {
				transform.position += transform.right * -1f * moveSpeed * 0.1f;
			}
			if (Input.GetButton("RightP1") && transform.position.x + sprite.bounds.max.x < cameraBounds.max.x) {
				transform.position += transform.right * moveSpeed * 0.1f;
			}
		}

	}

	public void GetDamage(int dmg) {

		health -= dmg;
	}
}
