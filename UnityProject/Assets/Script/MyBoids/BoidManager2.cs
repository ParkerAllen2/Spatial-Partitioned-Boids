using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoidManager2 : MonoBehaviour
{
    public BoidSettings settings;
    public Grid2 grid;
    List<Boid2> boids;

    public Transform target;
    [HideInInspector] public int numBoids;

    public int maxFlockSize = 100;

    public bool debugBool;

    public void FindAllBoids()
    {
        if (boids == null)
            boids = new List<Boid2>();

        boids.Clear();
        boids.AddRange(FindObjectsOfType<Boid2>());
        numBoids = boids.Count;
        foreach (Boid2 b in boids)
        {
            b.Initialize(this, settings, target);
            grid.AddBoidToGrid(b);
        }
    }

    public void AddBoid(Boid2 b)
    {
        if (boids == null)
            boids = new List<Boid2>();

        b.Initialize(this, settings, target);
        numBoids++;
        boids.Add(b);
        grid.AddBoidToGrid(b);
    }

    public void Update()
    {
        MoveBoids();
    }

    public void MoveBoids()
    {
        CalculateCells();

        foreach(Boid2 b in boids)
        {
            b.UpdateBoid();
            grid.Move(b);
        }
    }

    public void CalculateCells()
    {
        Cell2 c = grid.filledCells;
        int fcNum = grid.numOfFilled;
        for(int i = 0; i < fcNum; i++)
        {
            c.ResetTotals();
            c = c.nextCell;
        }
        c = grid.filledCells;
        for (int i = 0; i < fcNum; i++)
        {
            c.AddToCells();
            CalculateAvoidence(c);
            c = c.nextCell;
        }
    }

    public void CalculateAvoidence(Cell2 c)
    {
        Boid2 b1 = c.head;
        for (int i = 0; i < c.numOfBoids; i++)
        {
            Vector3 separationHeading = new Vector3(0, 0, 0);

            int acNum = c.avoidCells.Count;
            for(int x = 0; x < acNum; x++)
            {
                Cell2 c2 = c.avoidCells[x];
                int c2Num = c2.numOfBoids;
                Boid2 b2 = c2.head;
                for (int j = 0; j < c2Num; j++)
                {
                    if(b1 != b2)
                    {
                        Vector3 offset = new Vector3(b2.position.x - b1.position.x, b2.position.y - b1.position.y, 0);
                        float sqrDst = offset.x * offset.x + offset.y * offset.y;
                        offset = new Vector3( offset.x / sqrDst, offset.y / sqrDst, 0);
                        separationHeading = new Vector3(separationHeading.x + offset.x, separationHeading.y + offset.y, 0);
                    }
                    b2 = b2.nextBoid;
                }
            }
            b1.avgAvoidanceHeading = -separationHeading;
            b1 = b1.nextBoid;
        }
    }

    public void DeleteBoids()
    {
        foreach(Boid2 b in boids)
        {
            Destroy(b.gameObject);
        }
        boids.Clear();
        numBoids = 0;
    }
}
