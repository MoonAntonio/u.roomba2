using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Ave_IA : MonoBehaviour {
    public actions roombaations;
    public List <Vector2> Visitoposi;
    public Percepts roombasensor;
    public float tiempo_actuacion = 1;
    public List<Vector2> solution;

    public int width;
    public int height;
    void Start()
    {
        roombaations = GetComponent<actions>();
        Visitoposi = new List<Vector2>();
        roombasensor = GetComponent<Percepts>();
        StartCoroutine(Decision());
    }


        //uso de listas
        //se hace una lista y un algoritmo bfs para encontrar el camino y haciendo un apequeña memoriadel recorido
        //Algoritmo A* DFS este es el q se utiliza
        //si fuera bfs seria digistra
        //bfs-->breath, shallow, explora los hermanos
        //dfs--> explora los hujos antes de los hermanos(a*)
        //recorrido mas optimo sabiendo la base b(x,y)
        //y el roomba r(x1,y1)   la heuristica seria h(x-x1)+(y-y1) y asi opteniamos el numero de movimientos minimos
        // y asi ordenador por el mejor valor heuristico al ser bfs  se exploraran primero los que menos movinientos tenga H(x,y) donde
        // x,y serian los que menores valores tubieran
        //UNA POSIble optimizacion seria la rotacion del propio rumba enel q el camino mas corto sea ala vez sea donde menos se gire

    IEnumerator Decision()
    {


        while (roombasensor.encendido)
        {
            yield return new WaitForSeconds(tiempo_actuacion);
            // lista metodo contains
            if (Visitoposi.Count + 1 == width * height)
            {

                if (solution.Count == 0)
                {
                    vuelveacasa();//aplica A* para encontrar la ruta de casillas
                }else
                {
                    if (roombasensor.currentposition == roombasensor.Homeposition)
                    {
                        roombaations.apagar();
                        
                    }

                    if (solution[0] == roombasensor.currentposition)
                    {
                        solution.RemoveAt(0);


                    }
                    //rommba vuelve por el camino que a pensado
                    Vector2 relativeposition = Vector2.zero;
                    if (solution.Count > 0)
                    {
                        relativeposition= solution[0] - roombasensor.currentposition;
                    }

                    Debug.Log(relativeposition);
                    switch (roombasensor.rotacion)
                    {
                        case 0:
                            if (relativeposition.y > 0)
                            {
                                roombaations.avanzar();
                            }
                            else
                            {
                                if (relativeposition.x > 0)
                                {
                                    roombaations.girarDerecha();
                                 }
                                else
                                {
                                    roombaations.girarIzquierda();
                                }
                              

                            }

                            break;

                        case 90:
                            if (relativeposition.x > 0)
                            {
                                roombaations.avanzar();
                            }
                            else
                            {
                                if (relativeposition.y < 0)
                                {
                                    roombaations.girarDerecha();
                                }
                                else
                                {
                                    roombaations.girarIzquierda();
                                }

                            }

                            break;

                        case 180:
                            if (relativeposition.y < 0)
                            {
                                roombaations.avanzar();
                            }
                            else
                            {
                                if (relativeposition.x < 0)
                                {
                                    roombaations.girarDerecha();
                                }
                                else
                                {
                                    roombaations.girarIzquierda();
                                }

                            }
                            break;

                        case 270:
                            if (relativeposition.x < 0)
                            {
                                roombaations.avanzar();
                            }
                            else
                            {
                                if (relativeposition.y > 0)
                                {
                                    roombaations.girarDerecha();
                                }
                                else
                                {
                                    roombaations.girarIzquierda();
                                }
                            }
                            break;



                    }
                    continue;
                }
                //roombaations.apagar();
               


            }

            if (roombasensor.sensores.y == 1)
            {
                roombaations.aspirar();
                //continue hace q se salte todo lo de abajo y vuelva a empezar el bucle
                continue;
            }


            if ((roombasensor.sensores.x == 1 )||(Visitoposi.Contains(siguientecasilla())))
            {
                roombaations.girarDerecha();
            }
            else
            {
                Visitoposi.Add(roombasensor.currentposition);
                roombaations.avanzar();
            }

            if (Visitoposi.Count > 0)
            {
                width = (int)Visitoposi.OrderByDescending(o => o.x).ToList()[0].x+1;
                //cogemos el valor absoluto para ignorar las negativas y saber el array
                // y se ordenar de menor a mayor por el tema de -3,-2,-1 y asi el menor lo ponemos en 
                //absolut para saber su distancia
                // height = Mathf.Abs((int)Visitoposi.OrderBy(o => o.x).ToList()[0].y);
                height = (int)Visitoposi.OrderByDescending(o => o.y).ToList()[0].y+1;
            }



            

        }
        
      
    }

    public void vuelveacasa()
    {


        solution = new List<Vector2>();
        Stack<Vector2> Frontera = new Stack<Vector2>();
        Frontera.Push(roombasensor.currentposition);

        while (Frontera.Count > 0)
        {
            //  Obtener un nodo a explorar(el primero de la lista)
            Vector2 current = Frontera.Pop();
            // Frontera.Remove(current);borra en la lista todos los current q sea iguales
            //pero como en el algoritmo se borra el primero de la lista mejor el metodo de abajo
            //Frontera.RemoveAt(0);
            //al ser dfs se usa cola o pila
            solution.Add(current);

            // es este nodo la solucion?
            if (h(current, roombasensor.Homeposition)==0)
            {

            //si-... finalizamos el algoritmo
                // si la condicion es cero se a encontrado la base

                Debug.Log("CAMINO A CASA ENCONTRADO");
                return;
            }



            //no... obtener los hijos del nodo
            List<Vector2> hijos = ObtenerCasillasAdyacentes(current);

            //ordenadar los hijos de menor a mayor modo heuristico

            hijos = hijos.OrderByDescending(o => h(o, roombasensor.Homeposition)).ToList();


        //se pasan ala frontera los hijos

        foreach(Vector2 hijo in hijos)
            {
                Frontera.Push(hijo);
            }
        }


        //Si el algoritmo lleaqui aqui no hay camino posible

    }

    private int h (Vector2 a, Vector2 b)
    {
        int ret;
        ret =(int) Mathf.Abs(a.x - b.x) + (int)Mathf.Abs(a.y - b.y);
        return ret;


    }

    private List<Vector2> ObtenerCasillasAdyacentes(Vector2 casilla)
    {

        List<Vector2> retorno = new List<Vector2>();
        Vector2 up = casilla + new Vector2(0, 1);
        retorno.Add(up);


   
        Vector2 right = casilla + new Vector2(1, 0);
        retorno.Add(right);

        Vector2 down = casilla + new Vector2(0,- 1);
        retorno.Add(down);

     
        Vector2 left = casilla + new Vector2(-1, 0);
        retorno.Add(left);

        return retorno;
    }


    



Vector2 siguientecasilla()
    {
        Vector2 ret = roombasensor.currentposition;
        switch (roombasensor.rotacion)
        {
            case 0:
                ret += new Vector2(0, 1);
                break;

            case 90:
                ret += new Vector2(1, 0);
                break;

            case 180:
                ret += new Vector2(0,-1);
                break;

            case 270:
                ret += new Vector2(-1, 0);
                break;


                
        }
        return ret;
    }


    }
