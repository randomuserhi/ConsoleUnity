using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class ConsoleSceneObject
{
    public Vector2 Position;
    public ConsoleSceneObject(Vector2 Position)
    {
        this.Position = Position;
    }
    public abstract void Render(TMPConsole.ConsoleBuffer Buffer);
}

public class ConsoleScene
{
    public const int MaxNumObjects = 1000;

    private class LayerComparer : IComparer<int>
    {
        public int Compare(int a, int b) //Change such that sortedlayers return a list of objects rather than 1 such that the return 1 here doesnt break everything https://stackoverflow.com/questions/5716423/c-sharp-sortable-collection-which-allows-duplicate-keys
        {
            if (a == b)
                return -1;
            return a - b;
        }
    }
    private SortedList<int, ConsoleSceneObject> Objects = new SortedList<int, ConsoleSceneObject>(new LayerComparer());

    public void Add(ConsoleSceneObject Object, int SortLayer = 0)
    {
        if (Objects.Count > MaxNumObjects)
        {
            Debug.LogWarning("Scene cannot handle anymore objects...");
            return;
        }
        Objects.Add(SortLayer, Object);
        Debug.Log(Objects.Count);
    }

    public void Clear()
    {
        Objects.Clear();
    }

    public void Render(TMPConsole.ConsoleBuffer Buffer) //Could be GPU spread out so the GPU renders each object and determines wha pixels to replace using a depth buffer
    {
        for (int i = 0; i < Objects.Count; i++)
        {
            Objects.Values[i].Render(Buffer);
        }
    }
}
