using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ConsoleRender : ConsoleSceneObject
{
    public bool Centered;
    public int Frame;
    public ConsoleSprite Sprite;
    public ConsoleRender(ConsoleSprite Sprite, Vector2 Position, int Frame = 0, bool Centered = false) : base(Position)
    {
        this.Frame = Frame;
        this.Centered = Centered;
        this.Sprite = Sprite;
    }

    public override void Render(TMPConsole.ConsoleBuffer Buffer)
    {
        Buffer.RenderSprite(Sprite, Position.ToInt2(), Frame, Centered);
    }
}
