using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter(Collision col)
	{
		GameObject collidedWith = col.gameObject;
		Debug.Log("Block : " + collidedWith.tag);
		
		if (collidedWith.tag.Equals("PlayerBullet") || collidedWith.tag.Equals("EnemyBullet") || collidedWith.tag.Equals("Enemy"))
		{
			Destroy(this.gameObject);
			
		}
	}
}
