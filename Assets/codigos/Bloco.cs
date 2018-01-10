using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloco : MonoBehaviour {

	void AnimaDireita () {
		//velocidade
		GetComponent<Rigidbody2D>().velocity = new Vector2 (-10, 2);
		//desliga kinematic
		GetComponent<Rigidbody2D> ().isKinematic = false;
		//gira
		GetComponent<Rigidbody2D> ().AddTorque (100.0f);
		//invoca funcao pra apagar
		Invoke("ApagaBloco", 2.0f);
	}

	void AnimaEsquerda () {
		//velocidade
		GetComponent<Rigidbody2D>().velocity = new Vector2 (10, 2);
		//desliga kinematic
		GetComponent<Rigidbody2D> ().isKinematic = false;
		//gira
		GetComponent<Rigidbody2D> ().AddTorque (-100.0f);
		//invoca funcao pra apagar
		Invoke("ApagaBloco", 2.0f);
	}

	void ApagaBloco(){
		//destroy o objeto
		Destroy (this.gameObject);
	}

}