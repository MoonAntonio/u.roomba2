//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// Actions.cs (22/05/2017)														\\
// Autor: Antonio Mateo (Moon Antonio) 	antoniomt.moon@gmail.com				\\
// Descripcion:		Acciones que contiene el roomba								\\
// Fecha Mod:		22/05/2017													\\
// Ultima Mod:		Version Inicial												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
#endregion

namespace Antonio.IA
{
	/// <summary>
	/// <para>Acciones que contiene el roomba</para>
	/// </summary>
	[AddComponentMenu("UI/Roomba/Actions")]
	public class Actions : MonoBehaviour
	{
		#region API
		/// <summary>
		/// <para>Gira el roomba hacia la derecha.</para>
		/// </summary>
		public void GirarDerecha()// Gira el roomba hacia la derecha
		{
			// Rotamos en Y 90º
			transform.Rotate(new Vector3(0, 90, 0));

			// Mandamos un mensaje
			SendMessage("AccionRealizada", 1, SendMessageOptions.DontRequireReceiver);
		}

		/// <summary>
		/// <para>Gira el roomba hacia la izquierda.</para>
		/// </summary>
		public void GirarIzquierda()// Gira el roomba hacia la izquierda
		{
			// Rotamos en Y 90º
			transform.Rotate(new Vector3(0, -90, 0));

			// Mandamos un mensaje
			SendMessage("AccionRealizada", 1, SendMessageOptions.DontRequireReceiver);
		}

		/// <summary>
		/// <para>Avanza el roomba hacia delante.</para>
		/// </summary>
		public void Avanzar()// Avanza el roomba hacia delante
		{
			// Avanzamos
			transform.localPosition += transform.forward;

			// Mandamos un mensaje
			SendMessage("AccionRealizada", 1, SendMessageOptions.DontRequireReceiver);
		}

		/// <summary>
		/// <para>Apaga el roomba.</para>
		/// </summary>
		public void Apagar()// Apaga el roomba
		{
			// Apagamos el roomba
			GetComponent<Percepts>().encendido = false;

			// Mandamos un mensaje
			SendMessage("AccionRealizada", 1, SendMessageOptions.DontRequireReceiver);
		}

		/// <summary>
		/// <para>Aspira la suciedad.</para>
		/// </summary>
		public void Aspirar()// Aspira la suciedad
		{
			RaycastHit rayobajo;

			if (Physics.Raycast(transform.position, transform.up * -1, out rayobajo, 0.5f))
			{
				// Detecta el obstaculo
				Destroy(rayobajo.collider.gameObject);

				// Mandamos un mensaje
				SendMessage("SuciedadAspirada", 1, SendMessageOptions.DontRequireReceiver);
			}

			// Mandamos un mensaje
			SendMessage("AccionRealizada", 1, SendMessageOptions.DontRequireReceiver);
		}
		#endregion
	}
}
