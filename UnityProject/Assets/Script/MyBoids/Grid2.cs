using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid2 : MonoBehaviour
{
    float cellSize;
    Vector3 origin;

    public Cell2[,] cells;
    public Cell2 filledCells;
    public int numOfFilled;

    public BoidSettings boidSettings;

    public void Initialize(float cs, Vector3 o, int s)
    {
        cellSize = cs;
        origin = o;
        int size = (int)(s / cs) + 1;

        cells = new Cell2[size, size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                cells[i, j] = new Cell2();
                cells[i, j].grid = this;
                cells[i, j].cellPosition = new Vector3(origin.x + j * cellSize, origin.y - i * cellSize);
            }
        }

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                cells[i, j].perceptionCells = GetCellsInRadius(cellSize, i, j, boidSettings.perceptionRadius);
                cells[i, j].avoidCells = GetCellsInRadius(cellSize, i, j, boidSettings.avoidanceRadius);
                cells[i, j].avoidCells.Add(cells[i, j]);
            }
        }
    }

    public void AddBoidToGrid(Boid2 b)
    {
        Cell2 cell = GetCellFromPosition(b.position);

        if(cell.head == null)
        {
            AddCell(cell);
        }

        cell.AddBoid(b);
    }

    public void Move(Boid2 b)
    {
        Cell2 cell = GetCellFromPosition(b.position);

        //stays in cell
        if(cell == b.cell)
        {
            cell.UpdateValues(b.position - b.oldPosition, b.up - b.oldUp);
            return;
        }

        //remove boid from old cell
        b.cell.RemoveBoid(b);
        b.cell.UpdateValues(-b.oldPosition, -b.oldUp);

        //add new cell to filled if was empty
        if (cell.numOfBoids == 0)
        {
            AddCell(cell);
        }

        //add boid to new cell;
        cell.AddBoid(b);
        cell.UpdateValues(b.position, b.up);
    }

    public void AddCell(Cell2 c)
    {
        c.previousCell = null;
        c.nextCell = filledCells;
        filledCells = c;
        if (c.nextCell != null)
        {
            c.nextCell.previousCell = c;
        }
        numOfFilled++;
    }

    public void RemoveCell(Cell2 c)
    {
        if (c.previousCell != null)
        {
            c.previousCell.nextCell = c.nextCell;
        }

        if (c.nextCell != null)
        {
            c.nextCell.previousCell = c.previousCell;
        }

        if (filledCells == c)
        {
            filledCells = c.nextCell;
        }
        numOfFilled--;
    }

    public List<Cell2> GetCellsInRadius(float cellSize, int r, int c, float radius)
    {
        int size = (int)(radius * 2 / cellSize);
        List<Cell2> rtn = new List<Cell2>();

        int startY, startX, endY, endX;
        startY = Mathf.Min(Mathf.Max(0, r - size / 2), cells.GetLength(0) - 1);
        startX = Mathf.Min(Mathf.Max(0, c - size / 2), cells.GetLength(1) - 1);
        endY = Mathf.Min(Mathf.Max(0, r + size / 2), cells.GetLength(0) - 1);
        endX = Mathf.Min(Mathf.Max(0, c + size / 2), cells.GetLength(1) - 1);

        for (int y = 0; startY + y <= endY; y++)
        {
            for (int x = 0; startX + x <= endX; x++)
            {
                if (!(startY + y == r && startX + x == c))
                    rtn.Add(cells[startY + y, startX + x]);
            }
        }

        return rtn;
    }

    private Cell2 GetCellFromPosition(Vector3 pos)
    {
        int cellY = (int)((origin.y - pos.y) / cellSize);
        int cellX = (int)((pos.x - origin.x) / cellSize);
        return cells[cellY, cellX];
    }

    public void DebugGridSize()
    {
        int l = cells.GetLength(0) - 1;
        Debug.DrawLine(cells[0, 0].cellPosition, cells[l, l].cellPosition);
        //Debug.DrawLine(cells[0, l].cellPosition, cells[l, 0].cellPosition);
    }

    public void DebugGridFilled()
    {
        int cc = 0;
        Cell2 c = filledCells;
        while (c != null)
        {
            //Debug.DrawRay(c.cellPosition, new Vector3(1, -1) * cellSize, Color.green);
            //c.DebugBoidsInCell();
            c.DebugOtherCells();
            c = c.nextCell;
            cc++;
        }
        //print("filled = " + cc);
    }
}
