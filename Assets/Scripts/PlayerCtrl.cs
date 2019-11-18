using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerActionType
{
    CanMove,
    CanAttack,
    CanAddOne,
    Null
}

public class PlayerCtrl : MonoBehaviour
{
    public float m_moveSpeed;


    public bool m_isCanAction=false;

    public bool m_isDragDice;

    public PlayerActionType m_currentPlayerActionType=PlayerActionType.Null;

    public ActionCtrl m_actionCtrl;
    private void Start()
    {
        MapMgr.Instance.m_mapItems[(int)transform.position.x,(int)transform.position.z].m_currentType = MapItemType.Player;
    }
    // Update is called once per frame
    void Update()
    {


        if(m_isCanAction)
        {
            if (Input.GetMouseButtonDown(1))
            {
                m_currentPlayerActionType = PlayerActionType.Null;
            }
            switch (m_currentPlayerActionType)
            {
                case PlayerActionType.Null:
                    break;
                case PlayerActionType.CanMove:
                    {
                        if(Input.GetMouseButtonDown(0))
                        {
                            RaycastHit hit;
                            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit))
                            {
                                if(hit.collider.CompareTag("MapItem"))
                                {
                                    if(hit.collider.GetComponent<MapItem>().m_isWalkable)
                                    {
                                        Move(hit.collider.GetComponent<MapItem>().m_itemPosVector2);
                                    }
                                }
                            }
                        }
                    }
                    break;
                case PlayerActionType.CanAttack:
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            RaycastHit[] hits= Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition));
                            if(hits!=null)
                            {
                                for(int i=0;i<hits.Length;i++)
                                {
                                    if(hits[i].collider.CompareTag("Enemy"))
                                    {
                                        Vector3 enemyPos = hits[i].collider.transform.position;
                                        if (MapMgr.Instance.m_mapItems[(int)enemyPos.x,(int)enemyPos.z].m_currentType==MapItemType.Enemy)
                                        {
                                            Attack(hits[i].collider.GetComponent<AICtrl>());
                                            break;
                                        }
                                    }
                                }

                            }
                        }
                        break;
                    }
                case PlayerActionType.CanAddOne:
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            RaycastHit[] hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition));
                            if (hits != null)
                            {
                                for (int i = 0; i < hits.Length; i++)
                                {
                                    if (hits[i].collider.CompareTag("Player"))
                                    {
                                        m_actionCtrl.OverAction();
                                        break;
                                    }
                                }

                            }
                        }
                    }
                    break;
            }
        }



    }



    /// <summary>
    /// 显示路径
    /// </summary>
    /// <param name="stepNumber"></param>
    public void PrepareMove(int stepNumber)
    {
        //显示路径
        MapMgr.Instance.SetWalkableItemAndShow(GetCurrentPos(),stepNumber);
        m_currentPlayerActionType = PlayerActionType.CanMove;

    }
    void Move(Vector2Int targetPos)
    {
        m_actionCtrl.OverAction();
        //A*算法
        //记录起点当前位置
        Vector2Int startPos = GetCurrentPos();

        StopAllCoroutines();
        index = 0;

        StartCoroutine("MoveTo", Vector2IntSwitchVector3(AStar.AutomaticPathFinding(startPos, targetPos).ToArray()));
        MapMgr.Instance.HideWaleableItem();

    }







    int attackDamage;
    /// <summary>
    /// 显示可攻击
    /// </summary>
    public void PrepareAttack(int attackDamage)
    {
        MapMgr.Instance.SetAttackableAndShow(GetCurrentPos());
        this.attackDamage = attackDamage;
        m_currentPlayerActionType = PlayerActionType.CanAttack;
    }

    public void Attack(AICtrl enemy)
    {
        m_actionCtrl.OverAction();
        enemy.Hit(attackDamage);
        MapMgr.Instance.HideAttackableItem();

    }









    Vector2Int GetCurrentPos()
    {
        return new Vector2Int(transform.position.x - (int)transform.position.x > 0.5f ? (int)(transform.position.x + 1) : (int)transform.position.x,
            transform.position.z - (int)transform.position.z > 0.5f ? (int)(transform.position.z + 1) : (int)transform.position.z);
    }

    Vector3[] Vector2IntSwitchVector3(Vector2Int[] targets)
    {
        Vector3[] tempArrs = new Vector3[targets.Length];
        for(int i=0;i< targets.Length;i++)
        {
            tempArrs[i] = new Vector3(targets[i].x, transform.position.y, targets[i].y);
        }
        return tempArrs;
    }


    int index;
    IEnumerator MoveTo(Vector3[] targets)
    {
        MapMgr.Instance.SetItemType(GetCurrentPos(), MapItemType.Null);
        while (true)
        {
            float step = m_moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targets[index], step);
            if (Vector3.Distance(transform.position, targets[index]) < 0.001f)
            {
                if (index == targets.Length - 1)
                {
                    print("寻路结束");
                    break;
                }
                else
                {
                    index++;
                    yield return null;
                }
            }
            else
            {
                yield return null;
            }
        }
        //重置
        MapMgr.Instance.SetItemType(GetCurrentPos(), MapItemType.Player);
        
    }




}
