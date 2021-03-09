using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class TextEngine : MonoBehaviour
{
    public static TMPConsole Console;
    private Task MainThread;
    private CancellationTokenSource MainThreadCancelToken = new CancellationTokenSource();

    private void Start()
    {
        //Get Console
        Console = GameObject.FindGameObjectWithTag("TMPConsole").GetComponent<TMPConsole>();

        Loader.LoadAllSprites(); //Load all sprites

        MainThread = Task.Run(() => { Game.Start(MainThreadCancelToken.Token); });
    }

    private void RenderLoop()
    {

    }

    private void Update()
    {
        RenderLoop();
        Console.Render();
    }

    private void OnApplicationQuit()
    {
        MainThreadCancelToken.Cancel();
        MainThreadCancelToken.Dispose();
        if (MainThread != null)
            MainThread.Dispose();
    }
}
