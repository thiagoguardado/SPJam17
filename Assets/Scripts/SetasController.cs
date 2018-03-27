using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetasController : MonoBehaviour {


	public SetaTamanho setaEtereo;
	public SetaTamanho setaPegadaPequena;
	public SetaTamanho setaPegadaGrande;


	public void ComecarAtivar (Jogador.ModoDetalhe modoDetalhe){

		switch (modoDetalhe) {
		case Jogador.ModoDetalhe.Etereo:

			setaPegadaGrande.Desativar ();
			setaPegadaPequena.Desativar ();
			setaEtereo.ComecarAtivar ();
			break;

		case Jogador.ModoDetalhe.PegadaPequena:
			
			setaPegadaGrande.Desativar ();
			setaPegadaPequena.ComecarAtivar ();
			setaEtereo.Desativar ();
			break;

		case Jogador.ModoDetalhe.PegadaGrande:

			setaPegadaGrande.ComecarAtivar ();
			setaPegadaPequena.Desativar ();
			setaEtereo.Desativar ();
			break;

		default:
			break;
		}

	}

	public void Atulizar (Jogador.ModoDetalhe modoDetalhe,Vector3 posicao)
	{
		switch (modoDetalhe) {
		case Jogador.ModoDetalhe.Etereo:
			
			setaEtereo.Atulizar (posicao);
			break;

		case Jogador.ModoDetalhe.PegadaPequena:
			
			setaPegadaPequena.Atulizar (posicao);
			break;

		case Jogador.ModoDetalhe.PegadaGrande:

			setaPegadaGrande.Atulizar (posicao);
			break;

		default:
			break;
		}
	}

	public void Desativar ()
	{

		setaPegadaGrande.Desativar ();
		setaPegadaPequena.Desativar ();
		setaEtereo.Desativar ();

	}
}
