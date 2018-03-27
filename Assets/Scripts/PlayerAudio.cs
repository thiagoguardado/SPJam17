using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour {

	public AudioSource audioSourceAndar;
	public AudioSource audioSourceSFX;

	public AudioClip overheat;
	public AudioClip footstepsPequeno;
	public AudioClip footstepsGrande;
	public AudioClip formaEtereaWalk;
	public AudioClip formaMaterial;
	public AudioClip formaEterea;


	public void Overheat(){
		audioSourceSFX.Stop ();
		audioSourceSFX.PlayOneShot (overheat);
	}

	public void Materializar(){
		audioSourceSFX.Stop ();
		audioSourceSFX.PlayOneShot (formaMaterial);
	}

	public void Eterializar(){
		audioSourceSFX.Stop ();
		audioSourceSFX.PlayOneShot (formaEterea);
	}

	public void StartPlayingEterea(){
		audioSourceAndar.Stop ();
		audioSourceAndar.clip = formaEtereaWalk;
		audioSourceAndar.loop = true;
		audioSourceAndar.Play ();
	}

	public void StartPlayingPegadasPequenas(){
		audioSourceAndar.Stop ();
		audioSourceAndar.clip = footstepsPequeno;
		audioSourceAndar.loop = true;
		audioSourceAndar.Play ();
	}

	public void StartPlayingPegadasGrandes(){
		audioSourceAndar.Stop ();
		audioSourceAndar.clip = footstepsGrande;
		audioSourceAndar.loop = true;
		audioSourceAndar.Play ();
	}

	public void StopPlaying(){
		audioSourceAndar.Stop ();
	}



}
