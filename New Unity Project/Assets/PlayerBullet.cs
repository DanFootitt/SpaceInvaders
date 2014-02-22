using UnityEngine;
using System.Collections;

public class PlayerBullet : MonoBehaviour {
	
	Vector3 position;
	int speed = 7;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		position = transform.position;
		transform.Translate( new Vector3(0,1 * speed * Time.smoothDeltaTime, 0));
		
		if (position.y >= 8)
		{
			Destroy (this.gameObject);
		}
	}
	
	void OnCollisionEnter(Collision col)
	{
		GameObject collidedWith = col.gameObject;
		
		if (collidedWith.tag == "Block" || collidedWith.tag == "UFO" || collidedWith.tag == "Enemy")
		{
			Destroy(this.gameObject);
			
		}


	}
}
