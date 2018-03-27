using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creditos : MonoBehaviour {

	public float tempoEspera;

	private float tempoDecorrido = 0f;


	// Update is called once per frame
	void Update () {

		tempoDecorrido += Time.deltaTime;

		if (tempoDecorrido > -tempoEspera) {
		
			if (Input.GetMouseButtonDown (0) || Input.touchCount > 0) {

//				Voltar ();
			
			}
		
		}

	}

	public void Voltar(){


		LevelsController.LoadMenu ();

	}
}
