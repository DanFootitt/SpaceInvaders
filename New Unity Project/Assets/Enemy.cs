using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	
	GameObject collidedWith;
	public bool isMovingLeft = true;
	public Vector3 position;
	
	public float moveRate = 2.0f;
	public float nextMove = 0.0f;
	
	//Bullet Stuff
	public bool canFire = false;
	public Transform bulletPref;
	Object bullet;
	
	public Texture2D text_1;
	public Texture2D text_2;
	Texture2D death;
	float dieTime;
	Player player;
	
	public bool isAlive = true;
	
	// Use this for initialization
	void Start () {
		death = Resources.Load("alien_death") as Texture2D;
		
	}
	
	// Update is called once per frame
	void Update () {
		GameObject p = GameObject.FindGameObjectWithTag("Player");
		player = p.GetComponent<Player>();
		if (this.moveRate <= 0.1f) moveRate = 0.1f;
		
		position = transform.position;
		
		if (player.isAlive)
		{
			if (Time.time >= nextMove)
			{
				if (!isAlive) Destroy(this.gameObject);
				
				if (isMovingLeft){
	
					transform.Translate(new Vector3(-0.25f,0,0));
	
				}
				else{
	
					transform.Translate(new Vector3(0.25f,0,0));
				}
				
				if(renderer.material.mainTexture == text_1) 
				{
					renderer.material.mainTexture = text_2;
				}
				else
				{
					renderer.material.mainTexture = text_1;
				}
				
				nextMove = Time.time + moveRate;
				
				
			}
			
			if (canFire && bullet == null)
			{
				bullet = Instantiate(bulletPref,position, new Quaternion (0,180,0,0));
				canFire = false;
			}
		}
		
					
		
		
	}
	
	
	void OnCollisionEnter(Collision col)
	{
		collidedWith = col.gameObject;
		
		if (collidedWith.tag == "PlayerBullet" && this.isAlive)
		{
			renderer.material.mainTexture = death;
			isAlive = false;
			player.score += 10;
		}
		
		if (collidedWith.tag == "Bottom" || collidedWith.tag == "Player")
		{
			Application.LoadLevel("GameOver");
		}
		
	}
	
	
	public void reverse()
	{
		transform.Translate(new Vector3(0,-0.5f,0));
		nextMove = Time.time + moveRate;
		if (isMovingLeft) isMovingLeft = false;
		else isMovingLeft = true;
	}

}
