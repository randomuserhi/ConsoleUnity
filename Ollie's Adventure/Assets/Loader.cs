using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Loader
{
    public static void LoadAllSprites()
    {
        new ConsoleSprite(@"C:\Users\User\Documents\Git\ConsoleUnity\Ollie's Adventure\Assets\Sprites\Test.txt", "TestSprite");
    }
}
