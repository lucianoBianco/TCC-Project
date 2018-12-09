using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;
using UnityEngine.Video;

public class LevelLoader : MonoBehaviour {
	// classe que controla o menu inicial
	public Animator anim;
	public GameObject fadeImage, mainMenu, optionsMenu, continueButton;
	public Light spot1, spot2, spot3;
	private bool gameStarto = false, videoEnded = true;

	public VideoPlayer videoPlayer;
	public AudioSource audioPlayer;
	public GameObject videoPlayerObj;
	public GameObject blackScreen;
	public Slider volumeSlider;

	public GameObject camera, virtualCamera2;
	public CinemachineVirtualCamera virtualCamera3;
	void Start(){
		if(PlayerPrefsX.GetVector3("JohannaPos") != Vector3.zero){
			continueButton.SetActive(true);
		}
		Application.backgroundLoadingPriority = ThreadPriority.Low;
	}

// inicia o level do zero
	public void LoadLevel(int sceneIndex){
		PlayerPrefsX.SetVector3("JohannaPos", new Vector3(-3, 2.5f,5));
		PlayerPrefsX.SetVector3("AsobrabPos", new Vector3(-31.19f, 2.69f,0.27f));
		PlayerPrefsX.SetVector3("TommyPos", new Vector3 (-108f, 8.3f, 82f));
		PlayerPrefs.SetInt("AsobrabTracker", 0);
		PlayerPrefs.SetFloat("ambientIntensity", 0.4f);
		PlayerPrefs.SetFloat("directionalIntensity", 0f);
		PlayerPrefsX.GetBool("GameStart", true);
		PlayerPrefsX.SetBool("Puzzle1Complete", false);
		PlayerPrefsX.SetBool("Puzzle2Complete", false);
		PlayerPrefsX.SetBool("Puzzle3Complete", false);
		PlayerPrefsX.SetBool("Puzzle4Complete", false);
		PlayerPrefsX.SetBool("Puzzle5Complete", false);
		optionsMenu.SetActive(false);
		StartCoroutine(NewGame());
	}
// carrega o level em background
	IEnumerator LoadAsynchronously (int sceneIndex){
		AsyncOperation operation = SceneManager.LoadSceneAsync (sceneIndex);

		while (!operation.isDone) {

			float progress = Mathf.Clamp01 (operation.progress / 0.9f);
			Debug.Log (progress);
			yield return null;
		}
	}

	IEnumerator LoadDelay(int sceneIndex){
		yield return new WaitForSeconds (5f);
		StartCoroutine (LoadAsynchronously (sceneIndex));
		yield return new WaitForSeconds (7f);
		anim.SetTrigger ("FadeOut2");
		yield return new WaitForSeconds (2f);
	}



	public void Quit(){
		Application.Quit ();
	}


	void Update(){
		if(camera.transform.position == virtualCamera2.transform.position && !gameStarto){
			mainMenu.SetActive(true);
		}else{
			mainMenu.SetActive(false);
		}
		videoPlayer.loopPointReached +=LoadAfterVideo;
	}
//controla o menu de volume
	public void ValueChangeCheck(){

		Debug.Log(volumeSlider.value);
		audioPlayer.volume = volumeSlider.value;
    }

	public void GameStart(){
		SceneManager.LoadScene ("Caverna");
		optionsMenu.SetActive(false);
	}
// abre o menu de volumes
	public void OptionMenu(){
		optionsMenu.SetActive (!optionsMenu.activeInHierarchy);
	}

	public void FadeToLevel(){
		anim.SetTrigger ("FadeOut");
	}
// começa o jogo a partir de um jogo salvo
	public void Continue(int sceneIndex){
		StartCoroutine (FadeDelay(sceneIndex));
	}
// mostra a tela de carregamento
	IEnumerator FadeDelay(int sceneIndex){
		anim.SetTrigger ("FadeOut");
		yield return new WaitForSeconds (2.5f);
		videoPlayerObj.SetActive(false);
		mainMenu.SetActive(false);
		virtualCamera3.Priority = 12;
		RenderSettings.reflectionIntensity = 0;
		RenderSettings.ambientIntensity = 0;
		yield return new WaitForSeconds (2f);
		float light = 0;
		StartCoroutine (LoadDelay (sceneIndex));
		while (light < 2f) {
			spot1.intensity = light;
			spot2.intensity = light;
			spot3.intensity = light;
			light += 0.01f;
			yield return null;
		}
	}

// ativa a cutscene inicial
	IEnumerator NewGame(){
		anim.SetTrigger ("FadeOut");
		while(audioPlayer.volume > 0.0001f){
			audioPlayer.volume -= 0.005f;
			yield return null;
		}
		blackScreen.SetActive(true);
		gameStarto = true;
		videoPlayerObj.SetActive(true);
		videoPlayer.Play ();
		audioPlayer.Play();
	}
// começa o carregamento apos a cutscene inicial
	void LoadAfterVideo(UnityEngine.Video.VideoPlayer vp){
		if(videoEnded){
			StartCoroutine (FadeDelay(1));
			videoEnded = false;
			blackScreen.SetActive(false);
		}
	}



}
