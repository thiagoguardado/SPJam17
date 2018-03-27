using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Fantasma : MonoBehaviour {


	public float velocidade;
	public GameObject campoVisao;
	public GameObject campoVisaoFenda;
	public TamanhoPegada pegadaASeguir;
	private Rigidbody rb;
	private Peso pesoScript;
	private FantasmaAudio audioScript;
	private FantasmaAnimator fantasmaAnimator;

	private bool acordado;
	public bool encontrouFenda = false;
	public float distanciaParVitoria;

	// movimento
	public bool movendo;
	private bool correndo;
	public float distanciaCorrida;
	public float corridaMultiplicador;
	private Vector3 destinoCorrida;


	// proxima pegada
	private Pegada destinoPegada;
	private Vector3 destino{
		get{ 
			return destinoPegada.gameObject.transform.position;
		}
	}
	private int destinoPegadaIndex{
		get{ 
			if (destinoPegada != null) {
				return destinoPegada.pegadaIndex;
			} else {
				return -1;
			}
		}
	}
	public float limiteDistanciaFimPegada = 0.5f;

	void Awake(){

		pesoScript = GetComponent<Peso> ();
		rb = GetComponent<Rigidbody> ();
		audioScript = GetComponent<FantasmaAudio> ();
		fantasmaAnimator = GetComponent<FantasmaAnimator> ();

	}

	void Update(){


		if (movendo && acordado) {
		
			Mover ();

		} else if (correndo) {
		
			Correr ();
		
		}

		ResetVelocidades ();

	}

	void OnCollisionEnter(Collision col){


		if (col.gameObject.tag == "Parede") {

				Bater ();

		} else if (col.gameObject.tag == "Fantasma" || col.gameObject.tag == "Player"){

				if (!encontrouFenda) {

					Bater ();

			}

		}


	}

	void Bater ()
	{
		Dormir ();
	}


	public void IniciarCorrida(){

		movendo = false;
		correndo = true;

		destinoCorrida = transform.position - transform.forward * distanciaCorrida;

		destinoPegada = null;

		audioScript.StartCorrer ();

		fantasmaAnimator.ComecarCorrer ();

	}

	public void Acordar(){

		if (!acordado) {

			acordado = true;

			StartCoroutine (AcordarEProcurar ());

		}

	}

	IEnumerator AcordarEProcurar(){

		fantasmaAnimator.Acordar();

		yield return new WaitForSeconds (0.5f);

		ProcurarObjetos ();

	}


	void Mover(){

		if (destinoPegada != null) {

			Vector3 direcao = destinoPegada.gameObject.transform.position - transform.position;

			Vector3 deslocamento = new Vector3 (direcao.x, 0, direcao.z).normalized * velocidade * Time.deltaTime;

			transform.position += deslocamento;
			transform.LookAt (transform.position + deslocamento);

			float distancia = Vector3.Distance (destinoPegada.gameObject.transform.position, transform.position);

			if (distancia <= limiteDistanciaFimPegada) {

				destinoPegada.ApagarPegada ();

				ProcurarObjetos ();

			}

		} else {
		
			movendo = false;
		
		}

	}

	void Correr ()
	{
		Vector3 deslocamento = new Vector3 (destinoCorrida.x, 0, destinoCorrida.z).normalized * velocidade * corridaMultiplicador * Time.deltaTime;

		transform.position += deslocamento;
		transform.LookAt (destinoCorrida);

		if (Vector3.Distance (destinoCorrida, transform.position) <= limiteDistanciaFimPegada) {

			Dormir ();

		}
	}

	void ProcurarObjetos ()
	{
		switch (Enxergar ()) {
		case 0:
			EncontrarFenda ();
			break;
		case 1:
			ComecarMover ();
			break;
		case 2:
			Dormir ();
			break;
		default:
			break;
		}
			
	}

	int Enxergar(){
	
		// procura fenda

		bool achouFenda = ProcuraFenda ();

		if (achouFenda) {
			encontrouFenda = true;
			return 0;
		} 

		// procura pegadas

		float raio = campoVisao.transform.lossyScale.x / 2;

		// encontra todos os objetos noc ampo de visao
		Collider[] cols = Physics.OverlapSphere (transform.position, raio);

		// ordena por distancia
		Collider[] orderdCols = cols.OrderBy(t => Vector3.Distance(t.gameObject.transform.position, transform.position)).ToArray();

		bool encontrouProximaPegada = false;
		int proximaPegadaIndex = 1000;
		Vector3 proximodestino = transform.position;
		Pegada proximaPegada = new Pegada ();


		// checa cada um dos objetos no campo de visao
		for (int i = 0; i < orderdCols.Length; i++) {

			string _tag = orderdCols [i].tag;

			if (_tag == "Pegada") {
				
				Pegada pegada = orderdCols [i].GetComponent<Pegada> ();
			
				if (pegada.tamanhoPegada == pegadaASeguir) {

					if (pegada.pegadaIndex > destinoPegadaIndex) {

						if (pegada.pegadaIndex < proximaPegadaIndex) {

							proximaPegadaIndex = pegada.pegadaIndex;
							proximaPegada = pegada;
							encontrouProximaPegada = true;

						}

					}

				}
					
			}


		}


		if (encontrouProximaPegada) {

			destinoPegada = proximaPegada;

			return 1;

		} else {
		
			// procura novamente fenda
			encontrouFenda = false;

			bool achouFenda2 = ProcuraFenda ();

			if (achouFenda2) {
				
				encontrouFenda = true;
				return 0;

			} else {
			
				return 2;
			
			}

		
		
		}


	}

	bool ProcuraFenda ()
	{

		float raioFenda = campoVisaoFenda.transform.lossyScale.x / 2;
		// encontra todos os objetos noc ampo de visao
		Collider[] colsFenda = Physics.OverlapSphere (transform.position, raioFenda);
		bool achouFenda = false;
		// checa cada um dos objetos no campo de visao
		for (int i = 0; i < colsFenda.Length; i++) {
			string _tag = colsFenda [i].tag;
			if (_tag == "Fenda" && !encontrouFenda) {
				achouFenda = true;
				break;
			}
		}
		return achouFenda;
	}

	void EncontrarFenda(){
	
		fantasmaAnimator.Fenda ();

		audioScript.Falar ();

		Repousar ();

	}

	void ComecarMover ()
	{
		if (!movendo) {
			movendo = true;

			fantasmaAnimator.ComecarAndar ();

			audioScript.StartAndar ();
		}


	}

	void Dormir ()
	{
		Repousar ();
		encontrouFenda = false;
	}

	void Repousar(){
		
		movendo = false;
		correndo = false;
		acordado = false;


		fantasmaAnimator.PararAndar ();
		fantasmaAnimator.PararCorrer ();

		audioScript.PararAndar ();

	}

	void ResetVelocidades ()
	{
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
	}

}
