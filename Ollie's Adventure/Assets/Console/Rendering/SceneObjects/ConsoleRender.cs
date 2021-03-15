using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ConsoleRender : ConsoleSceneObject
{
    public bool Centered;
    private float FrameDelay;
    public int Frame;
    public bool Animated;
    public ConsoleSprite Sprite;
    public ConsoleRender(ConsoleSprite Sprite, Vector2 Position, int Frame = 0, bool Centered = false, bool Animated = false) : base(Position)
    {
        this.Animated = Animated;
        this.Frame = Frame;
        this.Centered = Centered;
        this.Sprite = Sprite;
    }

    public override void Render(TMPConsole.ConsoleBuffer Buffer)
    {
        Buffer.RenderSprite(Sprite, Position.ToInt2(), Frame, Centered);
    }

    public override void Update()
    {
        if (Animated)
        {
            FrameDelay += Time.deltaTime;
            while (FrameDelay > Sprite.FrameDelay)
            {
                FrameDelay -= Sprite.FrameDelay;

                Frame++;
                if (Frame >= Sprite.FrameCount)
                    Frame = 0;
            }
        }
    }
}
