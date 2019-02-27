using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class InteractEvent : MonoBehaviour
{
    private GameEventManager manager;
    private Platformer2DUserControl user;

    private void Awake()
    {
        manager = transform.parent.GetComponent<GameEventManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (manager != null)
            {
                manager.EnteredEvent();
            }
            else
            {
                manager = transform.parent.GetComponent<GameEventManager>();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (manager != null)
            {
                manager.ExitedEvent();
            }
            else
            {
                manager = transform.parent.GetComponent<GameEventManager>();
            }
        }
    }
}
