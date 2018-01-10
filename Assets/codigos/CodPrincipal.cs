using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodPrincipal : MonoBehaviour {

	public GameObject jogadorFelpudo;
	public GameObject jogadorIdle;
	public GameObject jogadorBate;

	public GameObject barril;
	public GameObject barrilDireita;
	public GameObject barrilEsquerda;

	float escalaJogadorHorizontal;

	private List<GameObject> listaBlocos;

	bool ladoPersonagem;

	public Text pontuacao;
	int score;

	bool comecou;
	bool acabou;

	public GameObject barra;

	public AudioClip somBate;
	public AudioClip SomPerde;

	void Start () {
		//posicao do jogador
		escalaJogadorHorizontal = transform.localScale.x;
		//desativa ele batendo
		jogadorBate.SetActive (false);

		//iniciando lista
		listaBlocos = new List<GameObject>();
		//chamando
		CriaBarrisInicio ();

		pontuacao.transform.position = new Vector2 (Screen.width/2, Screen.height/2);
		pontuacao.text = "Toque para iniciar!";
		pontuacao.fontSize = 25;
	}

	void Update () {
		if(!acabou){
			//clique do mouse ou toque
			if(Input.GetButtonDown("Fire1")){
				if (!comecou) {
					comecou = true;
					barra.SendMessage ("Comecou");
				}

				GetComponent<AudioSource> ().PlayOneShot (somBate);

				// olha onde foi a posicao pra saber sefoi direita ou esquerda
				if (Input.mousePosition.x > Screen.width / 2) {
					//chama a funcao
					bateDireita ();
				} else {
					bateEsquerda ();
				}
				listaBlocos.RemoveAt (0);
				ReposicianaBlocos ();
				ConfereJogada ();
			}
		}
	}

	void bateEsquerda(){
		ladoPersonagem = false;
		//desativa 
		jogadorIdle.SetActive (false);
		//ativa
		jogadorBate.SetActive (true);
		//muda a posicao pra esquerda
		jogadorFelpudo.transform.position = new Vector2 (3.0f, -0.47f);
		jogadorFelpudo.transform.localScale = new Vector2 (escalaJogadorHorizontal, jogadorFelpudo.transform.localScale.y);
		//invoca o metodo
		Invoke ("VoltaAnimacao", 0.25f);
		listaBlocos [0].SendMessage ("AnimaEsquerda");
	}

	void bateDireita(){
		ladoPersonagem = true;
		//desativa 
		jogadorIdle.SetActive (false);
		//ativa
		jogadorBate.SetActive (true);
		//muda a posicao pra esquerda
		jogadorFelpudo.transform.position = new Vector2 (-3.0f, -0.47f);
		jogadorFelpudo.transform.localScale = new Vector2 (-escalaJogadorHorizontal, jogadorFelpudo.transform.localScale.y);
		//invoca o metodo
		Invoke ("VoltaAnimacao", 0.25f);
		listaBlocos [0].SendMessage ("AnimaDireita");
	}

	//volta 
	void VoltaAnimacao(){
		jogadorIdle.SetActive (true);
		jogadorBate.SetActive (false);
	}

	//criar barril
	GameObject CriaNovoBarril(Vector2 posicao){
		GameObject novoBarril;

		if (Random.value > 0.5f || listaBlocos.Count < 2) {
			novoBarril = Instantiate (barril);
		} else {
			if (Random.value > 0.5f) {
				novoBarril = Instantiate (barrilDireita);
			} else {
				novoBarril = Instantiate (barrilEsquerda);
			}
		}

		novoBarril.transform.position = posicao;

		return novoBarril;
	}

	void CriaBarrisInicio(){
		for (int i = 0; i <= 7; i++) {
			//criando barril
			GameObject objetoBarril = CriaNovoBarril (new Vector2(0, -1.98f+(i*1.18f)));
			listaBlocos.Add (objetoBarril);
		}
	}

	void ReposicianaBlocos(){
		//coloca o objeto em ultima posicao
		GameObject objBarril = CriaNovoBarril(new Vector2(0, -1.98f+(8*1.18f)));
		listaBlocos.Add (objBarril);
		for (int i = 0; i <= 7; i++) {
			listaBlocos [i].transform.position = new Vector2 (listaBlocos[i].transform.position.x, listaBlocos[i].transform.position.y-1.18f );
		}
	}

	void ConfereJogada(){
		if (listaBlocos [0].gameObject.CompareTag ("Inimigo")) {
			if ((listaBlocos [0].name == "inimigoEsq(Clone)" && !ladoPersonagem) || (listaBlocos [0].name == "inimigoDir(Clone)" && ladoPersonagem)) {
				
				FimDeJogo();
			} else {
				MarcaPonto ();
			}
		} else {
			MarcaPonto ();
		}
	}

	void MarcaPonto(){
		score++;
		pontuacao.transform.position = new Vector2 (Screen.width-50, Screen.height/2);
		pontuacao.text = score.ToString();
		pontuacao.fontSize = 36;
		pontuacao.color = new Color (0.96f, 1.0f, 0.35f);
		barra.SendMessage ("AumentaBarra");
	}

	void FimDeJogo(){
		acabou = true;
		jogadorBate.GetComponent<SpriteRenderer> ().color = new Color (1.0f, 0.35f, 0.35f);
		jogadorIdle.GetComponent<SpriteRenderer> ().color = new Color (1.0f, 0.35f, 0.35f);

		jogadorFelpudo.GetComponent<Rigidbody2D> ().isKinematic = false;
	
		if (ladoPersonagem) {
			jogadorFelpudo.GetComponent<Rigidbody2D> ().AddTorque (100.0f);
			jogadorFelpudo.GetComponent<Rigidbody2D> ().velocity = new Vector2 (10.0f, 1.0f);
		} else {
			jogadorFelpudo.GetComponent<Rigidbody2D> ().AddTorque (-100.0f);
			jogadorFelpudo.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-10.0f, 3.0f);
		}
		GetComponent<AudioSource> ().PlayOneShot (SomPerde);
		Invoke ("RecarregaCena",2);
	}

	void RecarregaCena(){
		//recarrega a cena
		Application.LoadLevel ("Cena_1");
	}
}
