using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneSubs : Subtitles
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected override IEnumerator Sequence()
    {
        base.Sequence();
        switch (StateMachine.Instance.CurrentLevel % 6)
        {
            case 1:
                textBox.text = "The moths were near but Lily was here";
                yield return new WaitForSeconds(3);
                break;
            case 2:
                textBox.text = "Scary as they may be, They're no match for Lily";
                yield return new WaitForSeconds(3);
                break;
            case 3:
                textBox.text = "The hideous horde advance but the lone be holds her ground";
                yield return new WaitForSeconds(3);
                break;
            case 4:
                textBox.text = "A tremendous enemy against a brave little bee";
                yield return new WaitForSeconds(3);
                break;
            case 5:
                textBox.text = "An unbeatable army were it not for our fearless Lily";
                yield return new WaitForSeconds(3);
                break;
            default:
                break;
        }
        textBox.text = "";
        yield return new WaitForSeconds(0.5f);
        switch (StateMachine.Instance.NumberOfBonusUsed / 2)
        {
            case 0:
                textBox.text = "Healthy and strong, Lily is ready";
                break;
            case 1:
                textBox.text = "A sip of nectar for Lily and nothing can beat this bee";
                break;
            case 2:
                textBox.text = "It's just to keep the moths away that Lily needs a little help";
                break;
            case 3:
                textBox.text = "The nectare... Just one more sip...";
                yield return new WaitForSeconds(2);
                textBox.text = "Just a bit more before the sleep";
                yield return new WaitForSeconds(3);
                break;
            default:
                break;
        }
    }
}
