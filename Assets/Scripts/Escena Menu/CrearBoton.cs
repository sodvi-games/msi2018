using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrearBoton : MonoBehaviour
{

    public GameObject boton;
    private Button BCreado;
    private string nom;
    private GameObject[] botones;

    void Start()
    {
        for (int i = 0; i < Canciones.NomClips.Length; i++)
        {
            GameObject creado = Instantiate(boton);
            creado.tag = "Boton";
            creado.transform.SetParent(transform);
            creado.transform.localRotation = Quaternion.identity;
            creado.transform.localScale = Vector3.one;
            nom = Canciones.NomClips[i];
            nom = nom.Remove(0, 7);
            nom = nom.Remove(nom.Length - 4, 4);
            creado.GetComponentInChildren<Text>().text = nom;       
            BCreado = creado.GetComponent<Button>();
            ColocarParametro(BCreado, i);
            creado.transform.position = new Vector3(0, 0, 0);
        }
    }

    void Update(){
        botones = GameObject.FindGameObjectsWithTag("Boton");
        foreach( GameObject boton in botones){
            boton.transform.localPosition = new Vector3(boton.transform.localPosition.x,boton.transform.localPosition.y,0);
            Debug.Log("boton: " + boton.transform.position);
        }
    }

    void ColocarParametro(Button b, int h)
    {
        b.onClick.AddListener(() => MenuPausa.IniciarJuego(h));
    }
}
