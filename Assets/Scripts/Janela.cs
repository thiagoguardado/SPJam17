using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Janela : MonoBehaviour {

	public float timeRunning = 3f;

	void OnTriggerStay(Collider col ){


		col.SendMessage ("IniciarCorrida",SendMessageOptions.DontRequireReceiver);

	
	}


}
