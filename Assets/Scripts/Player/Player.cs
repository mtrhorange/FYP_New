using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	Camera camera;
	public Player otherPlayer;
	public int playerNo = 1;

	float moveSpeed = 1f;
	public int health = 10;
	bool canShoot = true;
	float shootTimer = 0;

	public GameObject laserPrefab;
    public GameObject explosionPrefab;
	public Sprite lvl1Ship;
	public Sprite lvl2Ship;
	Sprite sprite;
	GameObject forcefield;
	GameObject shield;

	//Powerups
	int noOfLaser = 1;
	float laserPowerTimer = 0;

	bool splitPowerup = false;
	float splitPowerTimer = 0;

	int laserDamage = 1;
	bool damagePowerup = false;
	float damagePowerTimer = 0;

	bool shieldPowerup = false;
	float shieldPowerTimer = 0;

	// Use this for initialization
	void Start () {
		sprite = GetComponent<SpriteRenderer> ().sprite;
		forcefield = transform.FindChild ("Forcefield").gameObject;
		shield = transform.FindChild ("Shield").gameObject;
		DisableShieldPowerup ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!canShoot) {
			shootTimer += Time.deltaTime;
			if (shootTimer >= 0.75f) {
				canShoot = true;
				shootTimer = 0;
			}
		}

		PowerupTimers ();

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
		

	    if (shield.activeSelf == false)
        {
            health -= dmg;
            UpdateForcefield();
        }
        else
	    {
	        UpdateShield(dmg);
	    }
	}

    void UpdateShield(int dmg)
    {
        Debug.Log("Shield Health: " + shield.GetComponent<Shield>().health);
        shield.GetComponent<Shield>().health -= dmg;
        if (shield.GetComponent<Shield>().health <= 0)
        {
            DisableShieldPowerup();
        }
    }


    void UpdateForcefield() {

		if (health > 1) {
			//TODO
			//Forcefield change colour from healthy to die(1hp)
		    if (health > 6)
		    {
                forcefield.GetComponent<SpriteRenderer>().color = new Color32(0,255,255,255);

            }else if (health > 3 && health < 7)
		    {
                forcefield.GetComponent<SpriteRenderer>().color = new Color32(255, 153, 0, 255);
            }else if(health > 1 && health < 4)
		    {
                forcefield.GetComponent<SpriteRenderer>().color = new Color32(255, 50, 0, 255);
            }
			forcefield.SetActive (true);
			GetComponent<CircleCollider2D> ().radius = 0.5f;
		}
		else if (health <= 1 && forcefield.activeSelf) { //FORCEFIELD DIE(GONE)
		    health = 1;
            Debug.Log("force shield off");
			forcefield.SetActive (false);
			GetComponent<CircleCollider2D> ().radius = 0.2f;
		}else if (health < 1 && !forcefield.activeSelf)
		{
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

	}

	public void Shoot() {
		if (noOfLaser == 1) {
			GameObject laser = (GameObject)Instantiate (laserPrefab, transform.position, transform.rotation);
			laser.GetComponent<PlayerLaser> ().damage = laserDamage;
		} else if (noOfLaser == 2 && !splitPowerup) {
			GameObject laser1 = (GameObject)Instantiate (laserPrefab, transform.position + transform.right * 0.1f, transform.rotation);
			GameObject laser2 = (GameObject)Instantiate (laserPrefab, transform.position + transform.right * -0.1f, transform.rotation);
			laser1.GetComponent<PlayerLaser> ().damage = laserDamage;
			laser2.GetComponent<PlayerLaser> ().damage = laserDamage;
		} else if (noOfLaser == 3 && !splitPowerup) {
			GameObject laser1 = (GameObject)Instantiate (laserPrefab, transform.position + transform.right * 0.2f, transform.rotation);
			GameObject laser2 = (GameObject)Instantiate (laserPrefab, transform.position + transform.right * -0.2f, transform.rotation);
			GameObject laser3 = (GameObject)Instantiate (laserPrefab, transform.position + transform.up * 0.1f, transform.rotation);
			laser1.GetComponent<PlayerLaser> ().damage = laserDamage;
			laser2.GetComponent<PlayerLaser> ().damage = laserDamage;
			laser3.GetComponent<PlayerLaser> ().damage = laserDamage;
		} else if (noOfLaser == 2 && splitPowerup) {
			GameObject laser1 = (GameObject)Instantiate (laserPrefab, transform.position + transform.right * 0.1f, Quaternion.Euler(new Vector3(0,0,transform.rotation.eulerAngles.z - 5)));
			GameObject laser2 = (GameObject)Instantiate (laserPrefab, transform.position + transform.right * -0.1f, Quaternion.Euler(new Vector3(0,0,transform.rotation.eulerAngles.z + 5)));
			laser1.GetComponent<PlayerLaser> ().damage = laserDamage;
			laser2.GetComponent<PlayerLaser> ().damage = laserDamage;
		} else {
			GameObject laser1 = (GameObject)Instantiate (laserPrefab, transform.position + transform.right * 0.2f, Quaternion.Euler(new Vector3(0,0,transform.rotation.eulerAngles.z - 5)));
			GameObject laser2 = (GameObject)Instantiate (laserPrefab, transform.position + transform.right * -0.2f, Quaternion.Euler(new Vector3(0,0,transform.rotation.eulerAngles.z + 5)));
			GameObject laser3 = (GameObject)Instantiate (laserPrefab, transform.position + transform.up * 0.1f, transform.rotation);
			laser1.GetComponent<PlayerLaser> ().damage = laserDamage;
			laser2.GetComponent<PlayerLaser> ().damage = laserDamage;
			laser3.GetComponent<PlayerLaser> ().damage = laserDamage;
		}
		canShoot = false;
	}

	void OnTriggerEnter2D(Collider2D other) {

		if (other.GetComponent<EnemyLaser> ()) {
			GetDamage (other.GetComponent<EnemyLaser> ().damage);
            Destroy(other.gameObject);
		}

        if (other.GetComponent<AlienEnemy>())
        {
            GetDamage(2);
            Destroy(other.gameObject);
        }

		if (other.GetComponent<Powerup> ()) {
			Debug.Log ("111");
			switch (other.GetComponent<Powerup>().type) {

			case Powerup.Types.Heal:
				HealPowerup ();
				Destroy (other.gameObject);
				break;
			case Powerup.Types.Laser:
				LaserPowerup ();
				Destroy (other.gameObject);
				break;
			case Powerup.Types.Shield:
				ShieldPowerup ();
				Destroy (other.gameObject);
				break;
			case Powerup.Types.Split:
				SplitPowerup ();
				Destroy (other.gameObject);
				break;
			}


		}
	}

	#region Powerups

	void LaserPowerup() {
		if (noOfLaser < 3) {
			noOfLaser++;
			laserPowerTimer = 0;

		}
	}

	void SplitPowerup() {

		if (!splitPowerup) {
			if (noOfLaser == 1)
				LaserPowerup ();
			splitPowerup = true;
		} else
			splitPowerTimer = 0;
	}

	void ShieldPowerup() {
		shield.SetActive (true);
		shield.GetComponent<Shield> ().health = 4;

	}

	public void DisableShieldPowerup() {
        shield.GetComponent<Shield>().health = 0;
        shield.SetActive(false);
    }

	void HealPowerup() {

		health = 10;
		UpdateForcefield ();
	}

	void PowerupTimers() {
		if (noOfLaser > 1) {
			laserPowerTimer += Time.deltaTime;
			if (laserPowerTimer >= 20f) {
				laserPowerTimer = 0;
				noOfLaser--;
			}
		}

		if (splitPowerup) {
			splitPowerTimer += Time.deltaTime;
			if (splitPowerTimer >= 20f) {
				splitPowerTimer = 0;
				splitPowerup = false;
			}
		}
	}

	#endregion

}
