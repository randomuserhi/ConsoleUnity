using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Game
{
    public static IEnumerator Start()
    {
        do{ TextEngine.Console.ReadRoutine.MoveNext(); yield return null; } while (TextEngine.Console.ReadRoutine.Current == null); //Console ReadLine code
        Debug.Log(TextEngine.Console.ReadRoutine.Current);
    }
}