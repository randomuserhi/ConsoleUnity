using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;
using System.Runtime.CompilerServices;

public static class VectorExtension
{
    public static Vector2Int ToInt2(this Vector2 v)
    {
        return new Vector2Int((int)v.x, (int)v.y);
    }
}

public class TMPConsole : MonoBehaviour
{
    public enum TextResolution
    {
        Standard,
        Double
    }

    public bool DeveloperMode = false;
    public bool DeveloperClear = false;
    public bool DeveloperStandardResolution = true;

    public GameObject StandardResObject;
    public GameObject DoubleResObject;
    public GameObject InputObject;

    private Canvas CanvasComponent;
    private CanvasScaler CanvasScaler;
    private TMP_Text StandardRes;
    private TMP_Text DoubleRes;
    private TMP_InputField Input;

    public class ConsoleBuffer
    {
        public bool Changed { get; private set; } = true;
        private char[] Buffer;
        private StringBuilder Builder;

        public int Width;
        public int Height;

        private int ActualWidth;
        private int ActualHeight;

        public ConsoleBuffer(int Width, int Height)
        {
            Builder = new StringBuilder();

            this.Width = Width;
            this.Height = Height;
            ActualWidth = Width + 1;
            ActualHeight = Height;
            Buffer = new char[ActualWidth * this.Height];
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < ActualWidth; j++)
                {
                    if (j == ActualWidth - 1)
                        Buffer[i * ActualWidth + j] = '\n';
                    else
                        Buffer[i * ActualWidth + j] = ' ';
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int PosToIndex(Vector2Int Position)
        {
            return Position.y * ActualWidth + Position.x;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool InvalidPosition(Vector2Int Position)
        {
            return Position.x < 0 || Position.x >= Width || Position.y < 0 || Position.y >= Height;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool InvalidPosition(int Index)
        {
            int Y = Index / ActualWidth;
            int X = Index - ActualWidth * Y;
            return Y < 0 || X >= Width || Y < 0 || X >= Height;
        }

        public void RenderSprite(ConsoleSprite Sprite, Vector2Int Position, bool Centered = true)
        {
            Position -= new Vector2Int((Sprite.Width + Sprite.Left) / 2, (Sprite.Height + Sprite.Top) / 2);
            for (int i = 0; i < Sprite.Length; i++)
            {
                for (int j = 0; j < Sprite[i].Length; j++)
                {
                    if (Sprite[i][j] != Sprite.TransparencyColor)
                        Render(Sprite[i][j], Position + new Vector2Int(j, i));
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Render(char Text, Vector2Int Position)
        {
            if (InvalidPosition(Position)) return;
            Buffer[PosToIndex(Position)] = Text;
            Changed = true;
        }
        public void WriteAt(string Text, Vector2Int Position, bool Centered = false)
        {
            int RenderIndex = PosToIndex(Position);
            if (Centered)
                RenderIndex -= Text.Length / 2;
            for (int i = 0; i < Text.Length; RenderIndex++, i++)
                if (!InvalidPosition(RenderIndex))
                    Buffer[RenderIndex] = Text[i];
            Changed = true;
        }
        public void WriteAt(char[] Text, Vector2Int Position, bool Centered = false)
        {
            int RenderIndex = PosToIndex(Position);
            if (Centered)
                RenderIndex -= Text.Length / 2;
            for (int i = 0; i < Text.Length; RenderIndex++, i++)
                if (!InvalidPosition(RenderIndex))
                    Buffer[RenderIndex] = Text[i];
            Changed = true;
        }

        public void Clear()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Buffer[i * ActualWidth + j] = ' ';
                }
            }
        }

        public override string ToString()
        {
            Changed = false;
            Builder.Clear();
            Builder.Append(Buffer);
            return Builder.ToString();
        }
    }

    public ConsoleBuffer StandardBuffer;
    public ConsoleBuffer DoubleBuffer;

    private void Start()
    {
        CanvasComponent = GetComponent<Canvas>();
        CanvasScaler = GetComponent<CanvasScaler>();

        StandardRes = StandardResObject.GetComponent<TMP_Text>();
        DoubleRes = DoubleResObject.GetComponent<TMP_Text>();
        Input = InputObject.GetComponent<TMP_InputField>();
        Input.onFocusSelectAll = false;
        Input.text = string.Empty;

        StandardBuffer = new ConsoleBuffer(174, 47);
        DoubleBuffer = new ConsoleBuffer(240, 64);

        new ConsoleSprite(@"C:\Users\LenovoY720\Documents\Git\ConsoleUnity\Ollie's Adventure\Assets\Sprites\Test.txt", "TestSprite");
    }

    private void Update()
    {
        ConsoleSprite S = ConsoleSprite.Get("TestSprite");
        StandardBuffer.RenderSprite(S, new Vector2Int(StandardBuffer.Width / 2, StandardBuffer.Height / 2 - S.Height/2));
        DoubleBuffer.RenderSprite(S, new Vector2Int(DoubleBuffer.Width / 2, DoubleBuffer.Height / 2 + S.Height/2));

        if ((float)Screen.width / Screen.height > 1.77f)
            CanvasScaler.matchWidthOrHeight = 1;
        else
            CanvasScaler.matchWidthOrHeight = 0;

        if (StandardBuffer.Changed)
        {
            StandardRes.text = StandardBuffer.ToString();
            StandardRes.ForceMeshUpdate();

            StandardBuffer.Clear();
        }
        if (DoubleBuffer.Changed)
        {
            DoubleRes.text = DoubleBuffer.ToString();
            DoubleRes.ForceMeshUpdate();

            DoubleBuffer.Clear();
        }
    }

    public void Clear()
    {
        StandardBuffer.Clear();
        DoubleBuffer.Clear();
    }
    public void Clear(TextResolution Resolution)
    {
        switch (Resolution)
        {
            case TextResolution.Standard: StandardRes.text = string.Empty; break;
            case TextResolution.Double: DoubleRes.text = string.Empty; break;
        }
    }
}
