//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// GoalManager.cs (22/05/2017)													\\
// Autor: Antonio Mateo (Moon Antonio) 	antoniomt.moon@gmail.com				\\
// Descripcion:		Manager de puntos del roomba								\\
// Fecha Mod:		22/05/2017													\\
// Ultima Mod:		Version Inicial												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
#endregion

namespace Antonio.IA
{
	/// <summary>
	/// <para>Manager de puntos del roomba</para>
	/// </summary>
	[AddComponentMenu("UI/Roomba/GoalManager")]
	public class GoalManager : MonoBehaviour
	{
		#region Variables Publicas
		/// <summary>
		/// <para>Puntos del roomba.</para>
		/// </summary>
		public int puntos;
		#endregion

		#region API
		/// <summary>
		/// <para>Evento - Obtienes los puntos cuando aspiras una suciedad.</para>
		/// </summary>
		public void SuciedadAspirada()// Evento - Obtienes los puntos cuando aspiras una suciedad
		{
			puntos += 100;
			this.gameObject.GetComponent<Percepts>().bateria -= 1;
		}

		/// <summary>
		/// <para>Evento - Obtienes los puntos cuando realizas una accion.</para>
		/// </summary>
		public void AccionRealizada()// Evento - Obtienes los puntos cuando realizas una accion
		{
			puntos -= 1;
			this.gameObject.GetComponent<Percepts>().bateria -= 1;
		}

		/// <summary>
		/// <para>Evento -  Obtienes los puntos cuando realizas una accion</para>
		/// </summary>
		/// <param name="coste">Coste de la accion.</param>
		public void AccionRealizada(int coste)// Evento -  Obtienes los puntos cuando realizas una accion
		{
			puntos -= coste;
			this.gameObject.GetComponent<Percepts>().bateria -= 1;
		}

		/// <summary>
		/// <para>Evento - Se apaga fuera de la base.</para>
		/// </summary>
		public void ApagarseFueradelaBase()// Evento - Se apaga fuera de la base
		{
			puntos -= 1000;
		}

		/// <summary>
		/// <para>Evento - El roomba de apaga.</para>
		/// </summary>
		public void Apagar()// Evento - El roomba de apaga
		{
			Debug.Log("Me apago");

		}
		#endregion
	}
}
