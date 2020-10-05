using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndSub : Subtitles
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Sequence());
    }

    protected override IEnumerator Sequence()
    {
        base.Sequence();
        textBox.text = "So this is the end of Lily's story";
        yield return new WaitForSeconds(3);
        textBox.text = "We may never know the fate of the bees";
        yield return new WaitForSeconds(3);
        textBox.text = "Our bee fought bravely, as much as can be";
        yield return new WaitForSeconds(3);
        textBox.text = "But spiraled to the end, unknowingly";
        yield return new WaitForSeconds(3);
        textBox.text = "The nectar was here, helpful and yummy";
        yield return new WaitForSeconds(3);
        textBox.text = "A welcome ally in desperate times";
        yield return new WaitForSeconds(2);
        textBox.text = "It just lacked something, the slightest detail";
        yield return new WaitForSeconds(3);
        textBox.text = "A little warning for the loop downhill";
        yield return new WaitForSeconds(3);
        textBox.text = "";
    }
}
