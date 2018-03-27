using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FantasmaAnimator : MonoBehaviour {

	public Animator fantasmaAnimator;

	public bool andar;
	public bool correr;

	public void ComecarAndar(){
		andar = true;
	}

	public void ComecarCorrer(){
		andar = true;
		correr = true;
	}

	public void PararAndar(){
		andar = false;
	}

	public void PararCorrer(){
		correr = false;
		andar = false;
	}

	void Update(){

		SetupAnimators ();

	}

	public void SetupAnimators(){

		fantasmaAnimator.SetBool ("andar", andar);
		fantasmaAnimator.SetBool ("correr", correr);

	}

	public void Bater(){

		fantasmaAnimator.SetTrigger ("bater");

	}

	public void Acordar(){

		fantasmaAnimator.SetTrigger ("acordar");

	}

	public void Fenda(){

		fantasmaAnimator.SetTrigger ("fenda");

	}





}
