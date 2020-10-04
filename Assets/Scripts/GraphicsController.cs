using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicsController : GenericSingleton<GraphicsController>
{
    public Sprite[] availableBees;
    public Sprite[] availableBG;

    public Sprite DruggedBee;

    public SpriteRenderer bee;
    public Image background;
    private Animator anim;

    private void Start()
    {
        anim = bee.GetComponent<Animator>();
        UpdateSprites(false);
    }

    public void UpdateSprites(bool isDrugged)
    {
        if (anim != null)
        {
            anim.SetInteger("drugLvl", StateMachine.Instance.NumberOfBonusUsed);
        }
        if (isDrugged)
        {
            bee.sprite = DruggedBee;
            background.sprite = availableBG[0];
            anim.SetBool("drugged", true);
        }
        else
        {
            anim.SetBool("drugged", false);
            bee.sprite = availableBees[Mathf.Min(StateMachine.Instance.NumberOfBonusUsed, availableBees.Length - 1)];
            background.sprite = availableBG[Mathf.Min(StateMachine.Instance.NumberOfBonusUsed, availableBG.Length - 1)];
        }
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            anim.SetInteger("drugLvl", 1);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            anim.SetInteger("drugLvl", 2);
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            anim.SetInteger("drugLvl", 3);
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            anim.SetInteger("drugLvl", 4);
        }
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            anim.SetInteger("drugLvl", 5);
        }
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            anim.SetInteger("drugLvl", 6);
        }
        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            anim.SetInteger("drugLvl", 7);
        }
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            anim.SetInteger("drugLvl", 0);
        }


#endif
    }

}
