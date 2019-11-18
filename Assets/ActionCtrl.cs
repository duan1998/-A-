using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCtrl : MonoBehaviour
{

    public Action[] m_actions;

    public GameObject m_tipObj;

    public void InitActionState()
    {
        for (int i = 0; i < m_actions.Length; i++)
        {
            m_actions[i].m_isUsed = false;
        }
    }

    public Action GetActionByName(ActionName name)
    {
        for(int i=0;i<m_actions.Length;i++)
        {
            if(m_actions[i].m_actionName==name)
            {
                return m_actions[i];
            }
        }
        Debug.LogError("找不到该Action,名字是" + name);
        return null;
    }

    public bool m_isUsing;
    public Dice m_currentDice;
    [HideInInspector]
    public Action m_currentAction;

    public void DiceIntoTheActionUI(Action action,Dice dice)
    {
        m_isUsing = true;
        m_currentDice = dice;
        m_currentAction = action;
        m_tipObj.SetActive(true);
    }

    public void CancelAction()
    {
        if(m_currentAction!=null)
        {
            m_currentAction.CancelOperation(m_currentDice);
            m_currentAction = null;
            m_currentDice = null;
            m_isUsing = false;
            m_tipObj.SetActive(false);
            
        }
    }
    public void OverAction()
    {
        if (m_currentAction != null)
        {
            m_isUsing = false;
            m_currentAction.OverAction(m_currentDice);
            m_currentDice = null;
            m_currentAction = null;
            m_tipObj.SetActive(false);
        }
    }

    private void Update()
    {
        if (m_isUsing)
        {
            if (Input.GetMouseButtonDown(1))
            {
                CancelAction();
            }
        }
    }



}
