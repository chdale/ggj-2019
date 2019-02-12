using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    Canvas[] menuCanvases = new Canvas[0];
    GraphicRaycaster[] menuRaycasters = new GraphicRaycaster[0];

    void Awake()
    {
        menuCanvases = GetComponentsInChildren<Canvas>(true);
        menuRaycasters = GetComponentsInChildren<GraphicRaycaster>(true);
    }

    public void SetActive(bool active)
    {
        foreach (var canvas in menuCanvases)
        {
            canvas.enabled = active;
        }

        foreach (var raycaster in menuRaycasters)
        {
            raycaster.enabled = active;
        }
    }
}
