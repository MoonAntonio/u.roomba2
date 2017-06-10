using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour {

    public int puntos;

    public void SuciedadAspirada()
    {
        puntos += 100;
    }

    public void AccionRealizada()
    {
        puntos -= 1;
    }

    public void ApagarseFueradelaBase()
    {
        puntos -= 1000;
    }


    public void Apagar()
    {
        Debug.Log("Me apago");
        
    }


    public void Accionrealizada(int coste)
    {
        puntos -= coste;
    }

}
