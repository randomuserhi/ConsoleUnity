using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;

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

        private bool InvalidPosition(Vector2Int Position)
        {
            return Position.x < 0 || Position.x >= Width || Position.y < 0 || Position.y >= Height;
        }

        public void Render(char Text, Vector2Int Position)
        {
            if (InvalidPosition(Position)) return;
            Buffer[Position.y * ActualWidth + Position.x] = Text;
            Changed = true;
        }
        public void WriteAt(string Text, Vector2Int Position, bool Centered = false)
        {
            if (InvalidPosition(Position)) return;
            int RenderIndex = Position.y * ActualWidth + Position.x;
            if (Centered)
                RenderIndex -= Text.Length / 2;
            for (int i = 0; i < Text.Length; RenderIndex++, i++)
                Buffer[RenderIndex] = Text[i];
            Changed = true;
        }
        public void WriteAt(char[] Text, Vector2Int Position, bool Centered = false)
        {
            if (InvalidPosition(Position)) return;
            int RenderIndex = Position.y * ActualWidth + Position.x;
            if (Centered)
                RenderIndex -= Text.Length / 2;
            for (int i = 0; i < Text.Length; RenderIndex++, i++)
                Buffer[RenderIndex] = Text[i];
            Changed = true;
        }

        public void Clear()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Buffer[i * ActualWidth + j] = 'a';
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
        StandardRes = StandardResObject.GetComponent<TMP_Text>();
        DoubleRes = DoubleResObject.GetComponent<TMP_Text>();
        Input = InputObject.GetComponent<TMP_InputField>();
        Input.onFocusSelectAll = false;

        StandardBuffer = new ConsoleBuffer(133, 32);
        DoubleBuffer = new ConsoleBuffer(266, 64);
    }

    private void Update()
    {
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
