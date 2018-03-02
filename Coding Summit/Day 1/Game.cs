using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {

	public static int lives;

	public GameObject enemy1;
	public GameObject enemy2;
	public GameObject boss;
	public GameObject player;
	public GameObject bullet;

	public RawImage h1;
	public RawImage h2;
	public RawImage h3;

	public Texture2D h_empty;

	private float x, y;
	private bool difficultMode = false, bossMode = false;

	public static int kill_count;

	public static int enemies_on_screen;
	public static int max_enemies;
	private int loop;
	Vector3 enemy_spawn;
	Quaternion rotation = new Quaternion(0,0,0,0);

	public static float take_damage_start = 0;
	public static bool can_take_damage = true;

	public static float bullet_start = 0;
	public static bool can_spawn_bullet = true;



	void Start () {
		lives = 3;
		kill_count = 0;
		enemies_on_screen = 0;
		max_enemies = 0;
		loop = 0;
		difficultMode = false;
		bossMode = false;
	}
	
	void Update () {
		x = 0;
		y = 0;

		loop++;
		if (loop % 50 == 0 && max_enemies <= 5)
			max_enemies += 1;
		
		if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.W))
			y = 4f;

		if (Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.S))
			y = -4f;

		if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D))
			x = 4f;

		if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A))
			x = -4f;


		if(Input.GetKey (KeyCode.Space)){
			if (can_spawn_bullet) {
				Rigidbody2D bullet_rb = Instantiate (bullet.GetComponent <Rigidbody2D> (), player.transform.position, rotation) as Rigidbody2D;
				bullet_rb.velocity = transform.right * 10f;
				Physics2D.IgnoreCollision (bullet_rb.GetComponent <Collider2D> (), player.GetComponent <Collider2D> ());
				bullet_start = Time.time;
				can_spawn_bullet = false;
			}

			if (Time.time - bullet_start >= 0.2)
				can_spawn_bullet = true;
		}

		player.GetComponent <Rigidbody2D> ().velocity = new Vector2 (x,y);

		if (!difficultMode && Time.time >= 20)
			difficultMode = true;

		if (enemies_on_screen <= max_enemies) {
			
			enemy_spawn = new Vector3 (6, Random.Range (-4,4));

			if (kill_count >= 10) {
				if (!bossMode && enemies_on_screen == 0) {
					Boss.isBossMode = true;
					bossMode = true;
				}
			}


			else if (difficultMode) {
				int random = Random.Range (0, 2);
				if(random == 0)
					Instantiate (enemy1, enemy_spawn, rotation);
				else
					Instantiate (enemy2, enemy_spawn, rotation);
				
			} else {
				Instantiate (enemy1, enemy_spawn, rotation);
			}
		}

		if (Time.time - take_damage_start >= 5) {
			can_take_damage = true;
			player.GetComponent <SpriteRenderer> ().color = Color.white;
		} 
		if (!can_take_damage){
			player.GetComponent <SpriteRenderer> ().color = Color.grey;
		}


		UpdateLives ();
	}

	void UpdateLives(){
		switch(lives){
		case -1:
			SceneManager.LoadScene ("Scenes/Lose");
			break;
		case 0:
			h1.GetComponent <RawImage>().texture = h_empty;
			break;
		case 1:
			h2.GetComponent <RawImage>().texture = h_empty;
			break;
		case 2:
			h3.GetComponent <RawImage>().texture = h_empty;
			break;
			
		}
	}

	public static void TakeDamage(){
		if (can_take_damage) {
			take_damage_start = Time.time;
			lives--;
			can_take_damage = false;
		}
	}
		
}
