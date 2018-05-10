using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace EvidenceAppli
{
    public class CaptureProcess
    {
        #region field

        private const int SRCCOPY = 13369376;
        private const int CAPTUREBLT = 1073741824;

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

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
        /// GDI関連で利用するWindowsAPI
        /// </summary>
        [DllImport("user32.dll")]
        private static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        private static extern int GetWindowRect(IntPtr hwnd, ref RECT lpRect);

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [System.Runtime.InteropServices.DllImport("User32.dll")]
        private extern static bool PrintWindow(IntPtr hwnd, IntPtr hDC, uint nFlags);

        #endregion field

        #region Consturctor

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CaptureProcess()
        {
        }

        #endregion Consturctor

        #region Method

#if true

        /// <summary>
        /// アクティブなウィンドウの画像を取得する(PrintWindow版)
        /// </summary>
        /// <returns>アクティブなウィンドウの画像</returns>
        public Bitmap CaptureActiveWindow(IntPtr hWnd)
        {
            // キャプチャ対象をアクティブにする
            SetForegroundWindow(hWnd);
            // XP
            //IntPtr winDC = GetWindowDC(hWnd);
            // 7以降
            IntPtr winDC = GetDC(hWnd);
            //ウィンドウの大きさを取得
            RECT winRect = new RECT();
            GetWindowRect(hWnd, ref winRect);
            // 以下はdobon.netのほとんどパクリ
            Bitmap img = new Bitmap(winRect.right - winRect.left, winRect.bottom - winRect.top, PixelFormat.Format32bppArgb);
            Graphics memg = Graphics.FromImage(img);
            IntPtr dc = memg.GetHdc();
            try
            {
                PrintWindow(hWnd, dc, 0);
            }
            finally
            {
                memg.ReleaseHdc(dc);
                memg.Dispose();
                ReleaseDC(hWnd, winDC);
            }
            return img;
        }

#else

#if true
        /// <summary>
        /// アクティブなウィンドウの画像を取得する(Win32API版)
        /// </summary>
        /// <returns>アクティブなウィンドウの画像</returns>
        public Bitmap CaptureActiveWindow(IntPtr hWnd)
        {
            // キャプチャ対象をアクティブにする
            SetForegroundWindow(hWnd);

            // XP
            IntPtr winDC = GetWindowDC(hWnd);
            // 7以降
            //IntPtr winDC = GetDC(hWnd);

            //ウィンドウの大きさを取得
            RECT winRect = new RECT();
            GetWindowRect(hWnd, ref winRect);
            //Bitmapの作成
            Bitmap bmp = new Bitmap(winRect.right - winRect.left, winRect.bottom - winRect.top);
            //Graphicsの作成
            using (Graphics g = Graphics.FromImage(bmp))
            {
                //Graphicsのデバイスコンテキストを取得
                IntPtr hDC = g.GetHdc();
                //Bitmapに画像をコピーする
                BitBlt(hDC, 0, 0, bmp.Width, bmp.Height, winDC, 0, 0, SRCCOPY | CAPTUREBLT);
                //解放
                g.ReleaseHdc(hDC);
                g.Dispose();
            }
            ReleaseDC(hWnd, winDC);
            return bmp;
        }
#else
        /// <summary>
        /// アクティブなウィンドウの画像を取得する(CopyFromScreen版)
        /// </summary>
        /// <returns>アクティブなウィンドウの画像</returns>
        //public Bitmap CaptureActiveWindow(IntPtr hWnd)
        //{
        //    // ウィンドウの大きさを取得
        //    RECT wRect = new RECT();
        //    GetWindowRect(hWnd, ref wRect);

        //    int width = wRect.right - wRect.left;
        //    int height = wRect.bottom - wRect.top;

        //    Bitmap bmp = new Bitmap(width, height);
        //    using (Graphics g = Graphics.FromImage(bmp))
        //    {
        //        g.CopyFromScreen(new Point(wRect.left, wRect.top), new Point(0, 0), bmp.Size);
        //    }

        //    return bmp;

        //}

#endif
#endif

        /// <summary>
        /// 矩形範囲キャプチャ
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="ep"></param>
        /// <param name="bmp"></param>
        /// <returns></returns>
        public Bitmap CaptureRectOnScreen(Point sp, Point ep, Bitmap bmp)
        {
            /// クライアント座標からサイズ生成
            Size size = new Size(System.Math.Abs(sp.X - ep.X), System.Math.Abs(sp.Y - ep.Y));
            if (size.Equals(new Size(0, 0)))
            {
                return null;
            }
            Rectangle rect = new Rectangle(sp, size);
            /// 切り取り
            Bitmap bmpNew = bmp.Clone(rect, bmp.PixelFormat);
            bmp.Dispose();
            return bmpNew;
        }

        /// <summary>
        /// 全画面キャプチャ
        /// </summary>
        /// <returns></returns>
        public Bitmap CaptureFullScreen()
        {
            Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                //画面全体をコピーする
                g.CopyFromScreen(new Point(0, 0), new Point(0, 0), bmp.Size);
            }
            return bmp;
        }

        #endregion Method
    }
}