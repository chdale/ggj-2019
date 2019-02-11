using UnityEngine;

namespace Assets.Hatch.Scripts.Events
{
    public class OpenModal : TriggeredEvent
    {
        public bool isActive = false;
        public GameObject modal;

        private GameController gameController; 

        private void Awake()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            gameController = GameObject.Find("GameController").GetComponent<GameController>();
            GameController.Interact += ConditionallyTriggerEvent;
        }

        public override void TriggerEvent()
        {
            isActive = true;
            gameController.StopCharacter();
            gameController.InteractInactiveEvent();
            modal.SetActive(true);
        }

        public void Success()
        {
            if (isActive)
            {
                isActive = false;
                gameController.FinishModalEvent();
                this.transform.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }
}
