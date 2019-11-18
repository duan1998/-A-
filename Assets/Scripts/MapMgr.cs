using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMgr : MonoBehaviour
{

    private static MapMgr m_instance;
    public static MapMgr Instance
    {
        get { return m_instance; }
    }

    public MapItem[,] m_mapItems;
    public GameObject[,] m_mapWalkableItems;
    public GameObject[,] m_mapAttackableRangeItems;
    public GameObject[,] m_mapAttackableItems;
   


    public int[][] m_mapCost = {
    new int[8]{ 1,1,1,1,1,1,1,1},
    new int[8]{ 1,1,1,1,1,1,1,1},
    new int[8]{ 1,1,1,1,1,1,1,1},
    new int[8]{ 1,1,1,1,1,1,1,1},
    new int[8]{ 1,1,1,1,1,1,1,1},
    new int[8]{ 1,1,1,1,1,1,1,1},
    new int[8]{ 1,1,1,1,1,1,1,1},
    new int[8]{ 1,1,1,1,1,1,1,1},
    };

    


    void Awake()
    {
        m_instance = this;
    }

    void Start()
    {
        InitMap();

    }
    void InitMap()
    {
        int rows = this.transform.childCount;
        int cols = this.transform.GetChild(0).childCount;
        m_mapItems = new MapItem[cols,rows];


        m_mapWalkableItems = new GameObject[cols, rows];
        m_mapAttackableRangeItems = new GameObject[cols, rows];
        m_mapAttackableItems = new GameObject[cols, rows];


        MapItem[,] tempItems = new MapItem[rows, cols];



        for (int i = 0; i < rows; i++)
        {
            Transform rowTrans = this.transform.GetChild(i);
            for (int j = 0; j < cols; j++)
            {
                tempItems[i, j] = rowTrans.GetChild(j).GetComponent<MapItem>();
            }
        }
        for(int i=0;i<cols;i++)
        {
            for(int j=0;j<rows;j++)
            {
                m_mapItems[i, j] = tempItems[j, i];
                m_mapItems[i, j].m_itemPosVector2 = new Vector2Int(i, j);
                m_mapWalkableItems[i, j] = m_mapItems[i, j].transform.GetChild(0).gameObject;
                m_mapAttackableRangeItems[i, j] = m_mapItems[i, j].transform.GetChild(1).gameObject;
                m_mapAttackableItems[i, j] = m_mapItems[i, j].transform.GetChild(2).gameObject;
            }
        }
    }


    



    public void HideWaleableItem()
    {
        for (int i = 0; i < m_mapWalkableItems.GetLength(0); i++)
        {
            for (int j = 0; j < m_mapWalkableItems.GetLength(1); j++)
            {
                if (m_mapWalkableItems[i,j].activeSelf)
                {
                    SetAndHideWalkableItem(i, j);
                }
            }
        }
    }

    public void HideAttackableItem()
    {
        for (int i = 0; i < m_mapAttackableRangeItems.GetLength(0); i++)
        {
            for (int j = 0; j < m_mapAttackableRangeItems.GetLength(1); j++)
            {
                if (m_mapAttackableRangeItems[i,j].activeSelf||m_mapAttackableItems[i,j].activeSelf)
                {
                    SetAndHideAttackableItem(i, j);
                }
            }
        }
    }

    public struct Node
    {
        public int step;
        public int x, y;
        public Node(int step, int x, int y)
        {
            this.step = step;
            this.x = x;
            this.y = y;
        }
    }


    int[,] dirs = new int[4, 2]
    {
        {1, 0},
        {-1,0},
        {0,1},
        {0,-1}
    };
    bool[,] isVisits;
    public void SetWalkableItemAndShow(Vector2Int pos,int stepRange)
    {
        isVisits = new bool[m_mapAttackableRangeItems.GetLength(0), m_mapAttackableRangeItems.GetLength(1)];
        for (int i = 0; i < isVisits.GetLength(0); i++)
            for (int j = 0; j < isVisits.GetLength(1); j++)
                isVisits[i, j] = false;
        Queue<Node> que=new Queue<Node>();
        Node playerNode = new Node(0, pos.x, pos.y);
        que.Enqueue(playerNode);
        while (que.Count > 0)
        {
            Node newNode = que.Peek();
            que.Dequeue();
            isVisits[newNode.x,newNode.y] = true;
            if (newNode.step <= stepRange)
            {
                SetAndShowWalkableItem(newNode.x, newNode.y);
            }
            if(newNode.step+1<=stepRange)
            {
                for (int i = 0; i < dirs.GetLength(0); i++)
                {
                    int xx = newNode.x + dirs[i, 0];
                    int yy = newNode.y + dirs[i, 1];
                    if (CheckWalkable(xx, yy))
                    {
                        Node nextNode = new Node(newNode.step + 1, xx, yy);
                        que.Enqueue(nextNode);
                    }
                }
            }
        }

    }

    public void SetAttackableAndShow(Vector2Int pos)
    {
        //判断四个方向
        //x-1 y
        if (pos.x >= 1)
        {
            SetAndShowAttackableItem(pos.x - 1, pos.y);
        }
        if(pos.x < m_mapAttackableRangeItems.GetLength(1))
        {
            SetAndShowAttackableItem(pos.x + 1, pos.y);

        }
        if(pos.y >= 1)
        {
            SetAndShowAttackableItem(pos.x, pos.y - 1);
        }
        if(pos.y< m_mapAttackableRangeItems.GetLength(0))
        {
            SetAndShowAttackableItem(pos.x, pos.y + 1);
        }
        

    }


    void SetAndShowWalkableItem(int x,int y)
    {
        m_mapWalkableItems[x,y].SetActive(true);
        m_mapItems[x, y].m_isWalkable = true;
        
    }

    void SetAndShowAttackableItem(int x, int y)
    {
        m_mapAttackableRangeItems[x, y].SetActive(true);
        m_mapItems[x, y].m_isAttackable = true;
        if(m_mapItems[x,y].m_currentType==MapItemType.Enemy)
        {
            m_mapAttackableRangeItems[x, y].SetActive(false);
            m_mapAttackableItems[x, y].SetActive(true);
        }
    }
    void SetAndHideWalkableItem(int x,int y)
    {
        m_mapWalkableItems[x, y].SetActive(false);
        m_mapItems[x, y].m_isWalkable = false;
    }
    void SetAndHideAttackableItem(int x, int y)
    {
        m_mapAttackableRangeItems[x, y].SetActive(false);
        m_mapAttackableItems[x, y].SetActive(false);
        m_mapItems[x, y].m_isAttackable = false;
        
    }



    bool CheckWalkable(int x, int y)
    {
        if (x < 0 || x >= m_mapItems.GetLength(1) || y < 0 || y >= m_mapItems.GetLength(0) || isVisits[x, y] || m_mapItems[x, y].m_currentType == MapItemType.Enemy)
            return false;
        return true;
    }


    public void SetItemType(Vector2Int pos,MapItemType type)
    {
        m_mapItems[pos.x, pos.y].m_currentType = type;
    }



}
