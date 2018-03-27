using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParedeQuebravel : MonoBehaviour {

	public int minPesoParaQuebrar;

	public Animator animEsquerda;
	public Animator animDireita;

	public Collider colEsquerda;
	public Collider colDireita;

	public Collider esteCollider;

	void OnTriggerEnter(Collider other){

		if (other.gameObject.tag == "Fantasma") {

			Peso peso = other.gameObject.GetComponent<Peso> ();

			if (peso != null) {

				if (peso.peso >= minPesoParaQuebrar) {

					Quebrar ();
				
				}
			
			}
		
		}

	}

	public void Quebrar ()
	{
		animEsquerda.SetTrigger ("Destruir");
		animDireita.SetTrigger ("Destruir");
	
		colEsquerda.enabled = false;
		colDireita.enabled = false;

		esteCollider.enabled = false;

	}
}
