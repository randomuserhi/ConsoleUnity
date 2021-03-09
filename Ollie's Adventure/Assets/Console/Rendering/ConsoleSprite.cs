using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using UnityEngine;

public class ConsoleSprite
{
    private const int HeaderSize = 5;

    public static Dictionary<string, int> SpriteDict = new Dictionary<string, int>();
    public static List<ConsoleSprite> SpriteAtlas = new List<ConsoleSprite>();

    public readonly char TransparencyColor = '█';
    public readonly int Height;
    public readonly int Width;
    public readonly int Left;
    public readonly int Top;
    public int FrameCount = 1;

    private string[] Characters;
    public string this[int i, int Frame]
    {
        get { if (Frame > FrameCount) Frame = FrameCount - 1; return Characters[Frame * Height + i + HeaderSize]; }
    }
    public static ConsoleSprite Get(string Name)
    {
        return SpriteAtlas[SpriteDict[Name]];
    }
    public int Length { get { return Characters.Length - HeaderSize; } }

    public ConsoleSprite(string TexturePath, string SpriteName)
    {
        Characters = File.ReadAllLines(TexturePath);

        //Header setup
        Height = int.Parse(Characters[0]);
        Width = int.Parse(Characters[1]);
        Left = int.Parse(Characters[2]);
        Top = int.Parse(Characters[3]);
        TransparencyColor = Characters[4][0];

        if (SpriteName != string.Empty)
            SpriteDict.Add(SpriteName, SpriteAtlas.Count);
        SpriteAtlas.Add(this);
    }
}
