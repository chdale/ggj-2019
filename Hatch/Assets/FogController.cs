using Assets.Hatch.Scripts.Enumerations;
using System.Collections;
using Assets.Hatch.Scripts.Utilities;
using UnityEngine;

public class FogController : MonoBehaviour
{
    private GameController _gameController;

    [SerializeField]
    private SpriteRenderer _fogWallSmoke;
    [SerializeField]
    private AudioSource _fogFade;
    [SerializeField]
    private ParticleSystem _fogParticles;

    // Fog Faces
    [Space, Space]
    [SerializeField]
    private SpriteRenderer _smallFaces;
    [SerializeField]
    private SpriteRenderer _bigFaceClosed;
    [SerializeField]
    private SpriteRenderer _bigFaceOpen;

    [Space, Space]
    [SerializeField]
    private float _startSmallFaces = 18f;
    [SerializeField]
    private float _startBigFaceClosed = 10f;
    [SerializeField]
    private float _startBigFaceOpen = 7f;

    [SerializeField]
    private float _minFogVolume = 0f;
    [SerializeField]
    private float _maxFogVolume = 0.8f;

    // Non editor properties
    private GameObject _player;
    private AudioSource _audioSource;

    private Vector3 _shake = new Vector3(0.1f, 0.1f);
    private float _shakeTime = 5f;


    void Start ()
	{
        _gameController = GameObject.Find("GameController").GetComponent<GameController>();
	    _player = GameObject.Find("Player_Wireframe");
	    _audioSource = GetComponent<AudioSource>();

	    if (_fogParticles == null)
	    {
	        _fogParticles = GetComponentInChildren<ParticleSystem>();
	    }

        if (_gameController.currentGameState >= GameState.Photo1)
        {
            this.gameObject.SetActive(false);
        }
	}
	
	void Update ()
    {
        FadeFaces(_smallFaces, _startSmallFaces);
        FadeFaces(_bigFaceClosed, _startBigFaceClosed);
        FadeFaces(_bigFaceOpen, _startBigFaceOpen);

        HandleFogAudio();
    }

    private void HandleFogAudio()
    {
        // Track player distance from fog wall
        var distance = Vector2.Distance(_player.transform.position, transform.position);

        _audioSource.volume = AudioUtility.GetDynamicVolumeUsingDistance(
            distance: distance,
            maxDistance: _startSmallFaces,
            minDistance: _startBigFaceOpen,
            minVolume: _minFogVolume,
            maxVolume: _maxFogVolume
        );

        if (!_audioSource.isPlaying)
        {
            _audioSource.Play();
        }
    }

    IEnumerator FadeSpriteRenderer(SpriteRenderer sprite, bool fadeIn, float fadeModifier = 1.0f)
    {
        var color = sprite.color;

        if (fadeIn)
        {
            iTween.ShakePosition(sprite.gameObject, _shake, _shakeTime);
            while (color.a <= 1)
            {
                color.a += Time.deltaTime / fadeModifier;
                sprite.color = color;
                yield return null;
            }

            sprite.color = color;
        }
        else
        {
            while (color.a >= 0)
            {
                color.a -= Time.deltaTime / fadeModifier;
                sprite.color = color;
                yield return null;
            }

            if (sprite == _fogWallSmoke)
            {
                this.gameObject.SetActive(false);
            }
            sprite.color = color;
        }
        
    }

    private void FadeFaces(SpriteRenderer sprite, float renderDistance)
    {
        var distance = Vector2.Distance(_player.transform.position, transform.position);
        StartCoroutine(distance < renderDistance
            ? FadeSpriteRenderer(sprite, true)
            : FadeSpriteRenderer(sprite, false));
    }

    public void ClearFogWall()
    {
        _fogFade.Play();

        FadeParticles();

        // Start fade coroutine with increased fade time
        StartCoroutine(FadeSpriteRenderer(_fogWallSmoke, false, 3f));
    }

    private void FadeParticles()
    {
        // Update alpha value of particle colors
        var colorOverLifetime = _fogParticles.colorOverLifetime;
        var particleColor = colorOverLifetime.color.color;
        particleColor.a = 0f;
        colorOverLifetime.color = new ParticleSystem.MinMaxGradient(particleColor);

        // Increase particle speed
        var vel = _fogParticles.velocityOverLifetime;
        vel.zMultiplier = 10f;

        _fogParticles.Stop();
    }
}
