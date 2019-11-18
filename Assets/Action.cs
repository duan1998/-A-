using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public enum ActionName
{
    Move,
    Attack,
    AddOne
}
public abstract class Action : MonoBehaviour
{

    public ActionCtrl m_actionCtrl;
    public DiceCtrl m_diceCtrl;
    //是否已经被使用过
    public bool m_isUsed;

    public Image m_maskImage;

    public ActionName m_actionName;

    public PlayerCtrl m_playerCtrl;

    public virtual void UseAction(Dice dice)
    {
        dice.m_trans.position = this.transform.position;

        m_isUsed = true;
        m_actionCtrl.DiceIntoTheActionUI(this,dice);
    }

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

    
    public virtual void CancelOperation(Dice dice)
    {
        dice.m_trans.position = dice.m_lastPosition;
        m_playerCtrl.m_currentPlayerActionType = PlayerActionType.Null;
        m_isUsed = false;


    }
    public virtual void OverAction(Dice dice)
    {
        m_playerCtrl.m_currentPlayerActionType = PlayerActionType.Null;
    }

}
