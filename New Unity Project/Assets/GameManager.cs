using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	public Transform blockPref;
	float level = 0;
	Player player;
	public Texture ship;
	
	public int playerStartLives;
	
	//UFO
	public Transform ufoPref;
	public float ufoSpawn;
	Object ufo;
	int noOfUFO;
	
	//Enemy Stuff
	public Transform enemyPref_2;
	public Transform enemyPref_1;
	public Transform enemyPref;
	GameObject[] enemies;
	public GameObject[] enemyBullets;
	public int maxEnemyBullets = 0;
	float enemyShotRate = 10.0f;
	public float nextShot = 0.0f;
	float startMoveRate = 1.0f;
	float startY = 5.25f;
	
	//Movement
	public AudioClip move_1;
	public AudioClip move_2;
	public AudioClip move_3;
	public AudioClip move_4;
	int move = 0;
	int noOfEnemies;
	
	public float test;
	
	//GUI
	public GUISkin white;
	public GUISkin green;
	
	// Use this for initialization
	void Start () {
		buildBlocks();
		move_1 = Resources.Load("move1") as AudioClip;
		move_2 = Resources.Load("move2") as AudioClip;
		move_3 = Resources.Load("move3") as AudioClip;
		move_4 = Resources.Load("move4") as AudioClip;
		ship = Resources.Load("ship") as Texture;
		white = Resources.Load("white") as GUISkin;
	}
	
	// Update is called once per frame
	void Update () {
		test = Time.time;
		enemies = GameObject.FindGameObjectsWithTag("Enemy");
		enemyBullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		
		if (player.isAlive)
		{
			//Level Up
			if (enemies.Length <= 0) {
				level++;
				startY = startY - 0.25f;
				Mathf.Clamp(startY,1.5f,5.25f);
				spawnEnemies();
				setEnemySpeed(level);
				maxEnemyBullets = Mathf.Clamp(1 + ((int)level/2),1,5);
				enemyShotRate = Mathf.Clamp(enemyShotRate - 0.5f,1,10);
				nextShot = Time.time + enemyShotRate;
				move = 0;
				noOfEnemies = enemies.Length;
				ufoSpawn = Time.time + Random.Range(1, 50);
				noOfUFO = 0;
				
				if (playerStartLives == player.getLives())
				{
					player.lives = player.lives + 1;
					if (player.lives >= 5) player.lives = 5;
				}
				
				playerStartLives = player.getLives();
			}
			
			if (enemies.Length < noOfEnemies)
			{
				//Create function to change enemy speed
				for (int i = 0; i < enemies.Length; i++)
				{
					Enemy e = enemies[i].GetComponent<Enemy>();
					e.moveRate -= (float)(noOfEnemies - enemies.Length)/100;
				}
				
				noOfEnemies = enemies.Length;		
			}
			
			if (!isEnemyInBounds(enemies)) reverseEnemies(enemies);
			
			
			if (Time.time >= nextShot && enemyBullets.Length < maxEnemyBullets)
			{
				int r = Random.Range(1,maxEnemyBullets);
				for (int i = 0; i < r; i++)
				{
					chooseEnemyToShoot(Random.Range(0,enemies.Length));
				}
				
				nextShot = Time.time + enemyShotRate;
			}
			
	
			
			if (Time.time >= ufoSpawn && noOfUFO < 1)
			{
				ufo = Instantiate(ufoPref, new Vector3(-10,6,-1), new Quaternion(0,180,0,0));
				noOfUFO++;
			}
			
			
			if (didEnemyMove())
			{
				playMoveSound();
			}
		}
		
		if (player.getLives() <= 0)
		{
			Application.LoadLevel("GameOver");
		}
	}
	
	
	void buildBlocks()
	{
		for (int i = 0; i < 4; i++)
		{
			float blockX = -6.5f + (i * 5.0f);
			float blockY = -1.75f;
			
			for(int y = 0; y < 6; y++)
			{
				
				if (y != 0) 	Instantiate(blockPref, new Vector3(blockX		 ,(blockY - y * 0.25f), -1), new Quaternion (0,0,0,0));
								Instantiate(blockPref, new Vector3(blockX - 0.25f,(blockY - y * 0.25f), -1), new Quaternion (0,0,0,0));
				if (y < 4)		Instantiate(blockPref, new Vector3(blockX - 0.50f,(blockY - y * 0.25f), -1), new Quaternion (0,0,0,0));
				if (y < 4)		Instantiate(blockPref, new Vector3(blockX - 0.75f,(blockY - y * 0.25f), -1), new Quaternion (0,0,0,0));
				if (y < 4)		Instantiate(blockPref, new Vector3(blockX - 1.00f,(blockY - y * 0.25f), -1), new Quaternion (0,0,0,0));
				if (y < 4)		Instantiate(blockPref, new Vector3(blockX - 1.25f,(blockY - y * 0.25f), -1), new Quaternion (0,0,0,0));
								Instantiate(blockPref, new Vector3(blockX - 1.50f,(blockY - y * 0.25f), -1), new Quaternion (0,0,0,0));
				if (y != 0)		Instantiate(blockPref, new Vector3(blockX - 1.75f,(blockY - y * 0.25f), -1), new Quaternion (0,0,0,0));
				
			}
		}
		
	}
	
	void spawnEnemies()
	{
		
		for (int i = 0; i < 5 ; i++)
		{
			Transform tmpPref;
			
			if (i == 0) 				tmpPref = enemyPref_1;
			else if (i > 0 && i <= 2) 	tmpPref = enemyPref;
			else 						tmpPref = enemyPref_2;
			
			Instantiate(tmpPref, new Vector3(-5.0f,startY - i,-1), new Quaternion (0,180,0,0));
			Instantiate(tmpPref, new Vector3(-3.5f,startY - i,-1), new Quaternion (0,180,0,0));
			Instantiate(tmpPref, new Vector3(-2.0f,startY - i,-1), new Quaternion (0,180,0,0));
			Instantiate(tmpPref, new Vector3(-0.5f,startY - i,-1), new Quaternion (0,180,0,0));
			Instantiate(tmpPref, new Vector3(1.0f ,startY - i,-1), new Quaternion (0,180,0,0));
			Instantiate(tmpPref, new Vector3(2.5f ,startY - i,-1), new Quaternion (0,180,0,0));
			Instantiate(tmpPref, new Vector3(4.0f ,startY - i,-1), new Quaternion (0,180,0,0));
			Instantiate(tmpPref, new Vector3(5.5f ,startY - i,-1), new Quaternion (0,180,0,0));
		}

	}
	
	void chooseEnemyToShoot(int index)
	{
		Enemy e = enemies[index].GetComponent<Enemy>();
		e.canFire = true;

	}
	
	bool isEnemyInBounds(GameObject[] enemies){
		
		
		for (int i = 0; i < enemies.Length; i++)
		{
			Enemy e = enemies[i].GetComponent<Enemy>();
			
			if (e.position.x >= 10 && e.isMovingLeft)
			{
				return false;
			}
			
			if (e.position.x <= -9.5f && !e.isMovingLeft)
			{
				return false;
			}

		}
		return true;
		
	}
	
	void reverseEnemies(GameObject[] enemies)
	{
		for (int i = 0; i < enemies.Length; i++)
		{
			Enemy e = enemies[i].GetComponent<Enemy>();
			
			if (Time.time > e.nextMove)
			{
				e.reverse();
				playMoveSound();
				e.moveRate -= 0.025f;
			}
			
		}
	} 
	
	void setEnemySpeed(float level)
	{
		enemies = GameObject.FindGameObjectsWithTag("Enemy");
		
		for (int i = 0; i < enemies.Length; i++)
		{
			Enemy e = enemies[i].GetComponent<Enemy>();
			e.moveRate = startMoveRate - (level/10) / 2;
			
			
		}
	}
	
	bool didEnemyMove()
	{
		enemies = GameObject.FindGameObjectsWithTag("Enemy");
		
		for (int i = 0; i < enemies.Length; i++)
		{
			Enemy e = enemies[i].GetComponent<Enemy>();
			if (Time.time > e.nextMove)
			{
				return true;
			}

		}
		return false;
	}
	
	void playMoveSound()
	{
		move++;
		if (move == 1) audio.PlayOneShot(move_1);
		if (move == 2) audio.PlayOneShot(move_2);
		if (move == 3) audio.PlayOneShot(move_3);
		if (move >= 4)
		{
			audio.PlayOneShot(move_4);
			move = 0;
		}
	}
	
	void OnGUI()
	{
		Player p = player.GetComponent<Player>();
		GUI.skin = white;
		GUI.Label(new Rect(100,10,300,100), "SCORE : ");
		
		GUI.color = new Color(0,251,0,251);
		GUI.Label(new Rect(250,10,100,100),p.getScore().ToString());
		
		GUI.color  = Color.white;
		GUI.Label(new Rect(500,10,300,100), "LIVES : ");
		
		for (int i = 0; i < p.getLives(); i++)
		{
			int x = 650 + 50*i;
			GUI.DrawTexture(new Rect(x,10,40,25),ship);
		}
		
		
	}
}
