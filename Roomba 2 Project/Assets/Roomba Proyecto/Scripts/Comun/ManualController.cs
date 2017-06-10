//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// ManualController.cs (22/05/2017)												\\
// Autor: Antonio Mateo (Moon Antonio) 	antoniomt.moon@gmail.com				\\
// Descripcion:		Control manual para el roomba								\\
// Fecha Mod:		22/05/2017													\\
// Ultima Mod:		Version Inicial												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
#endregion

namespace Antonio.IA
{
	/// <summary>
	/// <para>Control manual para el roomba</para>
	/// </summary>
	[AddComponentMenu("UI/Roomba/ManualController")]
	public class ManualController : MonoBehaviour
	{
		#region Actualizadores
		/// <summary>
		/// <para>Actualizador de <see cref="ManualController"/>.</para>
		/// </summary>
		private void Update()// Actualizador de ManualController
		{
			// Gira hacia la izquierda
			if (Input.GetKeyDown(KeyCode.A))
			{
				SendMessage("GirarIzquierda", SendMessageOptions.DontRequireReceiver);
			}

			// Gira hacia la derecha
			if (Input.GetKeyDown(KeyCode.D))
			{
				SendMessage("GirarDerecha", SendMessageOptions.DontRequireReceiver);
			}

			// Avanza hacia delante
			if (Input.GetKeyDown(KeyCode.W))
			{
				SendMessage("Avanzar", SendMessageOptions.DontRequireReceiver);
			}
		}
		#endregion
	}
}
