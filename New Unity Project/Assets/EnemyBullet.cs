using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour {
	
	Vector3 position;
	int speed = 3;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		position = transform.position;
		transform.Translate( new Vector3(0,-1 * speed * Time.smoothDeltaTime, 0));
	}
	
	void OnCollisionEnter(Collision col)
	{
		GameObject collidedWith = col.gameObject;
		Debug.Log("enemyBullet : " + collidedWith.tag);
		
		if (collidedWith.tag == "Block" || collidedWith.tag == "Player" || collidedWith.tag.Equals("Bottom"))
		{
			Destroy(this.gameObject);
			
		}

	}
}
