using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{

	public static bool EnPausa = false;
	public static bool EnJuego = false;
	public AudioSource CanGlo;
	public AudioSource CanPau;
	public GameObject PanelA;
    public GameObject Geosic;
    public GameObject Jugar;
    public GameObject Panel;

	private float AnimT = 0;
	private float AnimTC;
	private int num = 3;
	public static ScriptAudio BSonido;

	public GameObject Conti;
    public GameObject PConti;
    public GameObject Sal;
    public GameObject PSal;
    public GameObject Rein;

    public GameObject Jug;

    void Start()
	{
		BSonido = FindObjectOfType<ScriptAudio>();
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && EnJuego)
        {

            if (!EnPausa) PonerPausa();
            else
            {
                QuitarPausa();
            }

        }
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (EnJuego == false && !Rein.active) MostrarFin();
        }
    }

	public void PonerPausa()
	{
		EnPausa = true;
		CanGlo.Pause();
		CanPau.time = Random.Range(35.0f, 165.0f);
		CanPau.Play();
		Conti.SetActive(true);
        PConti.SetActive(true);
		Sal.SetActive(true);
        PSal.SetActive(true);
	}

	public void QuitarPausa()
	{
		PanelA.SetActive(true);
		PanelA.GetComponent<ConjPan>().DetNum(3);
		AnimT = 0.28f;
		Conti.SetActive(false);
        PConti.SetActive(false);
		Sal.SetActive(false);
        PSal.SetActive(false);
        StartCoroutine(Meh());
	}

    public void MostrarLista()
    {
        Geosic.SetActive(false);
        Jugar.SetActive(false);
        Panel.SetActive(true);
    }

    public static void IniciarJuego(int NC)
    {
        EnJuego = true;
        Canciones.CanSelec = NC;
        MovJug.vida = 100;
        SceneManager.LoadScene(1);
    }

    public void VolverAlmenu()
    {
        EnJuego = false;
        EnPausa = false;
        MovJug.vida = 100;
        Creador.CanET = 0;
        MarcadorGlobal.Marcador = 0;
        SceneManager.LoadScene(0);
    }

    public void MostrarFin()
    {
        Sal.SetActive(true);
        PSal.SetActive(true);
        Rein.SetActive(true);
    }

    public void Reiniciar()
    {
        MovJug.vida = 100;
        GameObject.Find("Vida").GetComponent<ConjPan>().DetNum(MovJug.vida);
        MarcadorGlobal.Marcador = 0;
        GameObject.FindGameObjectWithTag("Mar").GetComponent<ConjPan>().DetNum(MarcadorGlobal.Marcador);
        Creador.CanET = 0;
        CanGlo.Stop();
        CanGlo.Play();
        GameObject[] enemigos;
        enemigos = GameObject.FindGameObjectsWithTag("Enemigo");
        foreach (GameObject c in enemigos)
        {
            Destroy(c);
        }
        GameObject[] BalasE;
        BalasE = GameObject.FindGameObjectsWithTag("BalaE");
        foreach (GameObject c in BalasE)
        {
            Destroy(c);
        }

        Rein.SetActive(false);
        Sal.SetActive(false);
        PSal.SetActive(false);
        MarcadorGlobal.Marcador = 0;

        EnJuego = true;

        GameObject creado = Instantiate(Jug);
        creado.transform.position = new Vector3(0, -5.65f, -2.31f);
        creado.name = "Jugador";

        creado = Instantiate(Jug);
        creado.transform.position = new Vector3(30.0f, -5.65f, -2.31f);
        creado.name = "JugadorC";

    }

	IEnumerator Meh()
	{
		while (num != -1)
		{
			AnimT += Time.deltaTime;
			if (AnimT > 1.0f / 3.0f)
			{
				if (num != 0)
				{
					PanelA.GetComponent<ConjPan>().DetNum(num);
					BSonido.Play("Beep");
				}
				else PanelA.SetActive(false);
				AnimT -= (1.0f / 3.0f);
				num--;
			}

			yield return null;
		}
		EnPausa = false;
		CanPau.Stop();
		CanGlo.Play();
		num = 3;
	}
    public void Salir()
    {
        Application.Quit();
    }

}
