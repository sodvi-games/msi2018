using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaE : MonoBehaviour {

	private float velx, vely;
	private float m;
	private float ang;
	public float velB = 10;
	private float xa, ya;

	public static GameObject JugO;
	public static GameObject JugC;
	private GameObject Jug;

	public GameObject BalaEE;

	public Light Luz;

	public GameObject Mvida;

	// Use this for initialization
	void Start () {
		Mvida = GameObject.Find("Vida");
		Luz = BalaEE.GetComponentInChildren<Light>();
		JugO = GameObject.Find("Jugador");
		JugC = GameObject.Find("JugadorC");
		if (JugO.GetComponent<MovJug>().EnPelea) Jug = JugO;
		else Jug = JugC;
		Luz.color = new Color(ConEspectro.ra, ConEspectro.ga, ConEspectro.ba);
		if (Random.Range(0, 7) > 1)
		{
			if (Jug.transform.position.x - this.transform.position.x != 0)
			{
				m = (Jug.transform.position.y - this.transform.position.y) / (Jug.transform.position.x - this.transform.position.x);
				ang = (Mathf.Atan(m) * 180.0f) / Mathf.PI;
				BalaEE.transform.eulerAngles = new Vector3(0, 0, 90 + ang);
				ang = (ang / 180.0f) * Mathf.PI;
				velx = velB * Mathf.Cos(ang);
				vely = velB * Mathf.Sin(ang);
				if (vely > 0) vely *= -1;
				if (m > 0) velx *= -1;
			}
			else
			{
				velx = 0;
				vely = 5.0f;
				BalaEE.transform.eulerAngles = new Vector3(0, 0, 0);
			}
		}
		else {

			m = Random.Range(-10.0f,10.0f);
			ang = (Mathf.Atan(m) * 180.0f) / Mathf.PI;
			BalaEE.transform.eulerAngles = new Vector3(0, 0, 90 + ang);
			ang = (ang / 180.0f) * Mathf.PI;
			velx = velB * Mathf.Cos(ang);
			vely = velB * Mathf.Sin(ang);
			if (vely > 0) vely *= -1;
			if (m > 0) velx *= -1;
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (!MenuPausa.EnPausa)
		{

			this.transform.Translate(Time.deltaTime * velx, Time.deltaTime * vely, 0);

			if (this.transform.position.y < -8) Destroy(gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{

        if (!MenuPausa.EnJuego) Jug = null;

		if (col.collider.tag == "Jugador")
		{
			Destroy(gameObject);
			MovJug.vida--;
			Mvida.GetComponent<ConjPan>().DetNum(MovJug.vida);
		}
	}



}
