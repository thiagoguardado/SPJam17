using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotaoPesoParede : BotaoPeso {

	public GameObject[] paredes;

	public SpriteRenderer rend;
	public Sprite botaoApertado;
	public Sprite botaoDesapertado;


	protected override void AtivarBotao ()
	{
		base.AtivarBotao ();

		rend.sprite = botaoApertado;

		DesativarParede ();
	
	}


	protected override void DesativarBotao ()
	{
		base.DesativarBotao ();

		rend.sprite = botaoDesapertado;

		AtivarParede ();
	}


	private void AtivarParede(){

		for (int i = 0; i < paredes.Length; i++) {

			paredes[i].SetActive (true);

		}



	}

	private void DesativarParede(){

		for (int i = 0; i < paredes.Length; i++) {

			paredes[i].SetActive (false);

		}
	
	}
}
