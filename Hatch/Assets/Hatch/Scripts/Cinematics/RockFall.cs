using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockFall : MonoBehaviour {
    public float Speed;
    public float Rate;
    public List<Rigidbody2D> Rocks;

    // Use this for initialization
    void Start () {
        Rocks = new List<Rigidbody2D>();
		foreach(Rigidbody2D rock in gameObject.GetComponentsInChildren<Rigidbody2D>()) {
			Rocks.Add(rock);
		}
    }

    public void RockSceneStart(float duration = 20f)
    {
        this.StartCoroutine(RockStart(duration));
    }

    public IEnumerator RockStart(float duration = 20f)
    {
        float elapsed = 0.0f;
        float rateElapsed = 0.0f;
        int counter = 0;

        while (elapsed < duration)
        {
            Debug.Log(rateElapsed);
            if (Rate < rateElapsed && Rocks.Count > 0) {
                var rock = Rocks[counter];
				if (rock != null) {
					rock.constraints = RigidbodyConstraints2D.None;
				}
                rateElapsed = 0f;
                counter++;
            }

            rateElapsed += Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }
        yield break;
    }
    public void RockSceneEnd(float duration = 0.8f)
    {
        Rocks.ForEach(x => GameObject.Destroy(gameObject));
        // gameObject.SetActive(false);
    }
}
