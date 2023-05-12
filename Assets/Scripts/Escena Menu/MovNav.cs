using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovNav : MonoBehaviour
{
    public float x, y, z;
    public float r;
    public float ang,vel;
	public GameObject cili;
	
	// Start is called before the first frame update
    void Start()
    {
        x = transform.position.x;
        y = transform.position.y;
        z = transform.position.z + r;
        vel = (2 * Mathf.PI) / 10;
    }

    // Update is called once per frame
    void Update()
    {
        ang += (Time.deltaTime * vel);

        transform.eulerAngles = new Vector3(0, -(ang *180)/Mathf.PI, 0);

        transform.position = new Vector3(r * Mathf.Sin(ang) + x, y, -r * Mathf.Cos(ang) + z);
	}
}
