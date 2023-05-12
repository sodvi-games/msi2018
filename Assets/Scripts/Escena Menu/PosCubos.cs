using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosCubos : MonoBehaviour
{

    public GameObject cubo;
    private GameObject creado;
    private float x, y, z, angx,angy,angz, r,rd;

    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < 30;i++)
        {
            r = Random.Range(5.0f, 6.0f);
            z = Random.Range(-r, r);
            rd = Mathf.Sqrt(Mathf.Pow(r, 2) - Mathf.Pow(z, 2));
            x = Random.Range(-rd, rd);
            y = Mathf.Sqrt(-Mathf.Pow(x, 2) - Mathf.Pow(z, 2) + Mathf.Pow(r, 2));
            if (Random.Range(0, 2) == 0) y *= -1;
            angy = (Mathf.Atan(x / z) * 180) / Mathf.PI;
            angx = (Mathf.Atan(Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(z, 2)) / y)*180)/Mathf.PI;
            creado = Instantiate(cubo, new Vector3(x, y, z), new Quaternion(0, 0, 0, 0));
            if (z < 0) angx *= -1;
            creado.transform.eulerAngles = new Vector3(angx + 90, angy, 0);
        }
    }

}
