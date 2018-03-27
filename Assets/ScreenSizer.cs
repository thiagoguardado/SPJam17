using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSizer : MonoBehaviour {

	public static ScreenSizer instancia;


	void Awake(){

		if (instancia == null) {

			instancia = this;
		
		} else {
		
			if (instancia != this) {
			
				Destroy (gameObject);

			}

		}

		DontDestroyOnLoad (gameObject);


		Screen.SetResolution(410,656,false);

	}

	void Update(){
	
		UpdateResolution ();
	
	}

	// Use this for initialization
	void UpdateResolution(){
	
		int height = Screen.height;

		int width = height * 10 / 16;
	
		Screen.SetResolution(width,height,false);

	}
}
