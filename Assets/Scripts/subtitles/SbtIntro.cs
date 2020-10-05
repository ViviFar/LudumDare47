using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SbtIntro : Subtitles
{

    protected override IEnumerator Sequence()
    {
        base.Sequence();
        if (!StateMachine.Instance.IntroductionPlayed)
        {
            textBox.text = "The death's-head moth are here again";
            yield return new WaitForSeconds(3);
            textBox.text = "Menacing the have and the bees inside";
            yield return new WaitForSeconds(4);
            textBox.text = "Nothing more than appetite in their mind";
            yield return new WaitForSeconds(3);
            textBox.text = "It's time for a bee to rise and to slain";
            yield return new WaitForSeconds(3);
            textBox.text = "";
            yield return new WaitForSeconds(1);
            textBox.text = "A valiant hero, a bee named Lily";
            yield return new WaitForSeconds(3);
            textBox.text = "Aided by nectar, gifted by her queen";
            yield return new WaitForSeconds(3);
        }
        textBox.text = "She will protect all, the hive will be clean";
        yield return new WaitForSeconds(3);
        textBox.text = "That's the destiny of Lily the bee";
        yield return new WaitForSeconds(3);
        textBox.text = "";
        yield return new WaitForSeconds(1);
    }
}
