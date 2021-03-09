using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ConsoleText : ConsoleSceneObject
{
    public string Text;
    public bool Centered;
    public ConsoleText(string Text, Vector2 Position, bool Centered = false) : base(Position)
    {
        this.Centered = Centered;
        this.Text = Text;
    }

    public override void Render(TMPConsole.ConsoleBuffer Buffer)
    {
        Buffer.WriteAt(Text, Position.ToInt2(), Centered);
    }
}

public class ConsoleBorder : ConsoleSceneObject
{
    public readonly ConsoleText Text;
    public bool Centered;
    public ConsoleBorder(string Text, Vector2 Position, bool Centered = false) : base(Position)
    {
        this.Centered = Centered;
        this.Text = new ConsoleText(Text, Position, Centered);
    }

    public override void Render(TMPConsole.ConsoleBuffer Buffer)
    {
        Text.Render(Buffer);  
    }
}
