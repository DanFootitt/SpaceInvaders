using UnityEngine;
using System.Collections;

public class enemyShip : MonoBehaviour {
	
	Player player;
	bool isAlive = true;
	
	Texture2D deathText_1;
	Texture2D deathText_2;
	float changeInterval = 0.2f;
	float changeTime;
	float dieTime;
	
	// Use this for initialization
	void Start () {
		deathText_1 = Resources.Load("enemyshipdeath_1") as Texture2D;
		deathText_2 = Resources.Load("enemyshipdeath_2") as Texture2D;
	}
	
	// Update is called once per frame
	void Update () {
		GameObject p = GameObject.FindGameObjectWithTag("Player");
		player = p.GetComponent<Player>();
		
		if (isAlive)
		{
			if (transform.position.x < 12)
			{
				transform.Translate(new Vector3(-3 * Time.deltaTime,0,0));
			}
			else
			{
				GameObject.Destroy(this.gameObject);
			}
		}
		
		else{
			
			if (Time.time > changeTime)
			{
				if (renderer.material.mainTexture == deathText_1) renderer.material.mainTexture = deathText_2;
				else renderer.material.mainTexture = deathText_1;
				changeTime = Time.time + changeInterval;
			}
			
			if (Time.time > dieTime)
			{
				Destroy(this.gameObject);
			}
			
		}
	}
	
	public void OnCollisionEnter(Collision col)
	{
		GameObject collidedWith = col.gameObject;
		
		if (collidedWith.tag == "PlayerBullet")
		{
			player.score += 200;
			isAlive = false;
			renderer.material.mainTexture = deathText_1;
			changeTime = Time.time + changeInterval;
			dieTime = Time.time + 1.0f;
		}
	}
	
}
