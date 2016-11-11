using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour
{

    public Sprite lastFrame;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (GetComponent<SpriteRenderer>().sprite == lastFrame)
	    {
	        Destroy(gameObject);
	    }
	}
}
