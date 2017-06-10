//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// Percepts.cs (22/05/2017)														\\
// Autor: Antonio Mateo (Moon Antonio) 	antoniomt.moon@gmail.com				\\
// Descripcion:		Control de los sensores del roomba							\\
// Fecha Mod:		22/05/2017													\\
// Ultima Mod:		Version Inicial												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
#endregion

namespace Antonio.IA
{
	/// <summary>
	/// <para>Control de los sensores del roomba</para>
	/// </summary>
	[AddComponentMenu("UI/Roomba/Percepts")]
	public class Percepts : MonoBehaviour
	{
		#region Variables Publicas
		/// <summary>
		/// <para>Actual posicion del roomba.</para>
		/// </summary>
		public Vector2 currentposition;                                 // Actual posicion del roomba
		/// <summary>
		/// <para>Los sensores del roomba.</para>
		/// <para>(t,d,h)</para>
		/// <para>t --> vale 1 si el agente choca con algo de frente, 0 si no hay ningún obstáculo</para>
		/// <para>d --> vale 1 si hay suciedad en la localización del agente</para>
		/// <para>h --> vale 1 si el agente está en su base</para>
		/// </summary>
		public Vector3 sensores;                                        // Los sensores del roomba
		/// <summary>
		/// <para>Rotacion del roomba.</para>
		/// </summary>
		public int rotacion;                                            // Rotacion del roomba
		/// <summary>
		/// <para>Posicion de la casa</para>
		/// </summary>
		public Vector2 homePosition;                                    // Posicion de la casa
		/// <summary>
		/// <para>Si el roomba esta encendido</para>
		/// </summary>
		public bool encendido = true;                                   // Si el roomba esta encendido
		/// <summary>
		/// <para>Bateria del roomba.</para>
		/// </summary>
		public int bateria = 100;										// Bateria del roomba
		/// <summary>
		/// <para>Layer de la basura.</para>
		/// </summary>
		public LayerMask dondeestabasura;								// Layer de la basura
		/// <summary>
		/// <para>Layer de la base.</para>
		/// </summary>
		public LayerMask dodneestalabase;								// Layer de la base
		#endregion

		#region Inicializadores
		/// <summary>
		/// <para>Inicializador de <see cref="Percepts"/>.</para>
		/// </summary>
		private void Start()// Inicializador de Percepts
		{
			// Reseteo de variables
			currentposition = Vector2.zero;
			sensores = Vector3.zero;
		}
		#endregion

		#region Actualizadores
		/// <summary>
		/// <para>Actualizador de <see cref="Percepts"/>.</para>
		/// </summary>
		private void Update()// Actualizador de Percepts
		{
			// Asignar la posicion en XY y la rotacion
			currentposition.x = Mathf.RoundToInt(transform.position.x);
			currentposition.y = Mathf.RoundToInt(transform.position.z);
			rotacion = (int)transform.rotation.eulerAngles.y;

			//sensores
			RaycastHit rayodelante;
			RaycastHit rayobajo;

			#region Debugs
			// Debug hacia adelante
			Debug.DrawRay(transform.position, transform.forward, Color.red);

			// Debug hacia arriba
			Debug.DrawRay(transform.position, transform.up * -1, Color.yellow);

			// Debug hacia arriba
			Debug.DrawRay(transform.position, transform.up * -1, Color.green);
			#endregion


			// Raycast para detectar obstaculos
			if (Physics.Raycast(transform.position, transform.forward, out rayodelante, 1))
			{
				// Detecto el obstaculo
				sensores.x = 1;
			}
			else
			{
				// No Detecto el obstaculo
				sensores.x = 0;
			}

			// Raycast para detectar basura
			if (Physics.Raycast(transform.position, transform.up * -1, out rayobajo, 0.5f, dondeestabasura))
			{
				// Detecto el basura
				sensores.y = 1;
			}
			else
			{
				// Detecto el basura
				sensores.y = 0;
			}

			// Nota
			// QueryTriggerInteraction -> El rayo detectara los triggers aunque este desactivada la opcion en Settings.

			// Raycast para detectar la base
			if (Physics.Raycast(transform.position, transform.up * -1, out rayobajo, 1, dodneestalabase, QueryTriggerInteraction.Collide))
			{
				// Detecto la base
				sensores.z = 1;
				homePosition = currentposition;
			}
			else
			{
				// No detecto la base
				sensores.z = 0;
			}
		}
		#endregion
	}
}
