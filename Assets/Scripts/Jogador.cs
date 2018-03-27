using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jogador : MonoBehaviour {

	public enum Modo
	{
		Etereo,
		Material
	}

	public enum ModoDetalhe
	{
		Etereo,
		PegadaPequena,
		PegadaGrande
	}

	public Modo modo {
		get{
			if (modoDetalhe == ModoDetalhe.Etereo) {
				return Modo.Etereo;
			} else {
				return Modo.Material;
			}
		}
	}
	public ModoDetalhe modoDetalhe = ModoDetalhe.Etereo;

	public HUD hud;
	public Animator cameraAnimator;
	public bool isPaused;

	private PlayerAudio audioScript;
	private JogadorMeshAnimado meshesScript;
	private JogadorAnimator animatorScript;

	// materiais e collider
	public Renderer rend;
	public Material materialEtereo;
	public Material materialMaterial;
	public Collider col;

	// pegadas
	public int maximoTintaPegadas;
	private int contagemTintaPegadas = 0;
	public float percTinta{
		get{ 
			return (1 - (float)contagemTintaPegadas / maximoTintaPegadas);
		}
	}
	public TamanhoPegada pegadaAtual = TamanhoPegada.Nenhuma;
	public GameObject pegadaPequena;
	public GameObject pegadaGrande;
	public float distanciaParaPegadaPequena;
	public float distanciaParaPegadaGrande;
	private int indexUltimaPegada;
	public Transform pegadasPaiTransform;
	public float tintaAGastar;
	public bool calculandoTinta;

	// movimento
	private Vector3 destino;
	public bool estaMovendo = false;
	public float limiteFinalDeslocamento;
	public float velocidade;
	private float distanciaCaminhada;
	private bool comecouAndar = false;
	public SetasController seta;
	private Rigidbody rb;

	//corrida
	private bool estaCorrendo = false;
	public float corridaMultiplicador;
	public float distanciaCorrida;

	//respawn
	public Transform respawnPoint;

	public bool primeiraChamada = false;
	public bool primeiroApagar = false;

	void Awake(){
		rb = GetComponent<Rigidbody> ();
		audioScript = GetComponent<PlayerAudio> ();
		meshesScript = GetComponent<JogadorMeshAnimado> ();
		animatorScript = GetComponent<JogadorAnimator> ();
	}

	void Update(){

		ResetVelocidades ();

		Mover ();

		CriarPegadas ();

	}


	void OnCollisionEnter(Collision col){

		if (col.gameObject.tag == "Parede" || col.gameObject.tag == "Fantasma") {

			BaterNaParede ();
		
		}
	
	
	}

	void ResetVelocidades ()
	{
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
	}


	public void IniciarCorrida(){

		if (!estaCorrendo) {

			ApagarTodasPegadas ();
			DesativarPegada ();

			hud.DesativarToggles ();

			estaMovendo = false;
			estaCorrendo = true;

			destino = transform.position - transform.forward * distanciaCorrida;

			animatorScript.ComecarCorrer ();

		}
	}


	public void ComecarMovimento(Vector3 _destino){

		estaMovendo = true;
		destino = _destino;

		animatorScript.ComecarAndar ();

		switch (modoDetalhe) {
		case ModoDetalhe.Etereo:
			audioScript.StartPlayingEterea();
			break;
		case ModoDetalhe.PegadaPequena:
			audioScript.StartPlayingPegadasPequenas();
			break;
		case ModoDetalhe.PegadaGrande:
			audioScript.StartPlayingPegadasGrandes();
			break;
		default:
			break;
		}

	
	}

	void Mover ()
	{

		if (estaMovendo) {

			Vector3 direcao = destino - transform.position;

			Vector3 deslocamento = new Vector3 (direcao.x, 0, direcao.z).normalized * velocidade * Time.deltaTime;
			distanciaCaminhada += Mathf.Abs (deslocamento.magnitude);
		
			transform.position += deslocamento;
			transform.LookAt (transform.position + direcao);

			if (Vector3.Distance (destino, transform.position) <= limiteFinalDeslocamento) {

				PararMovimento ();

			}

		}

		if (estaCorrendo) {


			Vector3 direcao = destino - transform.position;

			Vector3 deslocamento = new Vector3 (direcao.x, 0, direcao.z).normalized * velocidade * corridaMultiplicador * Time.deltaTime;
			distanciaCaminhada += Mathf.Abs (deslocamento.magnitude);

			transform.position += deslocamento;
			transform.LookAt (transform.position + direcao);

			if (Vector3.Distance (destino, transform.position) <= limiteFinalDeslocamento) {

				comecouAndar = false;
				estaCorrendo = false;

				animatorScript.PararCorrer ();

			}

		
		}

	}


	void BaterNaParede(){

		animatorScript.Bater ();

		PararMovimento ();

	}

	void PararMovimento ()
	{
		comecouAndar = false;
		estaMovendo = false;
		estaCorrendo = false;

		audioScript.StopPlaying ();

		animatorScript.PararAndar ();
	}

	public void AtivarPegadaPequena(){

		if (pegadaAtual != TamanhoPegada.Pequena) {

			pegadaAtual = TamanhoPegada.Pequena;

			indexUltimaPegada = 0;

			AtivarMaterial (ModoDetalhe.PegadaPequena);

			// muda modo
//			modoDetalhe = ModoDetalhe.PegadaPequena;

//			ImprimirPegada (pegadaPequena);

		}

	}

	public void AtivarPegadaGrande(){

		if (pegadaAtual != TamanhoPegada.Grande) {
			
			pegadaAtual = TamanhoPegada.Grande;

			indexUltimaPegada = 0;

			AtivarMaterial (ModoDetalhe.PegadaGrande);

//			// muda modo
//			modoDetalhe = ModoDetalhe.PegadaGrande;

//			ImprimirPegada (pegadaGrande);

		}

	}

	public void DesativarPegada(){

		DesativarMaterial ();

		pegadaAtual = TamanhoPegada.Nenhuma;

	}


	void CriarPegadas ()
	{

		if (estaMovendo) {

			if (pegadaAtual == TamanhoPegada.Pequena) {

				if (!comecouAndar && distanciaCaminhada > 0) {

					comecouAndar = true;
					ImprimirPegada (pegadaPequena);

				} else if (distanciaCaminhada >= distanciaParaPegadaPequena) {

					ImprimirPegada (pegadaPequena);
			
				}
		
			} else if (pegadaAtual == TamanhoPegada.Grande) {
		
				if (!comecouAndar && distanciaCaminhada > 0) {

					comecouAndar = true;
					ImprimirPegada (pegadaGrande);


				} else if (distanciaCaminhada >= distanciaParaPegadaGrande) {

					ImprimirPegada (pegadaGrande);

				}

			}

		}
	}


	void AtivarMaterial(ModoDetalhe novoModo){
	
//		if (modo != Modo.Material) {

			// muda material
			rend.material = materialMaterial;

			// ativa box collider
			col.isTrigger = false;

			// zera contador distancia
			distanciaCaminhada = 0f;

			meshesScript.Troca (modoDetalhe, novoModo);
		
//		}

		modoDetalhe = novoModo;

		//animacao camera
		cameraAnimator.SetBool ("material", true);

	}

	void DesativarMaterial(){
	
		if (modo == Modo.Material) {

			meshesScript.Troca (modoDetalhe, ModoDetalhe.Etereo);

			// muda modo
			modoDetalhe = ModoDetalhe.Etereo;

			// ativa box collider
			col.isTrigger = true;

			// muda material
			rend.material = materialEtereo;

			audioScript.Eterializar ();

			// animacao camera
			cameraAnimator.SetBool ("material", false);

		}

	}

	public void ApagarPegadasEDesativar(){

		primeiroApagar = true;

		ApagarTodasPegadas ();

		DesativarPegada ();

		hud.DesativarToggles ();

		animatorScript.Apagar ();

	}

	public void ApagarTodasPegadas(){

		Pegada[] pegadas = GameObject.FindObjectsOfType<Pegada> ();
		for (int i = 0; i < pegadas.Length; i++) {
			pegadas [i].ApagarPegada ();
		}

		indexUltimaPegada = 0;

		contagemTintaPegadas = 0;



	}

	public void AcordarFantasmas(){

		primeiraChamada = true;

		PararMovimento ();

		animatorScript.Chamar ();

		Fantasma[] fantasmas = GameObject.FindObjectsOfType<Fantasma> ();

		for (int i = 0; i < fantasmas.Length; i++) {
			fantasmas [i].Acordar ();
		}

	}



//	void ContarTempoMaterial(){
//
//		contagemTintaPegadas += Time.deltaTime;
//
//		if (contagemTintaPegadas >= maximoTintaPegadas) {
//		
//			UltrapassouTempoMaterial ();
//		
//		}
//
//
//	}
//
//
//	void UltrapassouTempoMaterial(){
//	
//		ApagarTodasPegadas ();
//
//		DesativarMaterial ();
//
//	}

	void ImprimirPegada (GameObject pegada)
	{
		
		if (contagemTintaPegadas >= maximoTintaPegadas) {
		
			UltrapassouLimitTinta ();

			return;
		
		}

		indexUltimaPegada++;
		GameObject go = Instantiate (pegada, transform.position + Vector3.up * 0.1f, transform.rotation,pegadasPaiTransform);
		Pegada peg = go.GetComponent<Pegada> ();
		peg.pegadaIndex = indexUltimaPegada;
		contagemTintaPegadas = Mathf.Min (contagemTintaPegadas + peg.gastoTintaPegada, maximoTintaPegadas);
		distanciaCaminhada = 0f;

	}

	void UltrapassouLimitTinta ()
	{

		StartCoroutine (Overheat());


	}

	IEnumerator Overheat(){

		PararMovimento ();

		isPaused = true;

		animatorScript.Overheat ();

		contagemTintaPegadas = 0;

		ApagarTodasPegadas ();

		yield return new WaitForSeconds (0.4f);

		isPaused = false;

		DesativarPegada ();

		hud.DesativarToggles ();

		audioScript.Overheat ();

	
	}


	public void RetirarTintaContador(int tinta){

		contagemTintaPegadas -= tinta;
	
	}

	public void CalcularMovimento (Vector3 posicao)
	{

		seta.ComecarAtivar (modoDetalhe);
		seta.Atulizar (modoDetalhe,posicao);

		if (pegadaAtual != TamanhoPegada.Nenhuma) {
		
			if (!calculandoTinta) {
			
				calculandoTinta = true;
				hud.AtivarCalculoTintaGastar ();

			
			}



			float distancia = Vector3.Distance (transform.position, posicao);
			float distanciaEntrePegadas = 0f;
			Pegada pegadaEquip = new Pegada();

			switch (pegadaAtual) {
			case TamanhoPegada.Pequena:
				distanciaEntrePegadas = distanciaParaPegadaPequena;
				pegadaEquip = pegadaPequena.GetComponent<Pegada>();
				break;
			case TamanhoPegada.Grande:
				distanciaEntrePegadas = distanciaParaPegadaGrande;
				pegadaEquip = pegadaGrande.GetComponent<Pegada>();
				break;
			default:
				break;
			}

			int pegadas = Mathf.FloorToInt(distancia / distanciaEntrePegadas) + 1;
			float energiaGastar = pegadas * pegadaEquip.gastoTintaPegada;

			tintaAGastar = energiaGastar/maximoTintaPegadas;

		}

	}

	public void DesativarCalcularMovimento(){

		calculandoTinta = false;
		hud.DesativarCalculoTintaGastar ();

		seta.Desativar ();
	}

	public void Respawn(){

		transform.position = respawnPoint.position;

		PararMovimento ();

	}
}
