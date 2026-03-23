using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{

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
	public GameObject PowerUp;

	private void Start()
	{
		// Obtenemos el color actual del espectro
		ColorN = new Color(ConEspectro.ra, ConEspectro.ga, ConEspectro.ba);

		// Coloreamos esferas con seguridad
		foreach (GameObject esfera in Esferas)
		{
			if (esfera != null)
				esfera.GetComponent<MeshRenderer>().material.SetColor("_MKGlowColor", ColorN);
		}

		if (CiliRot != null)
			CiliRot.GetComponent<MeshRenderer>().material.SetColor("_MKGlowTexColor", ColorN);

		TiemDis = Random.Range(1.0f, 5.0f);
	}

	private void Update()
	{
		if (MenuPausa.EnPausa) return;

		if (!muerto)
		{
			ManejarMovimientoYAparicion();
			ManejarDisparo();
		}
		else
		{
			ManejarMuerte();
		}
	}

	private void ManejarMovimientoYAparicion()
	{
		// Movimiento inicial de aparición
		if (TiemApa < 1.0f)
		{
			transform.Translate(Time.deltaTime * velx, Time.deltaTime * vely, 0);
			TiemApa += Time.deltaTime;
		}

		// Rotación visual
		if (CiliRot != null)
		{
			CiliRot.transform.eulerAngles = new Vector3(rot, 90, 90);
			rot += 40.0f * Time.deltaTime;
		}
	}

	private void ManejarDisparo()
	{
		if (TiemDisA < TiemDis)
		{
			TiemDisA += Time.deltaTime;
		}
		else
		{
			if (MenuPausa.EnJuego)
			{
				// Instanciamos la bala enemiga
				Vector3 posBala = new Vector3(transform.position.x, transform.position.y, -2.31f);
				GameObject bala = Instantiate(BalaE, posBala, Quaternion.identity);

				// Intentamos colorear la bala (buscando en hijos por si el prefab tiene el renderer dentro)
				var renderer = bala.GetComponentInChildren<MeshRenderer>();
				if (renderer != null) renderer.material.SetColor("_MKGlowColor", ColorN);
			}

			TiemDisA = 0;
			TiemDis = Random.Range(1.0f, 5.0f);
		}
	}

	private void ManejarMuerte()
	{
		Escala -= 2.0f * Time.deltaTime;
		if (Escala <= 0)
		{
			Destroy(gameObject);
		}
		else
		{
			transform.localScale = new Vector3(Escala, Escala, Escala);
		}
	}

	// Nota: Asegúrate de que las Balas tengan el Tag "Bala" y un Collider2D
	private void OnCollisionEnter2D(Collision2D col)
	{
		if (muerto) return;

		if (col.gameObject.CompareTag("Bala"))
		{
			golpes++;
			if (golpes >= 1)
			{
				Morir();
			}
		}
	}

	private void Morir()
	{
		muerto = true;

		// Desactivamos colisionador para que no reciba más golpes mientras se encoge
		if (TryGetComponent<CircleCollider2D>(out var col)) col.enabled = false;

		// Lógica de puntuación
		GameObject creado = Instantiate(Puntuador, transform.position, Quaternion.identity);
		if (creado.TryGetComponent<Puntuajes>(out var scriptPuntos))
		{
			scriptPuntos.cantidad = Random.Range(100, 999);

			// Probabilidad de PowerUp
			if (scriptPuntos.cantidad >= 980)
			{
				Instantiate(PowerUp, transform.position, Quaternion.identity);
				scriptPuntos.CM = Color.yellow;
				if (MenuPausa.BSonido != null) MenuPausa.BSonido.Play("Act");
			}
			else
			{
				scriptPuntos.CM = ColorN;
			}
		}

		Creador.CanET--;
	}
}