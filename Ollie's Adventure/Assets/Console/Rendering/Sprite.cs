using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using UnityEngine;

public class ConsoleSprite
{
    public static Dictionary<string, int> SpriteDict = new Dictionary<string, int>();
    public static List<ConsoleSprite> SpriteAtlas = new List<ConsoleSprite>();

    public char TransparencyColor = '█';
    public int FrameHeight;
    public int Frame = 0;

    private string[] Characters;
    public string this[int i]
    {
        get { return Characters[i + 2]; }
    }
    public static ConsoleSprite Get(string Name)
    {
        return SpriteAtlas[SpriteDict[Name]];
    }
    public int Length { get { return Characters.Length - 2; } }

    public ConsoleSprite(string TexturePath, string SpriteName)
    {
        Characters = File.ReadAllLines(TexturePath);
        FrameHeight = int.Parse(Characters[0]);
        TransparencyColor = Characters[1][0];
        if (SpriteName != string.Empty)
            SpriteDict.Add(SpriteName, SpriteAtlas.Count);
        SpriteAtlas.Add(this);
    }
}
