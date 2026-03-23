using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public static bool EnPausa = false;
    public static bool EnJuego = false;

    [Header("Audio")]
    public AudioSource CanGlo;
    public AudioSource CanPau;
    public static ScriptAudio BSonido;

    [Header("UI Panels")]
    public GameObject PanelA;
    public GameObject Geosic;
    public GameObject Jugar;
    public GameObject Panel;
    public GameObject Conti;
    public GameObject PConti;
    public GameObject Sal;
    public GameObject PSal;
    public GameObject Rein;

    [Header("Player Prefab")]
    public GameObject Jug;

    private float AnimT = 0;
    private int num = 3;

    void Start()
    {
        // En Unity 6, FindFirstObjectByType es la versión moderna y más rápida
        BSonido = Object.FindFirstObjectByType<ScriptAudio>();

        if (BSonido == null)
        {
            Debug.LogError("No se encontró ScriptAudio en la escena. Asegúrate de que el objeto con ScriptAudio existe.");
        }
    }

    void Update()
    {
        // Detectar tecla Escape o P (puedes añadir las que quieras)
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) && EnJuego)
        {
            AlternarPausa();
        }

        // Validación de escena para mostrar fin de juego
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            // Cambiado .active (obsoleto) por .activeSelf
            if (EnJuego == false && Rein != null && !Rein.activeSelf)
            {
                MostrarFin();
            }
        }


    }

    public void PonerPausa()
    {
        EnPausa = true;
        Time.timeScale = 0f; // Sugerencia: Congela el tiempo del motor si es necesario
        CanGlo.Pause();
        CanPau.time = Random.Range(35.0f, 165.0f);
        CanPau.Play();

        ToggleMenuUI(true);
    }

    public void QuitarPausa()
    {
        // Quitamos la UI de botones
        ToggleMenuUI(false);

        PanelA.SetActive(true);
        if (PanelA.TryGetComponent<ConjPan>(out var conj)) conj.DetNum(3);

        // No ponemos timeScale = 1 aquí, dejamos que la corrutina lo haga al final
        StartCoroutine(Meh());
    }

    private void ToggleMenuUI(bool estado)
    {
        Conti.SetActive(estado);
        PConti.SetActive(estado);
        Sal.SetActive(estado);
        PSal.SetActive(estado);
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
        EnPausa = false;
        Time.timeScale = 1f;
        Canciones.CanSelec = NC;
        MovJug.vida = 100;
        SceneManager.LoadScene(1);
    }

    public void VolverAlmenu()
    {
        Time.timeScale = 1f;
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
        Time.timeScale = 1f; // Aseguramos que el tiempo corre
        MovJug.vida = 100;

        // Uso de elvis operator (?) para evitar NullReference si los objetos no existen
        GameObject.Find("Vida")?.GetComponent<ConjPan>()?.DetNum(MovJug.vida);
        GameObject.FindGameObjectWithTag("Mar")?.GetComponent<ConjPan>()?.DetNum(0);

        Creador.CanET = 0;
        CanGlo.Stop();
        CanGlo.Play();

        LimpiarEscena("Enemigo");
        LimpiarEscena("BalaE");

        Rein.SetActive(false);
        Sal.SetActive(false);
        PSal.SetActive(false);

        EnJuego = true;

        // Instanciar jugadores (Quaternion.identity es el 0,0,0 de rotación)
        CrearJugador("Jugador", new Vector3(0, -5.65f, -2.31f));
        CrearJugador("JugadorC", new Vector3(30.0f, -5.65f, -2.31f));
    }

    private void LimpiarEscena(string tag)
    {
        GameObject[] objetos = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject o in objetos) Destroy(o);
    }

    private void CrearJugador(string nombre, Vector3 pos)
    {
        GameObject creado = Instantiate(Jug, pos, Quaternion.identity);
        creado.name = nombre;
    }

    IEnumerator Meh()
    {
        num = 3;
        AnimT = 0; // Resetear al inicio
        while (num >= 0)
        {
            // IMPORTANTE: unscaledDeltaTime ignora el Time.timeScale = 0
            AnimT += Time.unscaledDeltaTime;

            if (AnimT > 1.0f / 3.0f)
            {
                if (num != 0)
                {
                    if (PanelA.TryGetComponent<ConjPan>(out var conj)) conj.DetNum(num);
                    if (BSonido != null) BSonido.Play("Beep");
                }
                else
                {
                    PanelA.SetActive(false);
                }

                AnimT = 0;
                num--;
            }
            yield return null;
        }

        // Solo quitamos la pausa física al terminar la cuenta
        Time.timeScale = 1f;
        EnPausa = false;
        CanPau.Stop();
        CanGlo.UnPause();
        num = 3;
    }

    public void Salir()
    {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // Esta función la puedes llamar desde un BOTÓN DE UI
    public void AlternarPausa()
    {
        if (!EnJuego) return;

        if (!EnPausa)
            PonerPausa();
        else
            QuitarPausa();
    }
}