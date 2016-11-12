using UnityEngine;
using System.Collections;

public class Powerup : MonoBehaviour {

	public enum Types {Shield, Laser, Heal, Split}

    public Vector3 direction;
	public Types type;

	float lifespan = 20f;
	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody2D>().velocity = direction * Time.deltaTime * 100f;
	}
	
	// Update is called once per frame
	void Update () {
		lifespan -= Time.deltaTime;
		if (lifespan <= 0)
			Destroy (gameObject);

		if (transform.position.x < AIManager.instance.cameraBounds.min.x) {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (GetComponent<Rigidbody2D> ().velocity.x * -1f, GetComponent<Rigidbody2D> ().velocity.y);
			transform.position += transform.right * 0.2f;
		}
		if (transform.position.x > AIManager.instance.cameraBounds.max.x) {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (GetComponent<Rigidbody2D> ().velocity.x * -1f, GetComponent<Rigidbody2D> ().velocity.y);
			transform.position += transform.right * -0.2f;
		}
		if (transform.position.y < AIManager.instance.cameraBounds.min.y) {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (GetComponent<Rigidbody2D> ().velocity.x, GetComponent<Rigidbody2D> ().velocity.y * -1f);
			transform.position += transform.up * 0.2f;
		}
		if (transform.position.y > AIManager.instance.cameraBounds.max.y) {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (GetComponent<Rigidbody2D> ().velocity.x, GetComponent<Rigidbody2D> ().velocity.y * -1f);
			transform.position += transform.up * -0.2f;
		}
	}
}
