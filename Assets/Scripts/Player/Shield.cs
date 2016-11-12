using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {

	public float health = 4f;
	Player player;
	// Use this for initialization
	void Start () {
		player = transform.parent.GetComponent<Player> ();
		UpdateShieldColor ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {

		if (other.GetComponent<EnemyLaser> ()) {
			GetDamage (other.GetComponent<EnemyLaser> ().damage);
			GameObject laserHit = (GameObject)Resources.Load ("LaserHit");
			laserHit.GetComponent<SpriteRenderer> ().color = other.GetComponent<SpriteRenderer> ().color;
			Instantiate (laserHit, other.transform.position, other.transform.rotation);
            Destroy(other.gameObject);
        }
	    if (other.GetComponent<AlienEnemy>())
	    {
            GetDamage(2);
            Destroy(other.gameObject);
        }
			
        
	}

	void GetDamage(int i) {
		health -= i;
		UpdateShieldColor ();
		if (health <= 0) {
			player.DisableShieldPowerup ();
		}
	}

	public void UpdateShieldColor() {
		if (health == 4)
			GetComponent<SpriteRenderer> ().color = new Color32 (62, 175, 255, 255);
		else if (health == 3)
			GetComponent<SpriteRenderer> ().color = new Color32 (255, 255, 0, 255);
		else if (health == 2)
			GetComponent<SpriteRenderer> ().color = new Color32 (255, 153, 0, 255);
		else
			GetComponent<SpriteRenderer> ().color = new Color32 (255, 37, 0, 150);

	}
}
