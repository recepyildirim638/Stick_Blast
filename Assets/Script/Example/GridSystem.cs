using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reha
{
    public sealed class GridSystem <T> : BaseGridSystem where T : IGrid, new()
    {
        private T[] grids;
        public GridSystem(int w, int h)
        {
            this.width = w;
            this.height = h;

            Count = width * height;
            grids = new T[Count];

            for (int i = 0; i < Count; i++)
            {
                grids[i] = new T();
                grids[i].Index = i;
            }             
        }

        public T[] GetAll() => grids;
        public T GetGrid(int index)
        {
            //if (index < 0 || index >= Count) 
            //    return new T();

            return grids[index];
        } 
        public T GetDownNeighbor(int index)
        {
            int x = index % width;
            int y = index / width;

            int neighborY = y - 1;

            if (neighborY >= 0 && neighborY < height)
            {
                int neighborIndex = neighborY * width + x;
                return grids[neighborIndex];
            }
            return default;
        }
        public T GetUpNeighbor(int index)
        {
            int x = index % width;
            int y = index / width;

            int neighborY = y + 1;

            if (neighborY >= 0 && neighborY < height)
            {
                int neighborIndex = neighborY * width + x;
                return grids[neighborIndex];
            }
            return default; 
        }
        public T GetLeftNeighbor(int index)
        {
            int x = index % width;
            int y = index / width;

            int neighborX = x - 1; 

            if (neighborX >= 0 && neighborX < width)
            {
                int neighborIndex = y * width + neighborX;
                return grids[neighborIndex];
            }
            return default;
        }
        public T GetRightNeighbor(int index)
        {
            int x = index % width;
            int y = index / width;

            int neighborX = x + 1; 

            if (neighborX >= 0 && neighborX < width)
            {
                int neighborIndex = y * width + neighborX;
                return grids[neighborIndex];
            }
            return default;
        }

        public T GetNextNeighbor(int index)
        {
            index++;
            return grids[index];
        }


        public List<T> GetNeighbors(int index)
        {
            List<T> neighbors = new List<T>(4);

            int x = index % width;  
            int y = index / width;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (Mathf.Abs(i + j) != 1) continue;

                    int neighborX = x + i;
                    int neighborY = y + j;

                    if (neighborX >= 0 && neighborX < width && neighborY >= 0 && neighborY < height)
                    {
                        int neighborIndex = neighborY * width + neighborX;
                        neighbors.Add(grids[neighborIndex]);
                    }
                }
            }

            return neighbors;
        }
        public List<T> GetNeighbors4x4(int index)
        {
            List<T> neighbors = new List<T>(8);

            int x = index % width; 
            int y = index / width;   

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0) continue;

                    int neighborX = x + i;
                    int neighborY = y + j;

                    if (neighborX >= 0 && neighborX < width && neighborY >= 0 && neighborY < height)
                    {
                        int neighborIndex = neighborY * width + neighborX;
                        neighbors.Add(grids[neighborIndex]);
                    }
                }
            }

            return neighbors;
        }

        public T GetDirection(int index, int[] directions)
        {
            int x = index % width;  
            int y = index / width;

            int neighborX = x + directions[0];
            int neighborY = y + directions[1];

            if (neighborX >= 0 && neighborX < width && neighborY >= 0 && neighborY < height)
            {
                int neighborIndex = neighborY * width + neighborX; // Komþunun index'i
                return grids[neighborIndex];
            }

            return default; // Komþularýn listesini döner
        }
      

        public List<T> GetLineGrid(int lineIndex)
        {
            int lineStartIndex = (lineIndex - 1) * width;
            List<T> neighbors = new List<T>(width);
            for (int i = 0; i < width; i++)
            {
                neighbors.Add(grids[i + lineStartIndex]);
            }
            return neighbors;
        }
        public override void SetGridPosition(int index, Vector3 pos) => grids[index].Position = pos;

    }

    public abstract class BaseGridSystem
    {
        protected int width;
        protected int height;
        protected int Count;

        public int GetCount() => Count;
        public int Height => height;
        public int Width => width;

        public abstract void SetGridPosition(int index, Vector3 pos);
    }

    public interface IGrid 
    {
        public int Index { get; set; }
        public Vector3 Position { get; set; }
    }
}

