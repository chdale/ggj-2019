using System.Collections;
using System.Collections.Generic;
using Assets.Hatch.Scripts.Events.Scenes;
using UnityEngine;

namespace Assets.Hatch.Scripts.Events.Scenes
{
	public class RockTrigger : MonoBehaviour {

		public GameObject PitScene;
		public GameObject Player;
		public Collider2D collider;

		// Use this for initialization
		void Start () {
			collider = gameObject.GetComponent<Collider2D>();
		}
		
		// Update is called once per frame
		void Update () {
			if (collider.IsTouching(Player.GetComponent<Collider2D>())) {
				PitScene.GetComponent<Pit>().StartScene();
			}
		}
	}
}
