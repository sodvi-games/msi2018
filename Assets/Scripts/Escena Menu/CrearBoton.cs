using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Añadido por si usas TextMeshPro

public class CrearBoton : MonoBehaviour
{
    public GameObject botonPrefab;
    private string nom;

    // Eliminamos el array de 'botones' y el Update, ya que no es eficiente
    // y el posicionamiento se debe manejar con Layout Groups en el UI.

    void Start()
    {
        // Limpiar el contenedor antes de crear (por si acaso)
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Boton")) Destroy(child.gameObject);
        }

        for (int i = 0; i < Canciones.NomClips.Length; i++)
        {
            if (string.IsNullOrEmpty(Canciones.NomClips[i])) continue;

            GameObject creado = Instantiate(botonPrefab);
            creado.tag = "Boton";

            // Configuración del Transform
            creado.transform.SetParent(transform, false); // El 'false' mantiene la escala local correcta
            creado.transform.localRotation = Quaternion.identity;
            creado.transform.localScale = Vector3.one;

            // Procesar nombre (quitando ruta y extensión .wav)
            // Nota: El Remove(0,7) asume que la ruta empieza con algo fijo. 
            // Usar Path.GetFileNameWithoutExtension es más seguro en Unity 6.
            nom = System.IO.Path.GetFileNameWithoutExtension(Canciones.NomClips[i]);

            // Intentar asignar texto (Compatible con Text tradicional y TextMeshPro)
            var textoLegacy = creado.GetComponentInChildren<Text>();
            if (textoLegacy != null) textoLegacy.text = nom;
            else
            {
                var textoTMP = creado.GetComponentInChildren<TextMeshProUGUI>();
                if (textoTMP != null) textoTMP.text = nom;
            }

            // Configurar el botón
            Button btnComponent = creado.GetComponent<Button>();
            if (btnComponent != null)
            {
                ColocarParametro(btnComponent, i);
            }
        }
    }

    // El Update se elimina. Para que los botones se vean bien, 
    // usa un componente "Vertical Layout Group" en el objeto que tiene este script.

    void ColocarParametro(Button b, int h)
    {
        // Limpiamos listeners previos para evitar ejecuciones dobles
        b.onClick.RemoveAllListeners();
        b.onClick.AddListener(() => MenuPausa.IniciarJuego(h));
    }
}