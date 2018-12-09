using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTile : MonoBehaviour{

//controlla os tiles do puzzle de luz individualmente


    [HideInInspector]
    public PuzzleLuz parent;
    private bool tileLiberado = true;
    public bool isAtivado;

//verifica se o jogador está sobre o tile e com a skill ativada
    void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player" &&
                other.gameObject.GetComponent<PlayerController>().myController == PlayerController.Controller.Active &&
                other.GetComponent<JohannaSkills>() != null &&
                other.GetComponent<JohannaSkills>().skill &&
                tileLiberado)
        {
            parent.AtualizarEstado(this.gameObject);
            tileLiberado = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player" &&
                other.gameObject.GetComponent<PlayerController>().myController == PlayerController.Controller.Active)
            tileLiberado = true;
    }
}
