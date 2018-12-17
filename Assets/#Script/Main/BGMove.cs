using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMove : MonoBehaviour {

    public float speed;
    public float currentSpeed;

    private void Start()
    {
        currentSpeed = speed;
    }

    void FixedUpdate () {
        transform.position -= new Vector3(currentSpeed * Time.deltaTime, 0);
        if (transform.position.x < -10) transform.position += new Vector3(18, 0);
	}
}
