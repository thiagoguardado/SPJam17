using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotaoPeso : MonoBehaviour {

	public int pesoMin;
	private bool estaAtivo;

	public bool desativaAutomatico;

	private Collider col;


	void Awake(){

		col = GetComponent<Collider> ();

	}

	void Update(){
		ChecarBox ();
	}

	void ChecarBox(){

		bool alguemPesadoSobre = false;

		LayerMask lm = LayerMask.GetMask (new string[] {"Default","Fantasmas","Jogador"});
		Collider[] colsDentro = Physics.OverlapBox (col.bounds.center, col.bounds.extents, transform.rotation, lm, QueryTriggerInteraction.Collide);

		for (int i = 0; i < colsDentro.Length; i++) {

			Peso pesoScript = colsDentro [i].GetComponent<Peso> ();
			if (pesoScript != null) {

				if (pesoScript.ativo) {
			
					if (pesoScript.peso >= pesoMin) {

						alguemPesadoSobre = true;
						break;

					}
			
				}

			}

		}

		if (!estaAtivo && alguemPesadoSobre) {
			AtivarBotao ();
		} else if (estaAtivo && !alguemPesadoSobre && desativaAutomatico) {
			DesativarBotao ();
		}

	}
		

	protected virtual void AtivarBotao ()
	{
		estaAtivo = true;
	}

	protected virtual void DesativarBotao(){
	
		estaAtivo = false;

	}
}
