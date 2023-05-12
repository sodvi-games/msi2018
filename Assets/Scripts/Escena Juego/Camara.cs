using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour {

	void Start()
	{
		Camera.main.aspect = 1920f / 900f;
	}

}
