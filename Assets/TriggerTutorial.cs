using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTutorial : MonoBehaviour {


	public bool jogadorEntrou = false;


	void OnTriggerEnter(Collider col){

		if (col.tag == "Player") {
		
			jogadorEntrou = true;

		}

	}

}
