using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicsController : GenericSingleton<GraphicsController>
{
    public Sprite[] availableBees;
    public Sprite[] availableBG;

    public SpriteRenderer bee;
    public Image background;
    private void Start()
    {
        bee.sprite = availableBees[Mathf.Min(StateMachine.Instance.NumberOfBonusUsed, availableBees.Length - 1)];
        background.sprite = availableBG[Mathf.Min(StateMachine.Instance.NumberOfBonusUsed, availableBG.Length - 1)];
    }

}
