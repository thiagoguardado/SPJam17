using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JogadorAnimator : MonoBehaviour {

	public Animator etereoAnimator;
	public Animator materialAnimator;

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

		etereoAnimator.SetBool ("andar", andar);
		materialAnimator.SetBool ("andar", andar);
		etereoAnimator.SetBool ("correr", correr);
		materialAnimator.SetBool ("correr", correr);

	}

	public void Bater(){

		etereoAnimator.SetTrigger ("bater");
		materialAnimator.SetTrigger ("bater");

	}

	public void Chamar(){

		etereoAnimator.SetTrigger ("chamar");
		materialAnimator.SetTrigger ("chamar");

	}

	public void Apagar(){

		etereoAnimator.SetTrigger ("apagar");
		materialAnimator.SetTrigger ("apagar");

	}

	public void Overheat(){

		etereoAnimator.SetTrigger ("overheat");
		materialAnimator.SetTrigger ("overheat");

	}

}
