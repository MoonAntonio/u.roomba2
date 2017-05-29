//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// Antonio_IA.cs (22/05/2017)													\\
// Autor: Antonio Mateo (Moon Antonio) 	antoniomt.moon@gmail.com				\\
// Descripcion:		Inteligencia artificial del roomba							\\
// Fecha Mod:		29/05/2017													\\
// Ultima Mod:																	\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
#endregion

namespace Antonio.IA
{
	/// <summary>
	/// <para>Inteligencia artificial del roomba</para>
	/// </summary>
	public class Antonio_IA : MonoBehaviour 
	{
		/* INFO
		Se origina una lista y un algoritmo BFS para encontrar el camino.

		Algoritmo A* DFS
		BFS --> Breath, Shallow, explora los vecinos
		DFS --> Explora los hijos antes de los vecinos(A*)

		El recorrido mas optimo es sabiendo la base b(x,y) y el roomba r(x1,y1).
		La heuristica seria h(x-x1)+(y-y1) y asi obtenemos el numero de movimientos minimos (Coste)
		*/

		#region Variables Publicas
		public actions roombaAcciones;
		public List<Vector2> posVisitada;
		public Percepts roombaSensores;
		public float timeActuacion = 1;
		public List<Vector2> solution;
		public int w,h;
		#endregion

		#region Inicializadores
		/// <summary>
		/// <para>Inicializador de <see cref="Antonio_IA"/></para>
		/// </summary>
		private void Start()// Inicializador de Antonio_IA
		{
			// Asignamos las variables
			roombaAcciones = GetComponent<actions>();
			posVisitada = new List<Vector2>();
			roombaSensores = GetComponent<Percepts>();

			// Iniciamos el sistema de decision
			StartCoroutine(Decision());
		}
		#endregion

		#region Actualizadores
		/// <summary>
		/// <para>Toma una decision dependiendo de los parametros del roomba.</para>
		/// </summary>
		/// <returns></returns>
		private IEnumerator Decision()// Toma una decision dependiendo de los parametros del roomba
		{
			while (roombaSensores.encendido)
			{
				yield return new WaitForSeconds(timeActuacion);
				// lista metodo contains
				if (posVisitada.Count + 1 == w * h)
				{

					if (solution.Count == 0)
					{
						VuelveACasa();//aplica A* para encontrar la ruta de casillas
					}
					else
					{
						if (roombaSensores.currentposition == roombaSensores.Homeposition)
						{
							roombaAcciones.apagar();

						}

						if (solution[0] == roombaSensores.currentposition)
						{
							solution.RemoveAt(0);


						}
						//rommba vuelve por el camino que a pensado
						Vector2 relativeposition = Vector2.zero;
						if (solution.Count > 0)
						{
							relativeposition = solution[0] - roombaSensores.currentposition;
						}

						Debug.Log(relativeposition);
						switch (roombaSensores.rotacion)
						{
							case 0:
								if (relativeposition.y > 0)
								{
									roombaAcciones.avanzar();
								}
								else
								{
									if (relativeposition.x > 0)
									{
										roombaAcciones.girarDerecha();
									}
									else
									{
										roombaAcciones.girarIzquierda();
									}


								}

								break;

							case 90:
								if (relativeposition.x > 0)
								{
									roombaAcciones.avanzar();
								}
								else
								{
									if (relativeposition.y < 0)
									{
										roombaAcciones.girarDerecha();
									}
									else
									{
										roombaAcciones.girarIzquierda();
									}

								}

								break;

							case 180:
								if (relativeposition.y < 0)
								{
									roombaAcciones.avanzar();
								}
								else
								{
									if (relativeposition.x < 0)
									{
										roombaAcciones.girarDerecha();
									}
									else
									{
										roombaAcciones.girarIzquierda();
									}

								}
								break;

							case 270:
								if (relativeposition.x < 0)
								{
									roombaAcciones.avanzar();
								}
								else
								{
									if (relativeposition.y > 0)
									{
										roombaAcciones.girarDerecha();
									}
									else
									{
										roombaAcciones.girarIzquierda();
									}
								}
								break;



						}
						continue;
					}
					//roombaAcciones.apagar();



				}

				if (roombaSensores.sensores.y == 1)
				{
					roombaAcciones.aspirar();
					//continue hace q se salte todo lo de abajo y vuelva a empezar el bucle
					continue;
				}


				if ((roombaSensores.sensores.x == 1) || (posVisitada.Contains(SiguienteCasilla())))
				{
					roombaAcciones.girarDerecha();
				}
				else
				{
					posVisitada.Add(roombaSensores.currentposition);
					roombaAcciones.avanzar();
				}

				if (posVisitada.Count > 0)
				{
					w = (int)posVisitada.OrderByDescending(o => o.x).ToList()[0].x + 1;
					//cogemos el valor absoluto para ignorar las negativas y saber el array
					// y se ordenar de menor a mayor por el tema de -3,-2,-1 y asi el menor lo ponemos en 
					//absolut para saber su distancia
					// h = Mathf.Abs((int)posVisitada.OrderBy(o => o.x).ToList()[0].y);
					h = (int)posVisitada.OrderByDescending(o => o.y).ToList()[0].y + 1;
				}
			}
		}
		#endregion

		#region Metodos
		/// <summary>
		/// <para>Vuelve a la base.</para>
		/// </summary>
		public void VuelveACasa()// Vuelve a la base
		{
			solution = new List<Vector2>();
			Stack<Vector2> Frontera = new Stack<Vector2>();
			Frontera.Push(roombaSensores.currentposition);

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
				if (GetAbsoluto(current, roombaSensores.Homeposition) == 0)
				{

					//si-... finalizamos el algoritmo
					// si la condicion es cero se a encontrado la base

					Debug.Log("CAMINO A CASA ENCONTRADO");
					return;
				}



				//no... obtener los hijos del nodo
				List<Vector2> hijos = ObtenerCasillasAdyacentes(current);

				//ordenadar los hijos de menor a mayor modo heuristico

				hijos = hijos.OrderByDescending(o => GetAbsoluto(o, roombaSensores.Homeposition)).ToList();


				//se pasan ala frontera los hijos

				foreach (Vector2 hijo in hijos)
				{
					Frontera.Push(hijo);
				}
			}


			//Si el algoritmo lleaqui aqui no hay camino posible

		}
		#endregion

		#region Funcionalidad
		/// <summary>
		/// <para>Obtiene el valor absoluto</para>
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		private int GetAbsoluto(Vector2 a, Vector2 b)// Obtiene el valor absoluto
		{
			int ret;
			ret = (int)Mathf.Abs(a.x - b.x) + (int)Mathf.Abs(a.y - b.y);
			return ret;
		}

		/// <summary>
		/// <para>Obtiene las casillas vecinas.</para>
		/// </summary>
		/// <param name="casilla">Casilla central.</param>
		/// <returns>Devuelve la lista de vecinos</returns>
		private List<Vector2> ObtenerCasillasAdyacentes(Vector2 casilla)// Obtiene las casillas vecinas
		{
			// Lista que retornar
			List<Vector2> retorno = new List<Vector2>();

			// Obtiene el vecino UP
			Vector2 up = casilla + new Vector2(0, 1);
			retorno.Add(up);

			// Obtiene el vecino RIGHT
			Vector2 right = casilla + new Vector2(1, 0);
			retorno.Add(right);

			// Obtiene el vecino DOWN
			Vector2 down = casilla + new Vector2(0, -1);
			retorno.Add(down);

			// Obtiene el vecino LEFT
			Vector2 left = casilla + new Vector2(-1, 0);
			retorno.Add(left);

			// Devuelve la lista de vecinos
			return retorno;
		}

		/// <summary>
		/// <para>Obtiene la siguiente casilla</para>
		/// </summary>
		/// <returns>La casilla siguiente</returns>
		private Vector2 SiguienteCasilla()// Obtiene la siguiente casilla
		{
			Vector2 ret = roombaSensores.currentposition;

			// Obtiene la casilla dependiendo de su rotacion
			switch (roombaSensores.rotacion)
			{
				case 0:
					ret += new Vector2(0, 1);
					break;

				case 90:
					ret += new Vector2(1, 0);
					break;

				case 180:
					ret += new Vector2(0, -1);
					break;

				case 270:
					ret += new Vector2(-1, 0);
					break;
			}

			// Devuelve la casilla siguiente
			return ret;
		}
		#endregion
	}
}
