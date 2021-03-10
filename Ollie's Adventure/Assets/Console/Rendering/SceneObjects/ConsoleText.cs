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
    public string BorderChars;
    private StringBuilder Border;
    private string BorderSprite;
    public int LeftMargin;
    public int RightMargin;
    public int Width; //when 0 scales to fit text
    public ConsoleBorder(string Text, Vector2 Position, int LeftMargin = 0, int RightMargin = 0, int Width = 0, string BorderChars = "═║╔╗╚╝", bool Centered = false) : base(Position)
    {
        this.LeftMargin = LeftMargin;
        this.RightMargin = RightMargin;
        this.Width = Width;
        this.BorderChars = BorderChars;
        this.Centered = Centered;
        this.Text = new ConsoleText(Text, Position, Centered);
        Border = new StringBuilder();
        GenerateBorder();
    }

    private void GenerateBorder()
    {
        int BorderLength = LeftMargin + RightMargin + (Width == 0 ? Text.Text.Length : Width);

        Border.Clear();
        Border.Append(BorderChars[2]);
        for (int i = 0; i < BorderLength; i++)
        {
            Border.Append(BorderChars[0]);
        }
        Border.Append(BorderChars[3]);

        //TODO:: somehow make this happen for how many lines text occupies
        Border.Append('\n');
        Border.Append(BorderChars[1]);
        for (int i = 0; i < BorderLength; i++)
        {
            Border.Append(' ');
        }
        Border.Append(BorderChars[1]);

        Border.Append('\n');
        Border.Append(BorderChars[4]);
        for (int i = 0; i < BorderLength; i++)
        {
            Border.Append(BorderChars[0]);
        }
        Border.Append(BorderChars[5]);

        BorderSprite = Border.ToString();
    }

    public override void Render(TMPConsole.ConsoleBuffer Buffer)
    {
        Vector2 Location = Vector2.zero;
        if (Centered)
        {
            Location = Position - new Vector2(LeftMargin + 1 + Text.Text.Length / 2, 1);
        }
        else
        {
            Location = Position - new Vector2(LeftMargin + 1, 1);
        }
        Buffer.WriteAt(BorderSprite, Location.ToInt2());
        Text.Render(Buffer);  //Add code such that if text runs of the edge of border then it will get truncated
    }
}
