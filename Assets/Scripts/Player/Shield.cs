using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {

	public float health = 4f;
	Player player;
	// Use this for initialization
	void Start () {
		player = transform.parent.GetComponent<Player> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {

		if (other.GetComponent<EnemyLaser> ()) {
			GetDamage (other.GetComponent<EnemyLaser> ().damage);
		}
		if (other.GetComponent<Enemy> ()) 
			GetDamage (2);
	}

	void GetDamage(int i) {
		health -= i;
		if (health <= 0) {
			player.DisableShieldPowerup ();
		}
	}
}
