using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotaoPesoJanela : BotaoPeso {

	public GameObject parede;

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

		parede.SetActive (true);

	}

	private void DesativarParede(){

		parede.SetActive (false);

	}
}
