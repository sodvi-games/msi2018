using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public GameObject[] Power;
    private Transform[] cubos;
    int e,golpes;
    float vx, vy;
    float ang;

    void Start()
    {
        SacarVel(0.0f, 360.0f);
        e = Random.Range(0, Power.Length);
        GameObject creado = Instantiate(Power[e], transform.position, new Quaternion(0, 0, 0, 0));
        creado.transform.SetParent(transform);
        cubos = creado.GetComponentsInChildren<Transform>();
        switch (e)
        {
            case 0:
                for (int i = 1; i < cubos.Length; i++)
                {
                    cubos[i].GetComponent<MeshRenderer>().material.SetColor("_MKGlowColor", Color.green);
                }
                gameObject.AddComponent<BoxCollider2D>().size = new Vector2(0.2f, 0.6f);
                gameObject.AddComponent<BoxCollider2D>().size = new Vector2(0.6f, 0.2f);
                
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 14)
        {
            transform.position = new Vector3(14, transform.position.y, -2.31f);
            SacarVel(120.0f, 240.0f);
        }
        if (transform.position.x < -14)
        {
            transform.position = new Vector3(-14, transform.position.y, -2.31f);
            SacarVel(-60, 60);
        }
        if (transform.position.y > 6)
        {
            transform.position = new Vector3(transform.position.x, 6, -2.31f);
            SacarVel(210, 330);
        }
        if (transform.position.y < -7)
        {
            Destroy(gameObject);
        }
        if (!MenuPausa.EnPausa) transform.Translate(Time.deltaTime * vx, Time.deltaTime * vy, 0);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Bala"){
            golpes++;
            Color colorN = Color.green;
            if (golpes == 1) {
                colorN = Color.yellow;
            }
            if (golpes == 2)
            {
                colorN = Color.red;
            }

            for (int i = 1; i < cubos.Length; i++)
            {
                cubos[i].GetComponent<MeshRenderer>().material.SetColor("_MKGlowColor", colorN);
            }

            if (golpes == 3)
            {
                MovJug.vida += 10;
                MenuPausa.BSonido.Play("Toma");
                Destroy(gameObject);
            }
        }
    }

    void SacarVel(float min,float max)
    {
        ang = Random.Range(min, max);
        vx = 3 * Mathf.Cos((ang / 180) * Mathf.PI);
        vy = 3 * Mathf.Sin((ang / 180) * Mathf.PI);
    }

}
