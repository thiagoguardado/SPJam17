using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

	Jogador jogador;
	public GameController gameController;
	public Image dockImage;
	public Sprite dockImageEtereo;
	public Sprite dockImageMaterial;
	public Slider barraEnergia;
	public RectTransform preenchimentoAGastar;
	public Toggle togglePequena;
	public Toggle toggleGrande;
	public ToggleGroup toggleGroup;
	public Animator pausaAnimator;
	public Animator vitoriaAnimator;
	public AudioSource audioSource;
	public AudioClip chamarFantasmasClip;
	public AudioClip materializarClip;
	public AudioClip apagarPegadasClip;

	void Awake(){
		jogador = FindObjectOfType<Jogador> ();
	}

	void Update(){

		AtualizaBarraEnergia ();


		AtualizaPreenchimentoAGastar ();

	}

	public void AtivarPegadaPequena(){

		if (jogador.isPaused) {

			togglePequena.isOn = false;

		} else {

			if (togglePequena.isOn) {

				if (toggleGrande.isOn) {
					toggleGrande.isOn = false;
				}

				dockImage.sprite = dockImageMaterial;

				audioSource.PlayOneShot (materializarClip);

				jogador.AtivarPegadaPequena ();

			} else {

				ChecarDoisToggles ();

			}

		}
	
	}
		

	public void AtivarPegadaGrande(){

		if (jogador.isPaused) {

			toggleGrande.isOn = false;

		} else {

			if (toggleGrande.isOn) {

				if (togglePequena.isOn) {
					togglePequena.isOn = false;
				}

				dockImage.sprite = dockImageMaterial;

				audioSource.PlayOneShot (materializarClip);

				jogador.AtivarPegadaGrande ();

			} else {

				ChecarDoisToggles ();
			}

		}
	}

	void ChecarDoisToggles ()
	{
		if (!togglePequena.isOn && !toggleGrande.isOn) {

			dockImage.sprite = dockImageEtereo;

			jogador.DesativarPegada ();

		}
	}

	public void AcordarFantasmas(){

		audioSource.PlayOneShot (chamarFantasmasClip);

		jogador.AcordarFantasmas ();
	}


	public void ApagarPegadas(){

		audioSource.PlayOneShot (apagarPegadasClip);


		jogador.ApagarPegadasEDesativar ();
	}

	public void Pausa(){

		AudioManager.instancia.PlayPausa ();
		pausaAnimator.SetBool ("abrir", true);
		GameController.Pause (true);

	}

	public void PainelVitoria ()
	{
		AudioManager.instancia.PlayPausa ();
		vitoriaAnimator.SetBool ("abrir", true);
	}

	public void Continua(){

		AudioManager.instancia.PlayPausa ();
		pausaAnimator.SetBool ("abrir", false);
		GameController.Pause (false);

	}

	public void Menu(){

		AudioManager.instancia.PlayClipBotao ();
		LevelsController.LoadMenu ();
	}

	void AtualizaBarraEnergia ()
	{
		barraEnergia.value = jogador.percTinta;
	}

	public void DesativarToggles(){

		togglePequena.isOn = false;
		toggleGrande.isOn = false;

	}



	void AtualizaPreenchimentoAGastar ()
	{
		float preenchimento = jogador.tintaAGastar;
		float ancmin = 0;

		if (preenchimento < barraEnergia.value) {
			ancmin = 1 - (preenchimento) / (barraEnergia.value) ;
		}


		preenchimentoAGastar.anchorMin = new Vector2 (ancmin, 0f);


	}

	public void AtivarCalculoTintaGastar(){

		preenchimentoAGastar.GetComponent<Animator> ().SetBool ("visivel", true);

	}

	public void DesativarCalculoTintaGastar(){

		preenchimentoAGastar.GetComponent<Animator> ().SetBool ("visivel", false);

	}

	public void ProximaFase(){
	
		AudioManager.instancia.PlayClipBotao ();

		int nextlevel = gameController.levelNumber + 1;

		if (nextlevel <= LevelsController.levels.Length) {
		
			SimpleSceneFader.ChangeSceneWithFade (LevelsController.GetLevelName (nextlevel));

		} else {
		
			LevelsController.LoadCreditos ();

		}

	
	}

}

