using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour {

	private float velB = 5.0f;
	public Light Luz;

	// Use this for initialization
	void Start () {
		Luz = GetComponentInChildren<Light>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!MenuPausa.EnPausa)
		{
			transform.Translate(0, Time.deltaTime * velB, 0);

			if (transform.position.y > 8) Destroy(gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.collider.tag == "Enemigo" || col.collider.tag == "PowerUp") Destroy(gameObject);
	}
}
