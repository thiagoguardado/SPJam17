using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public AudioSource audioSource;
	public AudioSource sfxSource;

	public AudioClip menuClip;
	public AudioClip level1clip;
	public AudioClip level2clip;

	public AudioClip buttonPressclip;
	public AudioClip menuPausaOpen;

	public static AudioManager instancia;

	void Awake(){

		if (instancia == null) {

			instancia = this;
		
		} else {
		
			if (instancia != this) {
				Destroy (gameObject);
			} 

		}

		DontDestroyOnLoad (gameObject);
		ChangeToMenuBMG ();

	}

	public void ChangeToMenuBMG(){

		StartCoroutine(FadeAudioOutAndIn(1f,menuClip));
//		audioSource.Stop ();
//		audioSource.clip = menuClip;
//		audioSource.loop = true;
//		audioSource.Play ();
	
	}

	IEnumerator FadeAudioOutAndIn(float fadeDuration, AudioClip nextClip){

		float contaTempo = 0f;

		while (contaTempo <= fadeDuration) {

			contaTempo += Time.deltaTime;
			audioSource.volume = 1 - contaTempo / fadeDuration;
			yield return null;

		}

		audioSource.Stop ();
		audioSource.clip = nextClip;
		audioSource.loop = true;
		audioSource.Play ();

		contaTempo = 0f;

		while (contaTempo <= fadeDuration) {

			contaTempo += Time.deltaTime;
			audioSource.volume = contaTempo / fadeDuration;
			yield return null;

		}
			
	
	}


	public void ChangeToLevelAudio(int levelNumber){

		AudioClip clip;

		switch (levelNumber) {
			
		case 1:
			clip = level1clip;
			break;
		case 2:
			clip = level1clip;
			break;
		case 3:
			clip = level1clip;
			break;
		case 4:
			clip = level2clip;
			break;
		case 5:
			clip = level2clip;
			break;
		case 6:
			clip = level1clip;
			break;

		default:
			clip = level1clip;
			break;
		}

		StartCoroutine(FadeAudioOutAndIn(1f,clip));

//		audioSource.Stop ();
//		audioSource.clip = clip;
//		audioSource.loop = true;
//		audioSource.Play ();
	}

	public void PlayClipBotao(){

		sfxSource.PlayOneShot (buttonPressclip);
	}

	public void PlayPausa(){

		sfxSource.PlayOneShot (menuPausaOpen);
	}

}
