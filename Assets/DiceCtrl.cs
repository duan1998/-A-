using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Dice
{
    public Transform m_trans;
    public Vector3 m_lastPosition;
    public int m_index;
    public int m_value;
}
public class DiceCtrl : MonoBehaviour
{

    private Dice[] m_dices;
    public DiceUI m_diceUI;

    public Dice[] _Dices
    {
        get
        {
            if(m_dices==null)
            {
                m_dices= new Dice[3];
                for (int i = 0; i < m_dices.Length; i++)
                {
                    m_dices[i] = new Dice();
                    m_dices[i].m_index = i;
                }
            }
            return m_dices;
        }
        set
        {
            m_dices = value;
        }
    }

    /// <summary>
    /// 获取三个随机数
    /// </summary>
    public void GetNewThreeDice()
    {
        int[] diceNumbers = new int[_Dices.Length];
        for(int i=0;i< _Dices.Length;i++)
        {
            m_dices[i].m_value = Random.Range(1, 7);
            diceNumbers[i] = _Dices[i].m_value;
        }
        m_diceUI.UpdateDiceUIInNextTurn(diceNumbers);
    }
    public void ModifyDiceNumber(int index,int value)
    {
        int[] diceNumbers = new int[_Dices.Length];
        m_dices[index].m_value=Mathf.Clamp(value,1,6);
        for (int i = 0; i < _Dices.Length; i++)
        {
            diceNumbers[i] = _Dices[i].m_value;
        }
        m_diceUI.UpdateDiceUIInCurrentTurn(diceNumbers);
    }





}
