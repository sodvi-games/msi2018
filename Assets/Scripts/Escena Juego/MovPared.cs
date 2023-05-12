using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovPared : MonoBehaviour {

	static public float velobs = 1;

	// Update is called once per frame
	void Update()
	{
		if (!MenuPausa.EnPausa)
		{
			if (this.transform.position.y > 9) this.transform.position = new Vector3(0, -18 + this.transform.position.y, 0);

			this.transform.Translate(0, Time.deltaTime * velobs, 0);
		}
	}

}
