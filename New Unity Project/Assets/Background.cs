using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
				
		float offset = Time.time * 0.5f;
        renderer.material.SetTextureOffset ("_MainTex", new Vector2(0,-offset));
	}
}
