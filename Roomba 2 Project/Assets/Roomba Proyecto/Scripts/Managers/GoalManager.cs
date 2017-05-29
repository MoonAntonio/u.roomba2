using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour {

    public int puntos;

    public void suciedadAspirada()
    {
        puntos += 100;
    }

    public void accionRealizada()
    {
        puntos -= 1;
    }

    public void apagarseFueradelaBase()
    {
        puntos -= 1000;
    }


    public void SuciedadAspirada()
    {
        puntos += 100;
    }

    public void apagar()
    {
        Debug.Log("Me apago");
        
    }


    public void accionrealizada(int coste)
    {
        puntos -= coste;
    }

}
