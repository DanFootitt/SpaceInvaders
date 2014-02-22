using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	// Player Stuff
	Vector3 position;
	int speed = 5;
	public int lives = 3;
	public int score = 0;
	public bool isAlive = true;
	
	//Texture
	Texture2D ship;
	Texture2D deathText_1;
	Texture2D deathText_2;
	float textTimer;
	float textInterval = 0.2f;
	float nextSwitch;
	
	// Collision
	GameObject collidedWith;
	
	// Bullet Stuff
	public Transform bulletPref;
	float fireRate = 0.2f;
	float nextShot = 0.0f;
	GameObject[] bullets;
	AudioClip shoot;
	
	
	
	
	// Use this for initialization
	void Start () {
		position = transform.position;
		shoot = Resources.Load("shoot") as AudioClip;
		deathText_1 = Resources.Load("ship_death") as Texture2D;
		deathText_2 = Resources.Load("ship_death_2") as Texture2D;
		ship = Resources.Load("ship") as Texture2D;
	}
	
	// Update is called once per frame
	void Update () {
		
		position = transform.position;
		bullets = GameObject.FindGameObjectsWithTag("PlayerBullet");
		
		float x = Input.GetAxis("Horizontal");	
		
		if(isAlive)
		{
			// left border
			if (x > 0 && position.x >= -9.5f){
				transform.Translate(new Vector3(x * speed * Time.deltaTime, 0 , 0));
			}
			
			// right border
			if (x < 0 && position.x <= 10){
				transform.Translate(new Vector3(x * speed * Time.deltaTime, 0 , 0));
			}
			
			
			if (Input.GetKeyDown(KeyCode.Space))
			{
				if (Time.time >= nextShot && bullets.Length < 1)
				{
					audio.PlayOneShot(shoot);
					Instantiate(bulletPref, new Vector3(position.x,position.y + 1,-1), new Quaternion (0,0,0,0));
					nextShot = Time.time + fireRate;
				}
			}
		}
		
		if (Time.time > textTimer && !isAlive)
		{
			transform.position = new Vector3(0,-4,-1);
			renderer.material.mainTexture = ship;
			isAlive = true;
		}
		
		if (!isAlive && Time.time > nextSwitch)
		{
			if (renderer.material.mainTexture == deathText_1) renderer.material.mainTexture = deathText_2;
			else renderer.material.mainTexture = deathText_1;
			nextSwitch = Time.time + textInterval;
		}
				
	}
	
	
	void OnCollisionEnter(Collision col)
	{
		collidedWith = col.gameObject;
		
		if (collidedWith.tag == "EnemyBullet")
		{
			lives--;
			isAlive = false;
			renderer.material.mainTexture = deathText_1;
			textTimer = Time.time + 2.0f;
			isAlive = false;
			nextSwitch = Time.time + textInterval;

		}
		
	}
	
	void Awake()
	{
		DontDestroyOnLoad(transform.gameObject);
	}
	
	public int getLives()
	{
		return this.lives;
	}
	
	public void setLives(int l)
	{
		this.lives = lives;
		
	}
	
	public int getScore()
	{
		return this.score;
	}

}
