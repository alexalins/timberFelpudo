using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodPrincipal : MonoBehaviour {

	public GameObject jogadorFelpudo;
	public GameObject jogadorIdle;
	public GameObject jogadorBate;

	float escalaJogadorHorizontal;

	void Start () {
		//posicao do jogador
		escalaJogadorHorizontal = transform.localScale.x;
		//desativa ele batendo
		jogadorBate.SetActive (false);
	}

	void Update () {
		//clique do mouse ou toque
		if(Input.GetButtonDown("Fire1")){
			// olha onde foi a posicao pra saber sefoi direita ou esquerda
			if (Input.mousePosition.x > Screen.width / 2) {
				//chama a funcao
				bateDireita ();
			} else {
				bateEsquerda ();
			}
		}
	}

	void bateEsquerda(){
		//desativa 
		jogadorIdle.SetActive (false);
		//ativa
		jogadorBate.SetActive (true);
		//muda a posicao pra esquerda
		jogadorFelpudo.transform.position = new Vector2 (1.1f, jogadorFelpudo.transform.localScale.y);
		jogadorFelpudo.transform.localScale = new Vector2 (escalaJogadorHorizontal, jogadorFelpudo.transform.localScale.y);
		//invoca o metodo
		Invoke ("VoltaAnimacao", 0.25f);
	}

	void bateDireita(){
		//desativa 
		jogadorIdle.SetActive (false);
		//ativa
		jogadorBate.SetActive (true);
		//muda a posicao pra esquerda
		jogadorFelpudo.transform.position = new Vector2 (-1.1f, jogadorFelpudo.transform.localScale.y);
		jogadorFelpudo.transform.localScale = new Vector2 (-escalaJogadorHorizontal, jogadorFelpudo.transform.localScale.y);
		//invoca o metodo
		Invoke ("VoltaAnimacao", 0.25f);
	}

	//volta 
	void VoltaAnimacao(){
		jogadorIdle.SetActive (true);
		jogadorBate.SetActive (false);
	}
}
