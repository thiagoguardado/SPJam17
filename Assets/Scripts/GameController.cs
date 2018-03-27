using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public Fantasma fantasmaPequeno;
	public Fantasma fantasmaGrande;
	public static bool isPaused;
	public int levelNumber;
	public HUD hud;
	public bool ganhou;


	void Awake(){

		IniciarFase ();

	}


	void Update(){

		ChecarCondicaoVitoria ();

	}



	void ChecarCondicaoVitoria ()
	{
		if (fantasmaPequeno.encontrouFenda && fantasmaGrande.encontrouFenda && !ganhou) {
		
			ganhou = true;

			Debug.Log ("Vitoria");

			LevelsController.OpenLevel (levelNumber + 1);

			Pause (true);

			hud.PainelVitoria ();
		
		}
	}


	public static void IniciarFase(){

		isPaused = false;
	}

	public static void Pause(bool onOff){
		isPaused = onOff;
	}
}
