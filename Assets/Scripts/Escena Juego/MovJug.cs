using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovJug : MonoBehaviour
{

	public float vel = 15f; // Aumentado un poco para compensar el Time.deltaTime
	public static float rj, gj, bj, raj, gaj, baj;
	private float TiempoCC;
	private const float CC = 0.01f;
	private MeshRenderer Mat;
	public GameObject Cil;
	public GameObject[] Esfe = new GameObject[3];

	public GameObject Bala;
	private int E = 0;
	[SerializeField] private float CBalas = 50;
	[SerializeField] private float Trec = 0.5f;
	private const float Rec = 0.1f; // Quitamos Serialized y ajustamos tiempo de disparo

	private float limite_pantalla = 16.5f;

	[SerializeField] private float Trell = 0;
	[SerializeField] private float Rell = 0.1f;

	public bool EnPelea;

	public Animator anim;
	public static int vida = 100; // Inicializar vida por seguridad

	void Awake()
	{
		// Inicializamos componentes en Awake para evitar errores de Null en Start
		anim = GetComponent<Animator>();
		if (Cil != null) Mat = Cil.GetComponent<MeshRenderer>();
	}

	void Start()
	{
		rj = Random.Range(0.0f, 1.0f);
		gj = Random.Range(0.0f, 1.0f);
		bj = Random.Range(0.0f, 1.0f);
		raj = rj;
		gaj = gj;
		baj = bj;

		if (Mat != null)
			Mat.material.SetColor("_MKGlowTexColor", new Color(raj, gaj, baj));
	}

	void Update()
	{
		if (!MenuPausa.EnPausa)
		{
			ManejarColores();
			ManejarMovimiento();
			ManejarDisparo();
			ManejarEstadoVida();
		}
	}

	void ManejarColores()
	{
		if (TiempoCC > CC)
		{
			ConEspectro.VariarColor(ref raj, ref rj);
			ConEspectro.VariarColor(ref gaj, ref gj);
			ConEspectro.VariarColor(ref baj, ref bj);
			if (Mat != null) Mat.material.SetColor("_MKGlowTexColor", new Color(raj, gaj, baj));
			TiempoCC -= CC;
		}
		TiempoCC += Time.deltaTime;
	}

	void ManejarMovimiento()
	{
		float moveX = Input.GetAxis("Horizontal");
		transform.Translate(Vector3.right * moveX * vel * Time.deltaTime);

		// Teletransporte/Loop de pantalla
		if (transform.position.x < -30) transform.position = new Vector3(60 + transform.position.x, transform.position.y, transform.position.z);
		else if (transform.position.x > 30) transform.position = new Vector3(-60 + transform.position.x, transform.position.y, transform.position.z);

		EnPelea = (transform.position.x > -limite_pantalla && transform.position.x < limite_pantalla);
	}

	void ManejarDisparo()
	{
		// Lógica de disparo
		if (CBalas > 0 && Input.GetButton("Fire1"))
		{
			if (Trec >= Rec)
			{
				if (EnPelea)
				{
					Trec = 0;
					Trell = 0;
					CBalas--;

					if (E >= Esfe.Length) E = 0;

					// Instanciamos y coloreamos la bala
					GameObject nuevaBala = Instantiate(Bala, new Vector3(Esfe[E++].transform.position.x, transform.position.y, -2.31f), Quaternion.identity);
					nuevaBala.GetComponent<MeshRenderer>().material.SetColor("_MKGlowColor", new Color(raj, gaj, baj));

					// FIX: Error del Pitch. Verificamos que BSonido exista antes de llamar
					if (MenuPausa.BSonido != null)
					{
						MenuPausa.BSonido.CambiarPitch("D", CBalas);
						MenuPausa.BSonido.Play("D");
					}
				}
			}
			else Trec += Time.deltaTime;
		}
		else
		{ // Recarga
			if (Trell >= Rell && CBalas < 50)
			{
				CBalas++;
				Trell = 0; // Reiniciamos el contador de recarga
				Rell = Input.GetButton("Fire1") ? 0.1f : 0.01f;
			}
			else if (Trell < Rell) Trell += Time.deltaTime;
		}

		// Actualizamos brillo de las esferas (Armas)
		foreach (GameObject esfera in Esfe)
		{
			if (esfera != null)
				esfera.GetComponent<MeshRenderer>().material.SetFloat("_MKGlowPower", 0.05f * CBalas);
		}
	}

	void ManejarEstadoVida()
	{
		if (anim != null) anim.SetInteger("Vida", vida);

		if (vida <= 0)
		{
			// Verificamos si la animación de destrucción ya terminó
			if (anim != null && anim.GetCurrentAnimatorStateInfo(0).IsName("Destruido"))
			{
				MenuPausa.EnJuego = false;
				Destroy(gameObject);
			}
		}
	}
}