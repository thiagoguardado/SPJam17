using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FantasmaAudio : MonoBehaviour {

	public AudioSource audioSourceWalk;
	public AudioSource audioSourceSFX;

	public AudioClip andarClip;
	public AudioClip falarClip;
	public AudioClip correrClip;

	public void StartAndar(){

		audioSourceWalk.Stop ();
		audioSourceWalk.clip = andarClip;
		audioSourceWalk.loop = true;
		audioSourceWalk.Play ();

	}

	public void StartCorrer(){

		audioSourceWalk.Stop ();
		audioSourceWalk.clip = correrClip;
		audioSourceWalk.loop = true;
		audioSourceWalk.Play ();

	}



	public void PararAndar(){
	
		audioSourceWalk.Stop ();

	}

	public void Falar(){

		audioSourceSFX.PlayOneShot (falarClip);

	}

}
