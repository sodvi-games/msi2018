using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovJug : MonoBehaviour {

	// public KeyCode dere, izq, Dis;
	public float vel = 2;
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
	[SerializeField] private const float Rec = 0.01f;

	
	private float limite_pantalla = 16.5f;

	[SerializeField] private float Trell = 0;
	[SerializeField] private float Rell = 0.1f;

	public bool EnPelea;

    public Animator anim;
    public static int vida;

	void Start () {
		rj = Random.Range(0.0f, 1.0f);
		gj = Random.Range(0.0f, 1.0f);
		bj = Random.Range(0.0f, 1.0f);
		raj = rj;
		gaj = gj;
		baj = bj;
		rj = Random.Range(0.0f, 1.0f);
		gj= Random.Range(0.0f, 1.0f);
		bj = Random.Range(0.0f, 1.0f);
		Mat = Cil.GetComponent<MeshRenderer>();
		Mat.material.SetColor("_MKGlowTexColor", new Color(raj, gaj, baj));
        anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update() {
		if (!MenuPausa.EnPausa) { // siempre y cuando no esté en pausa
			if (TiempoCC > CC) { // si la variación de color está en CC
				ConEspectro.VariarColor(ref raj, ref rj);
				ConEspectro.VariarColor(ref gaj, ref gj);
				ConEspectro.VariarColor(ref baj, ref bj);
				Mat.material.SetColor("_MKGlowTexColor", new Color(raj, gaj, baj));
				TiempoCC -= CC;
			}
			TiempoCC += Time.deltaTime;

			// nos movemos gracias al eje X de cualquier mando, teclado.
			transform.Translate(Time.deltaTime * vel * Input.GetAxis("Horizontal"), 0, 0);

			// Hay una traslación necesaria para que cambie de objeto jugable
			if (transform.position.x < -30) transform.position = new Vector3(60 + transform.position.x, transform.position.y, transform.position.z);
			else if (transform.position.x > 30) transform.position = new Vector3(-60 + transform.position.x, transform.position.y, transform.position.z);

			if (CBalas > 0 && Input.GetButton("Fire1")) { // si tenemos balas y disparamos
				if (Trec >= Rec) { // si el cc del disparo es mayor al anterior disparo
					Trec = 0; // reiniciamos el cc
					Trell = 0;
					if (transform.position.x > -limite_pantalla && transform.position.x < limite_pantalla) { // si estamos dentro del límite de la pantalla
					 	// reducimos las balas restantes, solo si se generó una bala
						CBalas--;
						// regresamos a 0 el apuntador del arma
						if (E >= 3) E = 0;
						// creamos y coloreamos una bala en la posición E, y la iteramos
						Instantiate(Bala, new Vector3(Esfe[E++].transform.position.x, transform.position.y, -2.31f), Quaternion.identity).GetComponent<MeshRenderer>().material.SetColor("_MKGlowColor", new Color(raj, gaj, baj));
						// cada bala sale desde la posición de la esfera arma en x, la posición del jugador en y una falla en z
						// se supone que cambia el pitch al disparar para no molestar al jugador, pero da error .-.
						MenuPausa.BSonido.CambiarPitch("D", CBalas);
						MenuPausa.BSonido.Play("D");
					}
				}
				else Trec += Time.deltaTime; // si estás en CC tendrás que esperar
			} else { // no estás disparando, o no tienes balas
				if (Trell >= Rell && CBalas < 50) { // tus balas están por debajo de 50 y puedes recargar
					CBalas++; // se aumentan tus balas disponibles
					Trell -= Rell; // se reduce la recarga
					if (Input.GetButton("Fire1")) Rell = 0.1f; // intentas disparar, y recargas lento
					else Rell = 0.01f; // si no, recargas rápido
				}
				else if (Trell < Rell) Trell += Time.deltaTime; // si no estás disparando, y la recarga no está disponible esperas a que carge¿
			}

			// dependiendo de tu posición puedes o no disparar
			EnPelea = (transform.position.x > - limite_pantalla && transform.position.x < limite_pantalla) ? true : false;

			foreach (GameObject esfera in Esfe) // iluminamos las esferas de acuerdo a su contador de balas
				esfera.GetComponent<MeshRenderer>().material.SetFloat("_MKGlowPower", 0.05f * CBalas);
		}

        anim.SetInteger("Vida", vida);

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Destruido") && vida <= 0) {
            MenuPausa.EnJuego = false;
            Destroy(gameObject);
        }

	}
}
