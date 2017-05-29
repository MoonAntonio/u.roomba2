using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class actions : MonoBehaviour {


public void girarDerecha()
    {
        transform.Rotate(new Vector3(0, 90, 0));
        //transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, 90+transform.rotation.y,transform.rotation.y));
        //rotar en un punto especifico   transform.RotateAround
        SendMessage("accionrealizada",1,SendMessageOptions.DontRequireReceiver);

        //es un ejemplo para buscar un objeto en la escena y ejecutar esa funcion
        //GameObject sonido = GameObject.FindGameObjectWithTag("sound");
        //if(sonido)sonido.SendMessage
        

    }

    public void girarIzquierda()
    {
        transform.Rotate(new Vector3(0, -90, 0));
        SendMessage("accionrealizada", 1, SendMessageOptions.DontRequireReceiver);
    }

    public void avanzar()
    {
        SendMessage("accionrealizada", 1, SendMessageOptions.DontRequireReceiver);

        transform.localPosition += transform.forward;
    }

    public void apagar()
    {
        GetComponent<Percepts>().encendido = false;
        SendMessage("accionrealizada", 1, SendMessageOptions.DontRequireReceiver);
    }
    public void aspirar()
    {
        RaycastHit rayobajo;
        
        if (Physics.Raycast(transform.position, transform.up * -1, out rayobajo, 0.5f))
        {
            //detecta  el obstaculo
            Destroy(rayobajo.collider.gameObject);
            SendMessage("SuciedadAspirada", 1, SendMessageOptions.DontRequireReceiver);

        }
        else
        {
            
        }

        SendMessage("accionrealizada", 1, SendMessageOptions.DontRequireReceiver);




    }
    void Update()
    {
      
    }


}
