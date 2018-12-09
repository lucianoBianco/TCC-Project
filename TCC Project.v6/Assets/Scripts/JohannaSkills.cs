using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JohannaSkills : MonoBehaviour {

	// controlador das skills de luz da johanna
	public bool skill;
	bool musicMode = false;
	PlayerMotor motor;
	PlayerController controller;
	float timer, skillTimer;
	int nota;
	private AudioSource source;
	public GameObject luz;
	public Material glowMat;
	float intensidade = 0.2f;
	public AudioClip audio1, audio2, audio3, audio4;
	public GameObject arco, violino;
    public PuzzleLuz[] puzzles;
    private Animator anim;

	
	private bool isFirstTimeSkill = true;
	public Dialogue[] firstSkillBark;
	public BarkTrigger[] triggerBark;
	private int index = 0;
	private bool travaBark = true;
	public Quest quest;
	public GlobalVariablesCaverna globalVarsCav;
	public GameObject questHolder;
	public Text questText;

	// Use this for initialization
	void Start () {
		controller = GetComponent<PlayerController> ();
		anim = GetComponentInChildren<Animator> ();
		motor = transform.GetComponent<PlayerMotor> ();
		source = GetComponent<AudioSource> ();
		glowMat.EnableKeyword ("_EMISSION");
	}

	// Update is called once per frame
	void Update ()
	{

		if(!GlobalVariablesCaverna.isPaused){
			AttLuz (intensidade);

			switch (controller.myController) {
			case PlayerController.Controller.Active:
				timer += Time.deltaTime;
				//saca o instrumento
				if (Input.GetButtonDown ("Fire1")) {
					if(!motor.movBlocked){
						musicMode = true;
						timer = 0;
						arco.SetActive (true);
						violino.SetActive (true);
						anim.SetBool("GuitarOn", true);
					}
				}
				// ativa os inputs das notas
				if (musicMode)
					MusicMode ();

				if (Input.GetButtonDown ("Ação3")) {
					Activate ();
				}
				// reduz gradualmente a luz gerada pela skill
			if (luz.GetComponent<Light> ().range > 0) {
				luz.GetComponent<Light> ().range -= 0.5f * Time.deltaTime;
	            foreach (PuzzleLuz puzzle in puzzles)
	            {
	            	// reduz a luz dos puzzles em progresso
	                puzzle.range = luz.GetComponent<Light>().range;
	            }
	        }
	        else
	        {
	            skill = false;
	        }
	        if (intensidade > 0.2f)
	        {
	            intensidade -= 0.075f * Time.deltaTime;
	            foreach (PuzzleLuz puzzle in puzzles)
	            {
	                puzzle.intensidade = intensidade;
	            }
	        }
				break;
			}
		}
    }



	private void MusicMode()
	{
		// gerencia os inputs para cada nota e checa as condições para gerar uma skill
		if(Input.GetButtonDown("Number1")){
			nota = 1;
			timer = 0;
			source.PlayOneShot (audio1);
			anim.SetTrigger("Play1");
		}else if(Input.GetButtonDown("Number2")){
			if(nota == 1){
				nota = 2;
			}
			timer = 0;
			source.PlayOneShot (audio2);
			anim.SetTrigger("Play2");
		}else if(Input.GetButtonDown("Number3")){
			if(nota == 2){
				nota = 3;
			}
			timer = 0;
			source.PlayOneShot (audio3);
			anim.SetTrigger("Play3");
		}
		else if(Input.GetButtonDown("Number4")){
			if(nota == 3){
				nota = 0;
				Activate ();
			}
			timer = 0;
			//source.PlayOneShot (audio4);
			source.PlayOneShot (audio2);
			anim.SetTrigger("Play4");
		}
// desativa o instrumento apos muito tempo sem registrar um input de nota
		if (timer >= 8)
		{
			musicMode = false;;
			arco.SetActive (false);
			violino.SetActive (false);
			anim.SetBool("GuitarOn", false);
		}
// reseta o pregresso da skill caso demore muito tempo
		else if (timer >= 10)
		{
			nota = 0;
		}
	}


// ativa a skill, gera barks e ativa o delay para enconder o instrumento apos o uso da skill
	public void Activate(){
		if(isFirstTimeSkill && !travaBark && PlayerPrefsX.GetBool("GameStart")){
			gameObject.GetComponentInChildren<BarkManager>().RemoteBark(firstSkillBark[index]);
			TrigaQuestPuzzle();
			triggerBark[index].condition = true;
			index++;
			isFirstTimeSkill = false;
		}
		luz.GetComponent<Light> ().range = 20;
		musicMode = false;
		intensidade = 2.5f;
		timer = 0;
		StartCoroutine (DelayHideInstrument ());
        skill = true;
        travaBark = false;
	}
// desativa o instrumento
	private IEnumerator DelayHideInstrument(){
		yield return new WaitForSeconds (3f);

		arco.SetActive (false);
		violino.SetActive (false);
		anim.SetBool("GuitarOn", false);
	}


	private void AttLuz (float i){
		glowMat.SetColor ("_EmissionColor", new Color(1f,1f,1f) * i);
		DynamicGI.UpdateEnvironment ();
	}

	private void TrigaQuestPuzzle(){
		if(quest.prioridade >= globalVarsCav.prioridadeQuestAtual){
			questText.text = quest.descricao;
			questHolder.SetActive(true);

		}
	}
}
