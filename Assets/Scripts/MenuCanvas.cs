using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCanvas : MonoBehaviour {



	public void MudarTelaParaSelecionarFase(){

		AudioManager.instancia.PlayClipBotao ();

		SimpleSceneFader.ChangeSceneWithFade("selecionarFase",0.5f);


	}

	public void MudarTelaParaComoJogar(){

		AudioManager.instancia.PlayClipBotao ();

		SimpleSceneFader.ChangeSceneWithFade("comoJogar",0.5f);

	}

	public void MudarTelaParaMenu(){

		AudioManager.instancia.PlayClipBotao ();

		SimpleSceneFader.ChangeSceneWithFade("mainMenu",0.5f);

	}

	public void IniciarLevel1(){
	
		AudioManager.instancia.PlayClipBotao ();

		if (!LevelsController.levels [0].isOpened) {

			LevelsController.OpenLevel (1);

			// exibir tutorial primeiro
		

		}

		LevelsController.StartLevel (1);

	}

	public void Sair(){

		Application.Quit ();

	}



}
