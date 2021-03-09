using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class TextEngine : MonoBehaviour
{
    public static TMPConsole Console;
    private IEnumerator MainRoutine;

    private void Start()
    {
        //Get Console
        Console = GameObject.FindGameObjectWithTag("TMPConsole").GetComponent<TMPConsole>();

        Loader.LoadAllSprites(); //Load all sprites

        MainRoutine = Game.Start();
    }

    private void RenderLoop()
    {

    }

    private void Update()
    {
        MainRoutine.MoveNext();

        RenderLoop();
        Console.Render();
    }

    private void OnApplicationQuit()
    {
    }
}
