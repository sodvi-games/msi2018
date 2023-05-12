using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corazon : MonoBehaviour {

	public MeshRenderer[] CCora;

	public GameObject VidaM;

	void Start() {
		CCora = GetComponentsInChildren<MeshRenderer>();
		for (int j = 0; j < CCora.Length; j++)
		{
			CCora[j].material.SetColor("_MKGlowColor", Color.green);
		}
		VidaM.GetComponent<ConjPan>().DetNum(100);
		VidaM.GetComponent<ConjPan>().PonerColor(Color.green);
	}



}
