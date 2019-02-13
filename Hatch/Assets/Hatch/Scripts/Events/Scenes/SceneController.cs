using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Hatch.Scripts.Events.Scenes
{
    public class SceneController : MonoBehaviour
    {
        public GameObject Intro;
        private void Start()
        {
            var intro = Intro.GetComponent<Intro>();
            intro.StartScene();
        }
    }
}
