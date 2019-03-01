using System;
using System.Linq;
using Assets.Hatch.Scripts;
using RotaryHeart.Lib.SerializableDictionary;
using Spine;
using Spine.Unity;
using UnityEngine;
using Event = Spine.Event;
using Random = UnityEngine.Random;

public class SkeletonEvents : MonoBehaviour
{

    public SkeletonAnimation skeletonAnimation;

    [Space]
    public AudioSource audioSource;

    public float basePitch = 1f;

    public float randomPitchOffset = 0.1f;

    [Space] public bool logDebugMessage = false;

    [SerializeField] private SpineAudioDictionary spineAudioDictionary;

    [Serializable]
    public class SpineAudioDictionary : SerializableDictionaryBase<SpineEventKey, AudioClip> { }

    void OnValidate()
    {
        if (skeletonAnimation == null)
        {
            GetComponent<SkeletonAnimation>();
        }

        if (audioSource == null)
        {
            GetComponent<AudioSource>();
        }
    }

	// Use this for initialization
	void Start () {
	    if (audioSource == null)
	    {
	        return;
	    }

	    if (skeletonAnimation == null)
	    {
	        return;
	    }

	    skeletonAnimation.Initialize(false);
	    if (!skeletonAnimation.valid)
	    {
	        return;
	    }

	    skeletonAnimation.AnimationState.Event += HandleAnimationEvent;
	}

    void HandleAnimationEvent(TrackEntry trackEntry, Event e)
    {
        if (logDebugMessage)
        {
            Debug.Log("Event fired! " + e.Data.Name);
        }

        SpineEventKey spineKey;
        if (TryGetSpineKey(e.Data.Name, out spineKey))
        {
            var clip = spineAudioDictionary[spineKey];
            Play(clip);
        }
    }

    public void Play(AudioClip clip)
    {
        audioSource.pitch = basePitch + Random.Range(-randomPitchOffset, randomPitchOffset);
        audioSource.PlayOneShot(clip);
    }

    bool TryGetSpineKey(string eventName, out SpineEventKey key)
    {
        var spineEventKey = spineAudioDictionary.Keys.FirstOrDefault(x => string.Equals(x.spineEvent, eventName));
        if (spineEventKey != null && spineAudioDictionary.ContainsKey(spineEventKey))
        {
            key = spineEventKey;
            return true;
        }

        key = null;

        return false;
    }
}
