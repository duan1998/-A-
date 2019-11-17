using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public enum ActionType
{
    Move,
    Attack,
    AddDiceNumber
}
public abstract class Action : MonoBehaviour
{



    //是否已经被使用过
    public bool m_isUsed;

    public Image m_maskImage;

    public ActionType m_actionType;



    public abstract void UseAction(Transform targetDiceTrans, int diceNumber);

    public void DiceUIEnterHovered()
    {
        if(m_isUsed)
        {
            m_maskImage.enabled = true;
        }
    }


    public void DiceUIExitHovered()
    {
        if(m_isUsed)
        {
            m_maskImage.enabled = false;
        }
    }
}
