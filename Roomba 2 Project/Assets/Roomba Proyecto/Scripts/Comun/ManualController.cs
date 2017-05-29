using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SendMessage("girarIzquierda", SendMessageOptions.DontRequireReceiver);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            SendMessage("girarDerecha", SendMessageOptions.DontRequireReceiver);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            SendMessage("avanzar", SendMessageOptions.DontRequireReceiver);
        }


    }
}
