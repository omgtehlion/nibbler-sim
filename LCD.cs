using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Simulator
{
    public class LCD
    {
        bool m_4bitMode;
        int m_4bitPhase;
        byte m_4bitNibble;
        int Lines = 4;
        const int Scale = 4;
        IntPtr Lcd;
        int Width;
        int Height;
        Bitmap Bmp;

        public LCD() => Reset();

        public void SetLines(int lines)
        {
            Lines = lines;
            Reset();
        }

        public void Reset()
        {
            m_4bitMode = false;
            m_4bitPhase = 0;
            if (Lcd != IntPtr.Zero)
                vrEmuLcdDestroy(Lcd);
            Lcd = vrEmuLcdNew(16, Lines, 0);
            vrEmuLcdNumPixels(Lcd, out Width, out Height);
            Bmp = new Bitmap(Width * Scale, Height * Scale, PixelFormat.Format24bppRgb);
        }

        static readonly SolidBrush[] Brushes = new[] {
            new SolidBrush(Color.FromArgb(114, 186, 0)),
            new SolidBrush(Color.FromArgb(6, 30, 0)),
        };

        public Image Render()
        {
            vrEmuLcdUpdatePixels(Lcd);
            using (var gr = Graphics.FromImage(Bmp)) {
                gr.Clear(Color.FromArgb(125, 190, 0));
                for (var y = 0; y < Height; y++)
                    for (var x = 0; x < Width; x++) {
                        var px = vrEmuLcdPixelState(Lcd, x, y);
                        if (px >= 0)
                            gr.FillRectangle(Brushes[px], x * Scale, y * Scale, Scale - 1, Scale - 1);
                    }
            }
            return Bmp;
        }

        void Write(uint port, int value)
        {
            if (port == 0) {
                if ((value & 0xE0) == 0x20) {
                    m_4bitMode = (value & 0x10) == 0;
                    if (m_4bitMode)
                        return;
                }
                vrEmuLcdSendCommand(Lcd, (byte)value);
            } else {
                vrEmuLcdWriteByte(Lcd, (byte)value);
            }
        }

        public void WriteNibble(uint port, byte value)
        {
            if (m_4bitMode) {
                if (m_4bitPhase == 0)
                    m_4bitNibble = value;
                else
                    Write(port, m_4bitNibble * 16 + value);
                m_4bitPhase ^= 1;
            } else {
                Write(port, value * 16);
            }
        }

        [DllImport("vrEmuLcd.dll")]
        static extern IntPtr vrEmuLcdNew(int width, int height, int rom);

        [DllImport("vrEmuLcd.dll")]
        static extern void vrEmuLcdDestroy(IntPtr lcd);

        [DllImport("vrEmuLcd.dll")]
        static extern void vrEmuLcdSendCommand(IntPtr lcd, byte data);

        [DllImport("vrEmuLcd.dll")]
        static extern void vrEmuLcdWriteByte(IntPtr lcd, byte data);

        [DllImport("vrEmuLcd.dll")]
        static extern void vrEmuLcdUpdatePixels(IntPtr lcd);

        [DllImport("vrEmuLcd.dll")]
        static extern void vrEmuLcdNumPixels(IntPtr lcd, out int width, out int height);

        [DllImport("vrEmuLcd.dll")]
        static extern sbyte vrEmuLcdPixelState(IntPtr lcd, int x, int y);
    }
}
