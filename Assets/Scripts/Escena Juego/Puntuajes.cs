using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puntuajes : MonoBehaviour {

	public GameObject MarcadorG;

	public int cantidad;

	private float tiempoD;

	public Color CM;

	// Use this for initialization
	void Start () {
		GetComponent<ConjPan>().DetNum(cantidad);
		GetComponent<ConjPan>().PonerColor(CM);
		MarcadorGlobal.Marcador += cantidad;
		MarcadorG = GameObject.FindGameObjectWithTag("Mar");
		MarcadorG.GetComponent<ConjPan>().DetNum(MarcadorGlobal.Marcador);
	}
	
	void Update () {
		if (tiempoD > 1.0f) Destroy(gameObject);
		else tiempoD += Time.deltaTime;
	}

}
