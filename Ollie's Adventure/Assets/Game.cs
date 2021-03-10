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

        SceneStandard.Add(new ConsoleBorder("Beaned lmao", new Vector2(10, 10), Centered:true));

        /*while (!Token.IsCancellationRequested) //while true loop with cancel functionality
        {
            Debug.Log("Bruh");
        }*/

         
    }
}