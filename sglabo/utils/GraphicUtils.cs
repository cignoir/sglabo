using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sglabo
{
    class GraphicUtils
    {
        private  const int SRCCOPY = 13369376;
        private  const int CAPTUREBLT = 1073741824;

        [DllImport("user32.dll")]
        private static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("gdi32.dll")]
        private static extern int BitBlt(IntPtr hDestDC,
            int x,
            int y,
            int nWidth,
            int nHeight,
            IntPtr hSrcDC,
            int xSrc,
            int ySrc,
            int dwRop);

        [DllImport("user32.dll")]
        private static extern IntPtr ReleaseDC(IntPtr hwnd, IntPtr hdc);

        /// <summary>
        /// プライマリスクリーンの画像を取得する
        /// </summary>
        /// <returns>プライマリスクリーンの画像</returns>
        public static Bitmap CaptureScreen()
        {
            //プライマリモニタのデバイスコンテキストを取得
            IntPtr disDC = GetDC(IntPtr.Zero);
            //Bitmapの作成
            Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                Screen.PrimaryScreen.Bounds.Height);
            //Graphicsの作成
            Graphics g = Graphics.FromImage(bmp);
            //Graphicsのデバイスコンテキストを取得
            IntPtr hDC = g.GetHdc();
            //Bitmapに画像をコピーする
            BitBlt(hDC, 0, 0, bmp.Width, bmp.Height,
                disDC, 0, 0, SRCCOPY);
            //解放
            g.ReleaseHdc(hDC);
            g.Dispose();
            ReleaseDC(IntPtr.Zero, disDC);

            return bmp;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern int GetWindowRect(IntPtr hwnd,
            ref  RECT lpRect);

        /// <summary>
        /// アクティブなウィンドウの画像を取得する
        /// </summary>
        /// <returns>アクティブなウィンドウの画像</returns>
        public static Bitmap CaptureActiveWindow()
        {
            //アクティブなウィンドウのデバイスコンテキストを取得
            IntPtr hWnd = GetForegroundWindow();
            if(hWnd == IntPtr.Zero) return null;

            IntPtr winDC = GetWindowDC(hWnd);
            //ウィンドウの大きさを取得
            RECT winRect = new RECT();
            GetWindowRect(hWnd, ref winRect);
            //Bitmapの作成
            Bitmap bmp = new Bitmap(winRect.right - winRect.left,
                winRect.bottom - winRect.top);
            //Graphicsの作成
            Graphics g = Graphics.FromImage(bmp);
            //Graphicsのデバイスコンテキストを取得
            IntPtr hDC = g.GetHdc();
            //Bitmapに画像をコピーする
            BitBlt(hDC, 0, 0, bmp.Width, bmp.Height,
                winDC, 0, 0, SRCCOPY);
            //解放
            g.ReleaseHdc(hDC);
            g.Dispose();
            ReleaseDC(hWnd, winDC);

            return bmp;
        }

        public static int GenerateUniqueCode(Bitmap bmp)
        {
            BitmapData bmpdata = bmp.LockBits(
                    new Rectangle(0, 0, bmp.Width, bmp.Height),
                    ImageLockMode.ReadWrite,
                    PixelFormat.Format32bppArgb
                );

            byte[] ba = new byte[bmp.Width * bmp.Height * 4];
            Marshal.Copy(bmpdata.Scan0, ba, 0, ba.Length);
            int pixsize = bmp.Width * bmp.Height * 4;

            var parentCode = 0;
            for(int i = 0; i < ba.Length; i += 4)
            {
                var b = ba[i + 0];
                var g = ba[i + 1];
                var r = ba[i + 2];
                var a = ba[i + 3];

                var childCode = (b * g * r * a) / 4;
                parentCode = Math.Abs((parentCode + childCode) / 2);
            }

            Marshal.Copy(ba, 0, bmpdata.Scan0, ba.Length);
            bmp.UnlockBits(bmpdata);
            bmp.Dispose();

            return parentCode;
        }

        public static void Activate(IntPtr hWnd)
        {
            if(hWnd == IntPtr.Zero)
            {
                return;
            }

            if(Win32API.IsIconic(hWnd))
            {
                //SendMessage(hWnd, WM_SYSCOMMAND, SC_MAXIMIZE, 0);
                //ShowWindowAsync(hWnd, SW_RESTORE);
                //SetFocus(hWnd);
                Win32API.SwitchToThisWindow(hWnd, true);
            }

            IntPtr forehWnd = Win32API.GetForegroundWindow();
            if(forehWnd == hWnd)
            {
                return;
            }
            uint foreThread = Win32API.GetWindowThreadProcessId(forehWnd, IntPtr.Zero);
            uint thisThread = Win32API.GetCurrentThreadId();
            uint timeout = 200000;
            if(foreThread != thisThread)
            {
                Win32API.SystemParametersInfoGet(Win32API.SPI_GETFOREGROUNDLOCKTIMEOUT, 0, ref timeout, 0);
                Win32API.SystemParametersInfoSet(Win32API.SPI_SETFOREGROUNDLOCKTIMEOUT, 0, 0, 0);

                Win32API.AttachThreadInput(thisThread, foreThread, true);
            }

            Win32API.SetForegroundWindow(hWnd);
            Win32API.SetWindowPos(hWnd, Win32API.HWND_TOP, 0, 0, 0, 0,
                Win32API.SWP_NOMOVE | Win32API.SWP_NOSIZE | Win32API.SWP_SHOWWINDOW | Win32API.SWP_ASYNCWINDOWPOS);
            Win32API.BringWindowToTop(hWnd);
            //ShowWindowAsync(hWnd, SW_SHOW);
            Win32API.SetFocus(hWnd);
            Win32API.SwitchToThisWindow(hWnd, true);

            if(foreThread != thisThread)
            {
                Win32API.SystemParametersInfoSet(Win32API.SPI_SETFOREGROUNDLOCKTIMEOUT, 0, timeout, 0);
                Win32API.AttachThreadInput(thisThread, foreThread, false);
            }
        }
    }
}
