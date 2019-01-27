using UnityEngine;

namespace Assets.Hatch.Scripts.Events
{
    public class OpenKeypad : InteractEvent
    {
        public GameObject Keypad;
        public GameController gameController;

        private void Awake()
        {
            gameController = GameObject.Find("GameController").GetComponent<GameController>();
            Subscribe();
        }

        public override void TriggerEvent()
        {
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
