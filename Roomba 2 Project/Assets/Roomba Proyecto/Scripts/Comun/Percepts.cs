using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Percepts : MonoBehaviour {

    public bool encendido = true;

    public Vector2 currentposition;// (x,y)
    public Vector3 sensores;//(t,d,h)
                            //t--> vale 1 si el agente choca con algo de frente, 0 si no hay ningún obstáculo
                            //d--> vale 1 si hay suciedad en la localización del agente
                            //h--> vale 1 si el agente está en su base

    public int rotacion;
    public LayerMask dondeestabasura;
    public LayerMask dodneestalabase;
    public Vector2 Homeposition;

    


    void Start () {
        currentposition = Vector2.zero;
        sensores = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
        currentposition.x =Mathf.RoundToInt( transform.position.x);
        currentposition.y = Mathf.RoundToInt(transform.position.z);
        rotacion = (int)transform.rotation.eulerAngles.y;
        //sensores

        RaycastHit rayodelante;
        RaycastHit rayobajo;
            
        Debug.DrawRay(transform.position, transform.forward, Color.red);
        //el 0 es la duracion osea en cada fotograma se pinta y se borra
        Debug.DrawRay(transform.position, transform.up*-1, Color.yellow);

        //rayo que detecta la base
        Debug.DrawRay(transform.position, transform.up * -1, Color.green);

        //Rayo tira recto para detectar obstaculos
        if ( Physics.Raycast(transform.position, transform.forward, out rayodelante, 1))
        {
            //detecta  el obstaculo
            sensores.x = 1;
        }else
        {
            sensores.x = 0;

        }

        //rayo se tira para abajo para detectr solo la basura
        if (Physics.Raycast(transform.position, transform.up*-1, out rayobajo,0.5f,dondeestabasura))
        {
            //detecta  el obstaculo
            sensores.y = 1;
            Debug.Log(rayobajo.transform.name);
        }
        else
        {
            sensores.y = 0;

        }

        //rayo se tira para abajo para detectr solo la base 
        // el ultimo parametro lo que hace que si en pyect setting fisic esta desmarcado queries hit trigger los ray no detecta triguers
        //al ponerle ese parametro , aunq no este marcado los marcaaa
        if (Physics.Raycast(transform.position, transform.up * -1, out rayobajo, 1, dodneestalabase,QueryTriggerInteraction.Collide))
        {
            //detecta  la base
            sensores.z = 1;
            Homeposition = currentposition;
            Debug.Log(rayobajo.transform.name);
        }
        else
        {
            sensores.z = 0;

        }


    }
}
