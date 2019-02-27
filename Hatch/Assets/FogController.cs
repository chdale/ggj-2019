using System;
using Assets.Hatch.Scripts.Enumerations;
using System.Collections;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;

public class FogController : MonoBehaviour
{
    private GameController gameController;
    [SerializeField]
    private ParticleSystem fogParticles;
    [SerializeField]
    private SpriteRenderer fogWallSmoke;
    [SerializeField]
    private SpriteRenderer smallFaces;
    [SerializeField]
    private SpriteRenderer bigFaceClosed;
    [SerializeField]
    private SpriteRenderer bigFaceOpen;

    private GameObject player;

    private Vector3 shake = new Vector3(0.1f, 0.1f);
    private float shakeTime = 5f;
    private float fadeTime = 1f;

    [SerializeField, Space]
    private FogAudioDictionary fogAudioDictionary;

    [Serializable]
    public class FogAudioDictionary : SerializableDictionaryBase<int, AudioClip> {}

    private AudioSource audioSource;

    public CameraController cameraController;

    void Start ()
	{
	    if (fogParticles == null)
	    {
	        fogParticles = this.GetComponentInChildren<ParticleSystem>();
        }
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
	    player = GameObject.Find("Player_Wireframe");

        if (gameController.currentGameState >= GameState.Photo1)
        {
            this.gameObject.SetActive(false);
        }

	    audioSource = GetComponent<AudioSource>();
	}
	
	void Update ()
	{
	    FadeFaces(smallFaces, 18f);
	    FadeFaces(bigFaceClosed, 12f);
	    FadeFaces(bigFaceOpen, 8f);

	    var distance = Vector2.Distance(cameraController.transform.position, transform.position);
	    if (distance < 20f)
	    {
	        audioSource.clip = fogAudioDictionary[0];
	        if (!audioSource.isPlaying)
	        {
	            audioSource.Play();
	        }
	    }

	    if (distance < 17f)
	    {
            audioSource.Stop();
	        audioSource.clip = fogAudioDictionary[1];
	        if (!audioSource.isPlaying)
	        {
	            audioSource.Play();
	        }
        }
    }

    IEnumerator FadeSpriteRenderer(SpriteRenderer sprite, bool fadeIn)
    {
        var color = sprite.color;

        if (fadeIn)
        {
            iTween.ShakePosition(sprite.gameObject, shake, shakeTime);
            while (color.a < 1)
            {
                color.a += Time.deltaTime / fadeTime;
                sprite.color = color;
                yield return null;
            }

            sprite.color = color;
        }
        else
        {
            while (color.a > 0)
            {
                color.a -= Time.deltaTime / fadeTime;
                sprite.color = color;
                yield return null;
            }

            if (sprite == fogWallSmoke)
            {
                this.gameObject.SetActive(false);
            }
            sprite.color = color;
        }
        
    }

    private void FadeFaces(SpriteRenderer sprite, float renderDistance)
    {
        var distance = Vector2.Distance(player.transform.position, transform.position);
        StartCoroutine(distance < renderDistance
            ? FadeSpriteRenderer(sprite, true)
            : FadeSpriteRenderer(sprite, false));
    }

    public void ClearFogWall()
    {
        StartCoroutine(FadeSpriteRenderer(fogWallSmoke, false));
    }
}
