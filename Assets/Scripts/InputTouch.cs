using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTouch : MonoBehaviour {

	public Jogador jogador;

	// Update is called once per frame
	void Update () {

		if (!GameController.isPaused && !jogador.isPaused) {

			if (Input.GetMouseButton (0)) {

				SegurarClique (Input.mousePosition);
		
			} else if (Input.touchCount > 0 && (Input.GetTouch (0).phase != TouchPhase.Canceled || Input.GetTouch (0).phase != TouchPhase.Ended)) {
			
				Vector2 touchpos = Input.GetTouch (0).position;
				SegurarClique (new Vector3(touchpos.x,touchpos.y,0));

			}


			if (Input.GetMouseButtonUp (0)) {

				ExecutarClique (Input.mousePosition);
		
			} else if (Input.touchCount > 0 && (Input.GetTouch (0).phase == TouchPhase.Canceled || Input.GetTouch (0).phase == TouchPhase.Ended)) {

				Vector2 touchpos = Input.GetTouch (0).position;
				ExecutarClique (new Vector3(touchpos.x,touchpos.y,0));

			}

		}
	}


	void ExecutarClique (Vector3 position)
	{
		// desativa calculo movimento
		jogador.DesativarCalcularMovimento ();

		RaycastHit hit;
		LayerMask lm = LayerMask.GetMask ("Floor");

		if (Physics.Raycast (Camera.main.ScreenPointToRay (position),out hit,1000,lm)) {

			Vector3 posicao = new Vector3 (hit.point.x, jogador.transform.position.y, hit.point.z);
			jogador.ComecarMovimento (posicao);

		}
			
	}

	void SegurarClique(Vector3 position){
	
		RaycastHit hit;
		LayerMask lm = LayerMask.GetMask ("Floor");

		if (Physics.Raycast (Camera.main.ScreenPointToRay (position), out hit, 1000, lm)) {

			Vector3 posicao = new Vector3 (hit.point.x, jogador.transform.position.y, hit.point.z);
			jogador.CalcularMovimento (posicao);

		} else {

			jogador.DesativarCalcularMovimento ();
		
		}
	
	}
}
