using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	Camera camera;
	public int playerNo = 1;

	float moveSpeed = 1f;
	int health = 10;
	bool canShoot = true;
	float shootTimer = 0;

	public GameObject laser;
	Sprite sprite;
	// Use this for initialization
	void Start () {
		sprite = GetComponent<SpriteRenderer> ().sprite;
	}
	
	// Update is called once per frame
	void Update () {
		if (!canShoot) {
			shootTimer += Time.deltaTime;
			if (shootTimer >= 0.5f) {
				canShoot = true;
				shootTimer = 0;
			}
		}

		MovementInput ();
	}

	void MovementInput() {
		if (playerNo == 1) {
			if (Input.GetButton("UpP1") && transform.position.y + sprite.bounds.min.y > AIManager.instance.cameraBounds.min.y)
            {
				transform.position += transform.right * -1f * moveSpeed * 0.08f;
			}
            if (Input.GetButton("DownP1") && transform.position.y + sprite.bounds.max.y < AIManager.instance.cameraBounds.max.y)
            {
				transform.position += transform.right * moveSpeed * 0.08f;
			}
			if (!canShoot) {
				shootTimer += Time.deltaTime;
				if (shootTimer >= 0.5f) {
					canShoot = true;
					shootTimer = 0;
				}
			}
				
			if (Input.GetButton("ShootP1") && canShoot) {
				Shoot ();
			}
		} else {
			if (Input.GetButton("UpP2") && transform.position.y + sprite.bounds.min.y > AIManager.instance.cameraBounds.min.y)
            {
				transform.position += transform.right * moveSpeed * 0.08f;
			}
			if (Input.GetButton("DownP2") && transform.position.y + sprite.bounds.max.y < AIManager.instance.cameraBounds.max.y) {
				transform.position += transform.right * -1f * moveSpeed * 0.08f;
			}
			if (Input.GetButton ("ShootP2") && canShoot) {
				Shoot ();
			}
		}

	}

	public void GetDamage(int dmg) {
		health -= dmg;
	}

	public void Shoot() {
		Instantiate (laser, transform.position, transform.rotation);
		canShoot = false;
	}
}
