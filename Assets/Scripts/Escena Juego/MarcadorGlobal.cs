using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarcadorGlobal : MonoBehaviour {

	public static int Marcador;

	// Use this for initialization
	void Start () {
		GetComponent<ConjPan>().DetNum(0);
		GetComponent<ConjPan>().PonerColor(Color.white);
	}

}
