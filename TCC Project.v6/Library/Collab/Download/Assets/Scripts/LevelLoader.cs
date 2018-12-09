using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {
	public Animator anim;
	public GameObject fadeImage, mainMenu, optionsMenu, continueButton;
	public Light spot1, spot2, spot3;

	void Start(){
		if(PlayerPrefsX.GetVector3("JohannaPos") != Vector3.zero){
			continueButton.SetActive(true);
		}
		Application.backgroundLoadingPriority = ThreadPriority.Low;
	}

	public void LoadLevel(int sceneIndex){
		PlayerPrefsX.SetVector3("JohannaPos", new Vector3(-3, 2.5f,5));
		PlayerPrefsX.SetVector3("AsobrabPos", new Vector3(-31.19f, 2.69f,0.27f));
		PlayerPrefs.SetInt("AsobrabTracker", 0);
		PlayerPrefs.SetFloat("ambientIntensity", 0.4f);
		PlayerPrefs.SetFloat("directionalIntensity", 0f);
		PlayerPrefsX.GetBool("GameStart", true);
		PlayerPrefsX.SetBool("Puzzle1Complete", false);
		PlayerPrefsX.SetBool("Puzzle2Complete", false);
		PlayerPrefsX.SetBool("Puzzle3Complete", false);
		PlayerPrefsX.SetBool("Puzzle4Complete", false);
		PlayerPrefsX.SetBool("Puzzle5Complete", false);

		StartCoroutine (FadeDelay(sceneIndex));
	}

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


	public void GameStart(){
		SceneManager.LoadScene ("Caverna");
	}

	public void OptionMenu(){
		optionsMenu.SetActive (!optionsMenu.activeInHierarchy);
	}

	public void FadeToLevel(){
		anim.SetTrigger ("FadeOut");
	}

	public void Continue(int sceneIndex){
		StartCoroutine (FadeDelay(sceneIndex));
	}

	IEnumerator FadeDelay(int sceneIndex){
		anim.SetTrigger ("FadeOut");
		yield return new WaitForSeconds (2f);
		mainMenu.SetActive(false);
		yield return new WaitForSeconds (0.5f);
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
}
