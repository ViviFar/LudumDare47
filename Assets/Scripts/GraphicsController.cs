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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            anim.SetInteger("drugLvl", 1);
        }


#endif
    }

}
