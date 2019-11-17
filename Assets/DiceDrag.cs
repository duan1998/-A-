﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DiceDrag : MonoBehaviour,IDragHandler,IEndDragHandler,IBeginDragHandler
{




    public DiceCtrl m_diceCtrl;

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetAsLastSibling();
    }

    Action m_hoverdAction=null;
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        List<RaycastResult> resultList = new List<RaycastResult>();
        m_graphicRaycaster.Raycast(eventData, resultList);
        bool isHoveredAction = false;
        for (int i = 0; i < resultList.Count; i++)
        {
            Action action = resultList[i].gameObject.GetComponent<Action>();
            if (action != null)
            {
                isHoveredAction = true;
                if (action== m_hoverdAction)
                {
                    break;
                }
                else
                {
                    if (m_hoverdAction != null)
                        m_hoverdAction.DiceUIExitHovered();
                    m_hoverdAction = action;
                    m_hoverdAction.DiceUIEnterHovered();
                }
                break;
            }
        }
        if(!isHoveredAction&&m_hoverdAction!=null)
        {
            m_hoverdAction.DiceUIExitHovered();
            m_hoverdAction = null;
        }

    }


    public EventSystem m_eventSystem;
    public GraphicRaycaster m_graphicRaycaster;

    public void OnEndDrag(PointerEventData eventData)
    {
        List<RaycastResult> resultList=new List<RaycastResult>();
        m_graphicRaycaster.Raycast(eventData,resultList);

        for (int i = 0; i < resultList.Count; i++)
        {
            if (resultList[i].gameObject.GetComponent<Action>() != null)
            {
                resultList[i].gameObject.GetComponent<Action>().UseAction(this.transform, m_diceCtrl._DiceNumbers[transform.GetOrderOfBrother()]);
                break;
            }
        }

    }

    public void HideDiceUI()
    {
        this.gameObject.SetActive(false);
    }


    // Start is called before the first frame update

}
