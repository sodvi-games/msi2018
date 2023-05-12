using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour {

	[HideInInspector]
	public float velx, vely;
	public float x, y;
	private float TiemApa;
	private float TiemDis;
	private float TiemDisA;
	private float rot;
	public Color ColorN;
	private int golpes = 0;

	private bool muerto = false;
	private float Escala = 1.0f;

	public GameObject[] Esferas;

	public GameObject CiliRot;

	public GameObject BalaE;

	public GameObject Puntuador;
	private GameObject creado;

    public GameObject PowerUp;

	// Use this for initialization
	void Start () {
		ColorN = new Color(ConEspectro.ra, ConEspectro.ga, ConEspectro.ba);
		for (int i = 0; i < Esferas.Length; i++)
		{
			Esferas[i].GetComponent<MeshRenderer>().material.SetColor("_MKGlowColor", ColorN);
		}
		CiliRot.GetComponent<MeshRenderer>().material.SetColor("_MKGlowTexColor", ColorN);
		TiemDis = Random.Range(1.0f, 5.0f);
	}

	// Update is called once per frame
	void Update()
	{
		if (!MenuPausa.EnPausa)
		{
			if (!muerto)
			{
				if (TiemApa < 1.0f)
				{
					transform.Translate(Time.deltaTime * velx, Time.deltaTime * vely, 0);
					TiemApa += Time.deltaTime;
				}
				CiliRot.transform.eulerAngles = new Vector3(rot, 90, 90);
				rot += 40.0f * Time.deltaTime;
				if (TiemDisA < TiemDis)
				{
					TiemDisA += Time.deltaTime;
				}
				else
				{
					if (MenuPausa.EnJuego) Instantiate(BalaE, new Vector3(transform.position.x, transform.position.y, -2.31f), new Quaternion(0, 0, 0, 0)).GetComponentInChildren<MeshRenderer>().material.SetColor("_MKGlowColor", ColorN);
					TiemDisA -= TiemDis;
					TiemDis = Random.Range(1.0f, 5.0f);
				}
			}
			else
			{
				if (transform.localScale.x < 0) Destroy(gameObject);
				Escala -= 2.0f * Time.deltaTime;
				this.transform.localScale = new Vector3(Escala, Escala, Escala);
			}
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Bala") golpes++;
		if (golpes == 1)
		{
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            creado = Instantiate(Puntuador, transform.position, new Quaternion(0, 0, 0, 0));
			creado.GetComponent<Puntuajes>().cantidad = Random.Range(100, 999);
            if (creado.GetComponent<Puntuajes>().cantidad >= 980)
            {
                Instantiate(PowerUp, transform.position, new Quaternion(0, 0, 0, 0));
                creado.GetComponent<Puntuajes>().CM = Color.yellow;
                MenuPausa.BSonido.Play("Act");
            }else creado.GetComponent<Puntuajes>().CM = ColorN;

            gameObject.GetComponent<CircleCollider2D>().enabled = false;

			muerto = true;
            Creador.CanET--;
		}
	}

}
