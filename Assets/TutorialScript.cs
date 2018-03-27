using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour {

	public GameObject[] setas;

	public TriggerTutorial portalJogador;

	public Jogador jogador;
	public Fantasma fgrande;
	public Fantasma fpequeno;

	private int estagio = 0;

	private bool primeiromovimento = false;

	private bool primeiroTransformaMaterial  = false;

	private bool primeiraChamada = false;

	void Update(){
	
		Escutar ();
	
	}


	void Awake(){
	
		AtivarSeta (0);
	
	}

	public void AtivarSeta(int i){

		for (int j = 0; j < setas.Length; j++) {

			if (i != j) {

				setas [j].SetActive (false);
			
			} else {
			
				setas [j].SetActive (true);
			
			}

		}


		estagio = i;
	}



	void Escutar ()
	{

		switch (estagio) {

		// move
		case 0: 

			if (jogador.estaMovendo ){
				AtivarSeta (1);
			}

			break;

			//ativar material pequeno
		case 1:

			if (jogador.modoDetalhe == Jogador.ModoDetalhe.PegadaPequena) {
				
				AtivarSeta (2);

			}
			break;
		
			// andar ate portal
		case 2:

			if (portalJogador.jogadorEntrou) {
			
				AtivarSeta (3);
				jogador.primeiraChamada = false;
			}
			break;

			// chamar
		case 3:

			if (jogador.primeiraChamada) {
			
				AtivarSeta (4);
			
			}
			break;

			// mover ate o grande
		case 4:
			if (jogador.estaMovendo){
				AtivarSeta (5);
			}

			break;

			// apagar
		case 5:

			if (jogador.primeiroApagar) {

				AtivarSeta (6);

			}
			break;

			// passo grande
		case 6:

			if (jogador.modoDetalhe == Jogador.ModoDetalhe.PegadaGrande) {

				AtivarSeta (7);
				portalJogador.jogadorEntrou = false;
			}

			break;
		
			// mover ate portal
		case 7:
			if(portalJogador.jogadorEntrou){

				AtivarSeta (8);
				jogador.primeiraChamada = false;
			}
			break;

		case 8:

			if (jogador.primeiraChamada) {
				
				// fim

			}
			break;
		
		default:
			break;
		}
			
	}
}
