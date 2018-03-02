using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {


	private float spawn_time = 0;

	void Start(){
		spawn_time = Time.time;
	}
		
	void Update () {
		if (Time.time - spawn_time > 1)
			Destroy (gameObject);
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "wall")
			Destroy (gameObject);
		

		if(col.gameObject.tag == "enemy")
			Destroy (gameObject);
		

		if(col.gameObject.tag == "player"){
			Game.TakeDamage ();
			Destroy (gameObject);
		}

		if (col.gameObject.tag == "enemy bullet") {
			Destroy (col.gameObject);
			Destroy (gameObject);
		}
	}
}
