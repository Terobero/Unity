using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	float wanted_x = 0;
	float wanted_y = 4;
	float x;
	float y;

	int health = 2;

	public GameObject player;
	public GameObject enemy_bullet;

	public bool hard_enemy = false;

	private float bullet_start = 0;
	private bool can_spawn_bullet = true;
	private Quaternion rotation = new Quaternion(0,0,0,0);



	void Start () {
		Game.enemies_on_screen += 1;
	}



	void Update () {
		x = 0;
		y = 0;

		if (transform.position.x > wanted_x) {
			x = -0.1f;
			wanted_x = -5;
		} else if(transform.position.x < wanted_x) {
			x = 0.1f;
			wanted_x = 5;
		}

		if (transform.position.y > wanted_y) {
			y = -0.1f;
			wanted_y = -4;
		} else if(transform.position.y < wanted_y) {
			y = 0.1f;
			wanted_y = 4;
		}

		transform.Translate (x ,y, 0);

		if (can_spawn_bullet) {
			Rigidbody2D bullet_rb = Instantiate (enemy_bullet.GetComponent <Rigidbody2D> (), transform.Find ("cannon").transform.position, rotation) as Rigidbody2D;
			bullet_rb.velocity = -transform.right * 10f;
			Physics2D.IgnoreCollision (bullet_rb.GetComponent <Collider2D> (), GetComponent <Collider2D> ());

			if(hard_enemy){
				Rigidbody2D bullet_rb2 = Instantiate (enemy_bullet.GetComponent <Rigidbody2D> (), transform.Find ("cannon 2").transform.position, rotation) as Rigidbody2D;
				bullet_rb2.velocity = -transform.right * 10f;
				Physics2D.IgnoreCollision (bullet_rb2.GetComponent <Collider2D> (), GetComponent <Collider2D> ());
			}

			bullet_start = Time.time;
			can_spawn_bullet = false;
		}

		if (Time.time - bullet_start >= 1)
			can_spawn_bullet = true;


	}

	void OnCollisionEnter2D(Collision2D col){
		switch(col.gameObject.tag){
		case "bullet":
			health--;
			if (health == 0) {
				Game.enemies_on_screen -= 1;
				Game.max_enemies -= 1;
				Game.kill_count += 1;
				Destroy (gameObject);
			}
			
			break;

		case "player":
			Game.TakeDamage ();
			break;
		}
	}
}
