using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Subtitles : MonoBehaviour
{
    public Text textBox;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Sequence());
    }

    protected virtual IEnumerator Sequence()
    {
        textBox.text = "";
        yield return new WaitForSeconds(1);

    }
}
