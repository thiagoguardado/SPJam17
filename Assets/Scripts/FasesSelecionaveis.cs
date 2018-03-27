using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FasesSelecionaveis : MonoBehaviour {

	public Button anterior;
	public Button proximo;

	public Text numeroFase;


	void Awake(){

		if (!LevelsController.levels [0].isOpened) {

			// exibir tutorial

			LevelsController.OpenLevel (1);

			numeroFase.text = "1";


		}

		VerificarBotoes ();

	}


	void Update(){
	
		if (Input.GetKeyDown (KeyCode.P)) {

			// abrir todas as fases
			for (int i = 0; i < LevelsController.levels.Length; i++) {
				LevelsController.OpenLevel (i + 1);
			}
		
			VerificarBotoes ();

		}
	
	}

	public void Anterior(){

		numeroFase.text = (int.Parse (numeroFase.text) - 1).ToString ();
		VerificarBotoes ();

	}

	public void Proximo(){

		numeroFase.text = (int.Parse (numeroFase.text) + 1).ToString ();
		VerificarBotoes ();

	}

	void VerificarBotoes(){
	
		int faseexibida = int.Parse (numeroFase.text);

		if (faseexibida == 1) {
			anterior.interactable = false;

			if (LevelsController.levels [faseexibida].isOpened) {
				proximo.interactable = true;
			} else {
				proximo.interactable = false;
			}
		} else if (faseexibida == LevelsController.levels.Length) {

			anterior.interactable = true;
			proximo.interactable = false;
		
		} else {
		
			anterior.interactable = true;
		
			if (LevelsController.levels [faseexibida].isOpened) {
				proximo.interactable = true;
			} else {
				proximo.interactable = false;
			}

		}



	
	}

	public void IniciarFase(){

		LevelsController.StartLevel (int.Parse (numeroFase.text));
	
	}

}
