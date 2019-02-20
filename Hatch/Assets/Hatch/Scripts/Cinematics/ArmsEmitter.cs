using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class ArmsEmitter : MonoBehaviour
{

    public int ArmCount;
    public float Speed;
    public float Rate;
    public GameObject Arm;
    public List<Arm> Arms;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ArmsSceneStart(float duration = 20f)
    {
        gameObject.SetActive(true);
        this.StartCoroutine(ArmsStart(duration));
    }

    public IEnumerator ArmsStart(float duration = 20f)
    {
        var pitchCurve = AnimationCurve.EaseInOut(0.0f, 0.0f, 1.0f, 90.0f);
        float elapsed = 0.0f;
        float rateElapsed = 0.0f;
        //var armBig = Object.Instantiate(Arm).GetComponent<Arm>();
        //armBig.transform.position = new Vector3(transform.position.x, transform.position.y-2, transform.position.z);
        //armBig.spineAnimationState.SetAnimation(0, "rise", true);
        //armBig.transform.localScale *= 2;
        //var skeletonAnimation = armBig.transform.gameObject.GetComponent<SkeletonAnimation>();
        //armBig.spineAnimationState = skeletonAnimation.AnimationState;
        //armBig.SetAnim("rise");
        //Arms.Add(armBig);


        while (elapsed < duration)
        {
            if (Rate < rateElapsed && Arms.Count < ArmCount)
            {
                var arm = Object.Instantiate(Arm).GetComponent<Arm>();
                var scale = Random.Range(0.3f, 1.3f);
                var flipVector = (Random.Range(0f, 1f) > 0.5f) ? new Vector3(-1 * arm.transform.localScale.x, arm.transform.localScale.y, arm.transform.localScale.z) : arm.transform.localScale;
                var pos = Random.Range(-8f, 8f);
                var height = Random.Range(0f, 2f);
                if (Arms.Count <= 0)
                {
                    arm.transform.localScale *= 2;
                    arm.transform.position = new Vector3(transform.position.x, transform.position.y - 6, transform.position.z);
                    var skeletonAnimation = arm.transform.gameObject.GetComponent<SkeletonAnimation>();
                    skeletonAnimation.AnimationState.SetAnimation(0, "rise", true);
                }
                else
                {
                    arm.transform.position = new Vector3(transform.position.x + pos, transform.position.y - height, transform.position.z);
                    arm.transform.localScale = flipVector;
                    arm.transform.localScale *= scale;
                    arm.AnimSpeed = Random.Range(0.1f, 0.8f) * scale;
                }

                Arms.Add(arm);
            }

            rateElapsed += Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }
        yield break;
    }
    public void ArmsSceneEnd(float duration = 0.8f)
    {
        Arms.ForEach(x => x.transform.gameObject.SetActive(false));
        gameObject.SetActive(false);

    }
}
