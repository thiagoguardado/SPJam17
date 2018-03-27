using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetaTamanho : MonoBehaviour {


	public Transform ponta;
	public SpriteRenderer rend;
	public Transform setaOrigem;
	public bool visivel = false;
	public bool ativaNoProximo = false;


	// Update is called once per frame
	void Update () {

		if (visivel || ativaNoProximo) {

			AtualizarTamanhoPosicao ();

		}
	}

	public void Atulizar(Vector3 localizacao){

		ponta.position = new Vector3(localizacao.x,ponta.position.y,localizacao.z);

		if (ativaNoProximo) {
			AtualizarTamanhoPosicao ();
			Ativar ();
			ativaNoProximo = false;
		}


	
	}

	public void ComecarAtivar(){

		ativaNoProximo = true;
	}

	private void Ativar(){

		rend.enabled = true;
		visivel = true;
	
	}

	public void Desativar(){

		rend.enabled = false;
		visivel = false;

	}

	void AtualizarTamanhoPosicao ()
	{
		setaOrigem.LookAt (ponta.position);
		rend.size = new Vector2 (ponta.localPosition.magnitude, rend.size.y);
	}
}
