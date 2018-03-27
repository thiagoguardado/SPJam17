using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOutsideScreen : MonoBehaviour {

	private Jogador jogador;
	public Vector3 respawnPoint; 

	void Awake(){

		jogador = GetComponent<Jogador> ();
	}

	void Update(){

		Vector2 screenPos = Camera.main.WorldToScreenPoint (transform.position);

		if (screenPos.x < 0 || screenPos.x > Screen.width ||
		   screenPos.y < 0 || screenPos.y > Screen.height) {
		
			RespawnPlayer ();
		
		}


	}

	void RespawnPlayer(){

		jogador.Respawn ();

	}

}
