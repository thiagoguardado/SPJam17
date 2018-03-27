using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pegada : MonoBehaviour {

	public int pegadaIndex;
	public TamanhoPegada tamanhoPegada;
	public int gastoTintaPegada;
	private Collider col;

	Jogador jogador;


	void Awake(){
	
		col = GetComponent<Collider> ();
		jogador = FindObjectOfType<Jogador> ();

	}


	public void ApagarPegada(){

		jogador.RetirarTintaContador (gastoTintaPegada);

		Destroy (gameObject);
	
	}

}

public enum TamanhoPegada{
	Nenhuma,
	Pequena,
	Grande
}
