using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class DemonDog : MonoBehaviour
{
    public float DistanceFromPlayer;
    public float Agro1;
    public float Agro2;
    public float Agro3;
    public float Agro4;
    public string State;
    public string PrevState;
    public float AnimCounter;
    public float AnimDuration = 0.5f;
    public GameObject Player;
    public float digChance = 0.5f;

    public GameObject PitTrigger;

    public DemonDogDialogue dialogue;

    public SkeletonAnimation skeletonAnimation;
    // Spine.AnimationState and Spine.Skeleton are not Unity-serialized objects. You will not see them as fields in the inspector.
    public Spine.AnimationState spineAnimationState;
    public Spine.Skeleton skeleton;
    public bool agro4Activated = false;


    // Use this for initialization
    void Start()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        dialogue = GetComponent<DemonDogDialogue>();
        spineAnimationState = skeletonAnimation.AnimationState;
        skeleton = skeletonAnimation.Skeleton;
    }

    // Update is called once per frame
    void Update()
    {
        DistanceFromPlayer = Vector3.Distance(Player.transform.position, transform.position);
        if (DistanceFromPlayer < Agro4)
        {
            State = "agro4";
        }
        else if (DistanceFromPlayer < Agro3)
        {
            State = "agro3";
        }
        else if (DistanceFromPlayer < Agro2)
        {
            State = "agro2";
        }
        else
        {
            State = "agro1";
        }

        if (State != PrevState)
        {
            if (State == "agro1")
            {
                this.StartCoroutine(ExitAgro2());
                digChance = 0.5f;
            }
            else if (State == "agro2")
            {
                this.StartCoroutine(EnterAgro2());
                digChance = 0.5f;
            }
            else if (State == "agro3")
            {
                digChance = 1;
            }
            else if (State == "agro4")
            {
                digChance = 1;
                agro4Activated = true;
                dialogue.StartDialogue(Player);
                PitTrigger.GetComponent<Collider2D>().enabled = true;
            }
            PrevState = State;
        }

        if (State == "agro3") 
        {

            if (AnimCounter > AnimDuration)
            {
                AnimCounter = 0;

                var animationRange = Random.Range(0f, digChance);

                if (animationRange > 0.5f)
                {
                    spineAnimationState.SetAnimation(1, "dig", false);
                }
            }
        }
        AnimCounter += Time.deltaTime;
    }
    public IEnumerator EnterAgro2(float duration = 1f)
    {
        float elapsed = 0.0f;
        float rateElapsed = 0.0f;

        var skeletonAnimation = gameObject.transform.gameObject.GetComponent<SkeletonAnimation>();
        skeletonAnimation.AnimationState.SetAnimation(0, "growlEnter", false);

        while (elapsed < duration)
        {
            rateElapsed += Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }
        skeletonAnimation.AnimationState.SetAnimation(0, "growl", true);

        yield break;
    }
    public IEnumerator ExitAgro2(float duration = 1f)
    {
        float elapsed = 0.0f;
        float rateElapsed = 0.0f;

        var skeletonAnimation = gameObject.transform.gameObject.GetComponent<SkeletonAnimation>();
        skeletonAnimation.AnimationState.SetAnimation(0, "growlExit", false);

        while (elapsed < duration)
        {
            rateElapsed += Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }
        skeletonAnimation.AnimationState.SetAnimation(0, "idle", true);

        yield break;
    }
}
