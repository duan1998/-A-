using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Start is called before the first frame update
class Cell
{
    public int G;//已花费（从起点到该点）
    public int H;//预期花费（曼哈顿距离）
    public int F;//F=G+H
    public Vector2Int pos;
    public Cell parent;
    public Cell(Vector2Int pos, Vector2Int end, int G)
    {
        this.pos = pos;
        SetGAndHAndF(end, G);
    }
    public Cell(Vector2Int pos)
    {
        this.pos = pos;
        this.G = -1;
        this.H = -1;
        this.F = -1;
    }
    public Cell()
    {

    }
    public void SetGAndHAndF(Vector2Int end, int G)
    {
        this.G = G;
        this.H = Mathf.Abs(end.x - pos.x) + Mathf.Abs(end.y - pos.y);
        this.F = G + H;
    }
};


public class AStar : MonoBehaviour
{
    private static int INF = 1000000000;
    private static int[,] dir = new int[4, 2] { { 1, 0 }, { -1, 0 }, { 0, -1 }, { 0, 1 } };

    private static Dictionary<Vector2Int, Cell> cellDic = new Dictionary<Vector2Int, Cell>();


    /// <summary>
    /// 
    /// </summary>
    /// <param name="originIndex">起始点位置</param>
    /// <param name="endIndex">终点位置</param>
    /// <returns></returns>
    public static List<Vector2Int> AutomaticPathFinding(Vector2Int originPos, Vector2Int endPos,bool AI)
    {
        if (MapMgr.Instance == null)
        {
            Debug.LogError("地图没有加载");
            return null;
        }
        int[][] map = MapMgr.Instance.m_mapCost;
        int width = map.Length;
        int height = map[0].Length;

        cellDic.Clear();
        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
                cellDic.Add(new Vector2Int(i, j), new Cell(new Vector2Int(i, j)));

        //保存最终路径
        List<Vector2Int> pathList = new List<Vector2Int>();
        //待选择
        List<Cell> openList = new List<Cell>();

        List<Cell> clostList = new List<Cell>();

        //加入起点
        openList.Add(cellDic[originPos]);
        cellDic[originPos].SetGAndHAndF(endPos, 0);
        while (openList.Count > 0)
        {
            Cell cell = GetSmallFValue(openList);
            openList.Remove(cell);
            clostList.Add(cell);
            if (cell.pos == endPos)
            {

                //找到路径，存起来
                while (cell != null)
                {
                    Debug.Log(cell.pos);
                    pathList.Add(cell.pos);
                    cell = cell.parent;
                }
                pathList.Reverse();
                return pathList;
            }
            Cell newCell = null;
            for (int i = 0; i < 4; i++)
            {
                Vector2Int tmpVector2 = new Vector2Int(cell.pos.x + dir[i, 0], cell.pos.y + dir[i, 1]);
                if (Check(tmpVector2, width, height,true))
                {
                    newCell = cellDic[tmpVector2];
                    if (openList.Contains(newCell))
                    {
                        int newCellF = cell.G + map[newCell.pos.x][newCell.pos.y] +Mathf.Abs(tmpVector2.x - endPos.x) + Mathf.Abs(tmpVector2.y - endPos.y);
                        if (newCellF < newCell.F)
                        {
                            newCell.SetGAndHAndF(endPos, cell.G + map[newCell.pos.x][newCell.pos.y]);
                            newCell.parent = cell;
                        }
                    }
                    else if(!clostList.Contains(newCell))
                    {
                        newCell.SetGAndHAndF(endPos, cell.G+ map[newCell.pos.x][newCell.pos.y]);
                        openList.Add(newCell);
                        newCell.parent = cell;
                    }
                }
            }
        }
        return null;
    }



    public static List<Vector2Int> AutomaticPathFinding(Vector2Int originPos, Vector2Int endPos)
    {
        if (MapMgr.Instance == null)
        {
            Debug.LogError("地图没有加载");
            return null;
        }
        bool[,] maps = new bool[MapMgr.Instance.m_mapItems.GetLength(0),MapMgr.Instance.m_mapItems.GetLength(1)];
        for(int i=0;i<maps.GetLength(0);i++)
        {
            for(int j=0;j<maps.GetLength(1);j++)
            {
               maps[i,j]=MapMgr.Instance.m_mapItems[i, j].m_isWalkable;
            }
        }
        
        cellDic.Clear();
        for (int i = 0; i < maps.GetLength(0); i++)
            for (int j = 0; j < maps.GetLength(1); j++)
                cellDic.Add(new Vector2Int(i, j), new Cell(new Vector2Int(i, j)));

        //保存最终路径
        List<Vector2Int> pathList = new List<Vector2Int>();
        //待选择
        List<Cell> openList = new List<Cell>();

        List<Cell> clostList = new List<Cell>();

        //加入起点
        openList.Add(cellDic[originPos]);
        cellDic[originPos].SetGAndHAndF(endPos, 0);
        while (openList.Count > 0)
        {
            Cell cell = GetSmallFValue(openList);
            openList.Remove(cell);
            clostList.Add(cell);
            if (cell.pos == endPos)
            {

                //找到路径，存起来
                while (cell != null)
                {
                    Debug.Log(cell.pos);
                    pathList.Add(cell.pos);
                    cell = cell.parent;
                }
                pathList.Reverse();
                return pathList;
            }
            Cell newCell = null;
            for (int i = 0; i < 4; i++)
            {
                Vector2Int tmpVector2 = new Vector2Int(cell.pos.x + dir[i, 0], cell.pos.y + dir[i, 1]);
                if (Check(tmpVector2, maps.GetLength(0), maps.GetLength(1),maps))
                {
                    newCell = cellDic[tmpVector2];
                    if (openList.Contains(newCell))
                    {
                        int newCellF = cell.G + Mathf.Abs(tmpVector2.x - endPos.x) + Mathf.Abs(tmpVector2.y - endPos.y);
                        if (newCellF < newCell.F)
                        {
                            newCell.SetGAndHAndF(endPos, cell.G );
                            newCell.parent = cell;
                        }
                    }
                    else if (!clostList.Contains(newCell))
                    {
                        newCell.SetGAndHAndF(endPos, cell.G);
                        openList.Add(newCell);
                        newCell.parent = cell;
                    }
                }
            }
        }
        return null;
    }


    static bool Check(Vector2Int pos, int width, int height,bool[,] maps)
    {
        if (pos.x < 0 || pos.x >= width || pos.y < 0 || pos.y >= height|| !maps[pos.x,pos.y])
        {
            return false;
        }
        return true;
    }


    static bool Check(Vector2Int pos, int width, int height,bool AI)
    {
        if (pos.x < 0 || pos.x >= width || pos.y < 0 || pos.y >= height)
        {
            return false;
        }
        return true;
    }
    static Cell GetSmallFValue(List<Cell> list)
    {
        Cell newCell = null;
        int minF = INF;
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].F < minF)
            {
                minF = list[i].F;
                newCell = list[i];
            }
        }
        return newCell;
    }


}

