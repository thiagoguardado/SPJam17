using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JogadorMeshAnimado : MonoBehaviour {

	public GameObject jogadorEtereo;
	public GameObject jogadorMaterial;
	public Material jogadorMaterial_pequeno;
	public Material jogadorMaterial_grande;
	public Renderer jogadorMaterialRenderer;
	public ParticleSystem particulasEtereo;
	public ParticleSystem particulasGrande;
	public ParticleSystem particulasPequeno;
	public GameObject sombraEtereo;
	public GameObject sombraMatPequeno;
	public GameObject sombraMatGrande;


	public void Troca(Jogador.ModoDetalhe modoDetalheAntes, Jogador.ModoDetalhe modoDetalheDepois){
	
		// desativa anterior

		switch (modoDetalheAntes) {
			
		case Jogador.ModoDetalhe.Etereo:
			jogadorEtereo.SetActive (false);
			break;
		default:
			jogadorMaterial.SetActive (false);
			break;
		}

		// Faz efeito social
		FazEfeitoVisual(modoDetalheDepois);


		// ativa posterior

		switch (modoDetalheDepois) {

		case Jogador.ModoDetalhe.Etereo:
			jogadorEtereo.transform.rotation = jogadorMaterial.transform.rotation;
			jogadorEtereo.SetActive (true);
			sombraEtereo.SetActive (true);
			sombraMatGrande.SetActive (false);
			sombraMatPequeno.SetActive (false);
			break;
		case Jogador.ModoDetalhe.PegadaGrande:
			jogadorMaterialRenderer.material = jogadorMaterial_grande;
			jogadorMaterial.transform.rotation = jogadorEtereo.transform.rotation;
			jogadorMaterial.SetActive (true);
			sombraEtereo.SetActive (false);
			sombraMatGrande.SetActive (true);
			sombraMatPequeno.SetActive (false);
			break;
		case Jogador.ModoDetalhe.PegadaPequena:
			jogadorMaterialRenderer.material = jogadorMaterial_pequeno;
			jogadorMaterial.transform.rotation = jogadorEtereo.transform.rotation;
			jogadorMaterial.SetActive (true);
			sombraEtereo.SetActive (false);
			sombraMatGrande.SetActive (false);
			sombraMatPequeno.SetActive (true);
			break;
		default:
			break;
		}
	
	
	}

	void FazEfeitoVisual(Jogador.ModoDetalhe modo){
	
		switch (modo) {
		case Jogador.ModoDetalhe.Etereo:
			particulasEtereo.Play ();
			break;
		case Jogador.ModoDetalhe.PegadaPequena:
			particulasPequeno.Play ();
			break;
		case Jogador.ModoDetalhe.PegadaGrande:
			particulasGrande.Play ();
			break;
		default:
			break;
		}
	
	}

}
