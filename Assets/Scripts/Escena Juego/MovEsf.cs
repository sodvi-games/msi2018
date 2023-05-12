using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovEsf : MonoBehaviour {

	private float vel = 1;
	private float velx, vely, y, x, dis, tiem;

	// Use this for initialization
	void Start () {
		x = Random.Range(-14.0f, 14.0f);
		y = Random.Range(0.9f, 5.5f);
		this.transform.position = new Vector3(x, y,this.transform.position.z);
		x = 14;
		if (Random.Range(-1, 1) == 0) x *= -1;
		vel = Random.Range(2.0f, 7.0f);
		dis = Mathf.Sqrt(Mathf.Pow(y - this.transform.position.y, 2) + 784);
		tiem = dis / vel;
		velx = (x - this.transform.position.x) / tiem;
		vely = (y - this.transform.position.y) / tiem;
	}
	
	// Update is called once per frame
	void Update () {
		if (!MenuPausa.EnPausa)
		{
			if (this.transform.position.x >= -14 && this.transform.position.x <= 14)
			{
				this.transform.Translate(Time.deltaTime * velx, Time.deltaTime * vely, 0);
			}
			else
			{
				SacarVel();
			}
		}
	}

	private void SacarVel()
	{
		this.transform.position = new Vector3(x, y, this.transform.position.z);
		y = Random.Range(0.9f, 5.5f);
		x *= -1;
		vel = Random.Range(2.0f, 7.0f);
		dis = Mathf.Sqrt(Mathf.Pow(y - this.transform.position.y, 2) + 784);
		tiem = dis / vel;
		velx = (x - this.transform.position.x) / tiem;
		vely = (y - this.transform.position.y) / tiem;
	}


}
