using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PesoJogador : Peso {

	public Jogador jogador;

	public override bool ativo {
		get {
			if (jogador.modo == Jogador.Modo.Material) {
				return base.ativo;
			} else {
				return false;
			}
		}
	}
}
