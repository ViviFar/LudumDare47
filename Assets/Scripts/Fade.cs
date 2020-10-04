using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    //public void LaunchFade()
    //{
    //    StartCoroutine(Fading());
    //}
    ////TODO modify to use an image instead of a canvas
    //private IEnumerator Fading()
    //{
    //    CanvasGroup cvg = GetComponent<CanvasGroup>();
    //    while (cvg.alpha > 0)
    //    {
    //        cvg.alpha -= Time.deltaTime / 2;
    //        yield return null;
    //    }
    //    cvg.interactable = false;
    //    yield return null;
    //    GameController.Instance.Restart();
    //}
    [HideInInspector]
    public bool fading = false;

    public Image img;

    bool shuttingDown = false;

    private void Update()
    {
        if (fading)
        {
            img.CrossFadeAlpha(1.2f, 2.0f, false);
        }
        else
        {
            img.CrossFadeAlpha(0, 2.0f, false);
            if (!shuttingDown)
            {
                shuttingDown = true;
            }
        }
    }
}
