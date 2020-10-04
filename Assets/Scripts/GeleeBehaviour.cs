using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeleeBehaviour : MonoBehaviour
{
    public Animator anim;

    private float timer = 0;
    private bool canUseBonus = true;
    private bool bonusInUse = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && canUseBonus)
        {
            canUseBonus = false;
            bonusInUse = true;
            anim.SetBool("GeleeAvailable", false);
            GameController.Instance.TakeBonus();
            timer = 0;
        }
        if(timer < StateMachine.Instance.CurrentBonusDuration + StateMachine.Instance.BonusCd)
        {
            timer += Time.deltaTime;
            //we finished the bonus effects but havent finished cooldown
            if (timer > StateMachine.Instance.CurrentBonusDuration && bonusInUse)
            {
                bonusInUse = false;
                GameController.Instance.EndBonus();
            }
        }
        else if (!canUseBonus)
        {
            canUseBonus = true;
            anim.SetBool("GeleeAvailable", true);
        }
    }
}
