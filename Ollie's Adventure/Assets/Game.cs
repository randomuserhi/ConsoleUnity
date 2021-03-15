using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using UnityEngine;

public class Game
{
    private static TMPConsole Console;
    private static ConsoleScene SceneStandard = new ConsoleScene();
    private static ConsoleScene SceneDouble = new ConsoleScene();

    public static void Start(CancellationToken Token)
    {
        Token.ThrowIfCancellationRequested();

        Console = TextEngine.Console;
        Console.StandardBuffer.Scene = SceneStandard;
        Console.DoubleBuffer.Scene = SceneDouble;

        SceneDouble.Add(new ConsoleRender(ConsoleSprite.Get("Cat_Sleeping"), new Vector2(Console.StandardBuffer.Width / 2, Console.StandardBuffer.Height / 2), Animated:true));

        /*while (!Token.IsCancellationRequested) //while true loop with cancel functionality
        {
            Debug.Log("Bruh");
        }*/
    }
}