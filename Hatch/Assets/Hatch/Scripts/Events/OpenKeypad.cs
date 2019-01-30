using UnityEngine;

namespace Assets.Hatch.Scripts.Events
{
    public class OpenKeypad : InteractEvent
    {
        public GameObject Keypad;

        public delegate void StartKeypadAction();
        public static event StartKeypadAction StartKeypad;

        private void Awake()
        {
            Subscribe();
        }

        public override void TriggerEvent()
        {
            gameController.StopCharacter();
            gameController.InteractInactiveEvent();
            Keypad.SetActive(true);
        }

        public void Success()
        {
            gameController.FinishKeypadEvent();
            GameStates.States[GameStates.ACCESSCODE] = true;
            this.transform.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
