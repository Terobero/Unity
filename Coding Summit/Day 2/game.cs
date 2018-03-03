using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class game : MonoBehaviour {

	int money = 0;

	public Text moneytext;



	Animator anim;
	int moveHash = Animator.StringToHash("mario_anim");
	int idleHash = Animator.StringToHash("idle");
	float x;
	float y;
	Rigidbody2D rb2d;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {

		moneytext.text = "Money: " + money;

		x = 0;
		y = 0;
		GetComponent <SpriteRenderer> ().flipX = false;
		if(Input.GetKey (KeyCode.RightArrow)) {
			anim.Play (moveHash);
			x = 4f;
		}
		if(Input.GetKey (KeyCode.UpArrow)) {
			y = 4f;
		}

		if(Input.GetKey (KeyCode.LeftArrow)) {
			anim.Play (moveHash);
			x = -4f;
			GetComponent <SpriteRenderer> ().flipX = true;
		}
		if(Input.GetKeyUp (KeyCode.RightArrow) || Input.GetKeyUp (KeyCode.LeftArrow)) {
			anim.Play (idleHash);
		}
		rb2d.velocity = new Vector2 (x, y);
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "money"){
			money++;
			Destroy (other.gameObject);
		}
	}
}
