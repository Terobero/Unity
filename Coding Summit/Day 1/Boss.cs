using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour {

	int health = 50;

	public GameObject player;
	public GameObject enemy_bullet;
	public GameObject right_wall;


	private float bullet_start = 0;
	private bool can_spawn_bullet = true;

	private Quaternion rotation = new Quaternion(0,0,0,0);

	public static bool isBossMode = false;

	public Text boss_text;



	void Start () {
		Physics2D.IgnoreCollision (GetComponent <Collider2D>(), right_wall.GetComponent <Collider2D>());
		boss_text.text = "";
		isBossMode = false;
	}



	void Update () {
		if (isBossMode) {
			boss_text.text = "BOSS HEALTH: " + health;

			if (transform.position.x > 5)
				transform.Translate (-0.1f, 0, 0);

			if (can_spawn_bullet) {
				Rigidbody2D bullet_rb = Instantiate (enemy_bullet.GetComponent <Rigidbody2D> (), transform.Find ("cannon").transform.position, rotation) as Rigidbody2D;
				bullet_rb.velocity = -transform.right * 10f;
				Physics2D.IgnoreCollision (bullet_rb.GetComponent <Collider2D> (), GetComponent <Collider2D> ());

				Rigidbody2D bullet_rb2 = Instantiate (enemy_bullet.GetComponent <Rigidbody2D> (), transform.Find ("cannon 2").transform.position, rotation) as Rigidbody2D;
				bullet_rb2.velocity = -transform.right * 10f;
				Physics2D.IgnoreCollision (bullet_rb2.GetComponent <Collider2D> (), GetComponent <Collider2D> ());

				Rigidbody2D bullet_rb3 = Instantiate (enemy_bullet.GetComponent <Rigidbody2D> (), transform.Find ("cannon 3").transform.position, rotation) as Rigidbody2D;
				bullet_rb3.velocity = -transform.right * 10f;
				Physics2D.IgnoreCollision (bullet_rb3.GetComponent <Collider2D> (), GetComponent <Collider2D> ());

				Rigidbody2D bullet_rb4 = Instantiate (enemy_bullet.GetComponent <Rigidbody2D> (), transform.Find ("cannon 4").transform.position, rotation) as Rigidbody2D;
				bullet_rb4.velocity = -transform.right * 10f;
				Physics2D.IgnoreCollision (bullet_rb4.GetComponent <Collider2D> (), GetComponent <Collider2D> ());


				bullet_start = Time.time;
				can_spawn_bullet = false;
			}

			if (Time.time - bullet_start >= 1)
				can_spawn_bullet = true;
		}


	}

	void OnCollisionEnter2D(Collision2D col){
		switch(col.gameObject.tag){
		case "bullet":
			health--;
			if (health == 0) {
				SceneManager.LoadScene ("Scenes/Win");
				Destroy (gameObject);
			}

			break;

		case "player":
			Game.TakeDamage ();
			break;
		}
	}
}
