using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace EvidenceAppli
{
    public partial class MainForm : System.Windows.Forms.Form
    {
        #region field

        private enum BUTTOM
        {
            BSELECT, // 選択ボタン
            BCLEAR, // クリアボタン
            RFORM, // フォームラジオボタン
            RRECT, // 矩形ラジオボタン
            BCAPTURE, // キャプチャボタン
            BSAVE, // 保存して終了ボタン
            BCLOSE, // 保存せず終了ボタン
        }

        /// <summary>
        /// Window(フォーム)関連で利用するWindowsAPI
        /// </summary>
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern IntPtr WindowFromPoint(System.Drawing.Point point);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool GetCursorPos(ref System.Drawing.Point lpPoint);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int GetClassName(
          IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int GetWindowText(
          IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetWindowPos(IntPtr hWnd,
            int hWndInsertAfter, int x, int y, int cx, int cy, int uFlags);

        [DllImport("USER32.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr GetWindow(IntPtr hWnd, int uCmd);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetActiveWindow(IntPtr hWnd);

        private const int GW_HWNDNEXT = 2;
        private const uint WM_GETTEXT = 13;
        private const int START = 0;

        /// <summary>
        ///
        /// </summary>
        static private IntPtr hWnd;

        /// <summary>
        ///
        /// </summary>
        static private ScreenForm m_FullScreen = null;

        /// <summary>
        ///  開始点
        /// </summary>
        private System.Drawing.Point m_sp;

        /// <summary>
        /// 終了点
        /// </summary>
        private System.Drawing.Point m_ep;

        /// <summary>
        /// ドラッグ中フラグ
        /// </summary>
        private bool m_isDragging = false;

        /// <summary>
        ///
        /// </summary>
        private Bitmap m_FullScreenbmp = null;

        /// <summary>
        /// 作業フォルダ
        /// </summary>
        static private string tmpFolder = null;

        /// <summary>
        /// コメント残すか？フラグ
        /// </summary>
        private bool m_isNoClearComment = false;

        /// <summary>
        /// Excel挿入先ポインタ
        /// </summary>
        private int m_currentRow = 1;

        /// <summary>
        ///
        /// </summary>
        private int m_preno = -1;

        /// <summary>
        /// 画像番号
        /// </summary>
        private int m_gazou_num = 0;

        /// <summary>
        /// EvidencdAppli画像保存用一時作業フォルダ
        /// </summary>
        private const string TMP = @"\EvidenceAppliTMP";

        /// <summary>
        /// 「シートID」と[シート名]」の組の配列
        private Dictionary<string, string> m_SheetNo = new Dictionary<string, string>();

        /// <summary>
        ///
        /// </summary>
        private static MouseHookAPI.HookProcedureDelegate mouse_proc;

        /// <summary>
        /// 状態
        /// </summary>
        private Status m_st = Status.INIT;

        /// <summary>
        /// ファイルマネージャーとの関連
        /// </summary>
        private FileManager m_FileManager = null;

        /// <summary>
        /// キャプチャ処理との関連
        /// </summary>
        private CaptureProcess m_CaptureProcess = null;

        /// <summary>
        /// イメージファイルとの関連
        /// </summary>
        private ImageFile m_ImageFile = null;

        /// <summary>
        /// エクセルファイルプロセスとの関連
        /// </summary>
        private ExcelFileProcess m_ExcelFileProcess = null;

        #endregion field

        #region Consturctor

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            tmpFolder = System.IO.Directory.GetCurrentDirectory() + TMP;
            m_FileManager = new FileManager(tmpFolder);
            m_CaptureProcess = new CaptureProcess();
            m_ImageFile = new ImageFile();
            m_ExcelFileProcess = new ExcelFileProcess(tmpFolder); // Excel起動

            m_ExcelFileProcess.OpenBook(); // Excelブックオープン
            m_st = Status.INIT; //　状態は初期状態に
            SetButtonStatus(m_st);
            captuerTargetInfo.Text = ""; // 情報画面クリア
            sheetName_textBox.Text = ""; // シート名欄クリア
            comboBox1.Text = "png";
            m_ExcelFileProcess.GazounoHozonKeisiki = comboBox1.Text;
            m_FileManager.GazounoHozonKeisiki = comboBox1.Text;
        }

        #endregion Consturctor

        #region Method

        // 操作性の問題から廃止
        // Form#Show()でアクティブにしない
        //protected override bool ShowWithoutActivation { get { return true; } }

        private void SetButtonStatus(Status st)
        {
            // ボタン活性非活性用状態遷移テーブル
            bool[,] bstTable = new bool[,]{
                //  B選択,Bクリア,Rフォーム,R矩形,Bキャプ,B保存して終了,B保存せず終了
                {   false, true,false,false,false,false, true    }, // フォーム選択開始状態(フォームラジオボタンが押され、かつ選択開始ボタンが押された状態)
                {   true, false, true, true,false, true, true    }, // フォーム選択解除状態(フォームラジオボタンが押され、かつクリアボタンが押された状態)
                {   false, true,false,false,false,false, true    }, // 矩形選択開始状態(矩形ラジオボタンが押され、かつ選択開始ボタンが押された状態)
                {   true, false, true, true,false,false, true    }, // 矩形選択解除状態(矩形ラジオボタンが押され、かつクリアボタンが押された状態)
                {   false, true,false,false, true, true, true    }, // フォームキャプチャ状態
                {   false, true,false,false, true, true, true    }, // 矩形キャプチャ状態

                {   false, true,false,false, true,false, true    }, // フォーム画像選択中状態
                {   false, true,false,false, true,false, true    }, // 矩形画像選択中状態
                {   true, false, true, true,false,false, true    }, // 初期状態

                {   false,false,false,false,false,false,false    }, // ダミー(保存しないで終了はボタン活性非活性には関係ない)
                {   false,false,false,false,false,false,false    }, // ダミー(保存して終了はボタン活性非活性には関係ないの)
                };
            // 選択ボタン
            select_button.Enabled = bstTable[(int)st, (int)BUTTOM.BSELECT];
            // クリアボタン
            clear_button.Enabled = bstTable[(int)st, (int)BUTTOM.BCLEAR];
            // フォームラジオボタン
            form_radioButton.Enabled = bstTable[(int)st, (int)BUTTOM.RFORM];
            // 矩形ラジオボタン
            rect_radioButton.Enabled = bstTable[(int)st, (int)BUTTOM.RRECT];
            // フォームキャプチャボタン
            capture_button.Enabled = bstTable[(int)st, (int)BUTTOM.BCAPTURE];
            // 保存して終了ボタン
            save_button.Enabled = bstTable[(int)st, (int)BUTTOM.BSAVE];
            // 保存せず終了ボタン
            close_button.Enabled = bstTable[(int)st, (int)BUTTOM.BCLOSE];
        }

        /// <summary>
        /// 選択ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void select_button_Click(object sender, EventArgs e)
        {
            if (form_radioButton.Checked)
            {
                // フォームがチェックされているなら
                m_st = Status.FSELECT;
            }
            else
            {
                // 矩形がチェックされているなら
                m_st = Status.RSELECT;
                this.Opacity = 0;

                // EvidencdAppliの一枚下のFormがキャプチャ対象なので、見つけてアクティブにする
                IntPtr nexthWnd = GetWindow(hWnd, GW_HWNDNEXT);
                SetForegroundWindow(nexthWnd);
                SetActiveWindow(nexthWnd);

                m_FullScreenbmp = m_CaptureProcess.CaptureFullScreen();

                m_FullScreen = new ScreenForm();
                m_FullScreen.Owner = this; // べつに親がなくてもいいけど
                m_FullScreen.BackgroundImage = m_FullScreenbmp;
                m_FullScreen.TopMost = true;
                m_FullScreen.Show();
            }
            SetButtonStatus(m_st);
            captuerTargetInfo.Text = "";
        }

        /// <summary>
        /// 文字列って空だよね？
        /// </summary>
        /// <param name="mojiretu"></param>
        /// <returns></returns>
        private bool isStringNull(string mojiretu)
        {
            return mojiretu.Length == 0 ? true : false;
        }

        /// <summary>
        /// キャプチャボタンクリック
        /// </summary>
        private void capture_button_Click(object sender, System.EventArgs e)
        {
            // シート番号を更新して画面にセットする
            if (sheetgoto_radioButton.Checked)
            {
                m_FileManager.IncCount();
            }

            string sheetName = MakeSheetName();
            //sheetName_textBox.Text = "";

            if (douitu_radioButton.Checked)
            {
                // 同一シート内に画像を並べる場合は、画像番号をキャプチャごとにインクリメント
                ++m_gazou_num;
            }
            else
            {
                // シートごとに画像を一枚はる場合は、画像番号を1に固定
                m_gazou_num = 1;
            }
            string gazouName = sheetName + @"-" + m_gazou_num.ToString();

            if ((m_st == Status.HOLD) || (m_st == Status.CAPTURE))
            {
                // 画像をキャプチャする
                Bitmap bmp = m_CaptureProcess.CaptureActiveWindow(hWnd);
                // ファイル番号を生成する
                // 保存する
                m_FileManager.SaveImageFile(bmp, gazouName);
                // Excelファイル作成
                MakeExcelFile(m_FileManager.Count, sheetName, gazouName);
                // 状態をキャプチャ状態にする
                m_st = Status.CAPTURE;
                SetButtonStatus(m_st);
            }
            else if ((m_st == Status.RHOLD) || (m_st == Status.RCAPTURE))
            {
                // Zオーダー上から２番目をキャプチャ対象とみなす。写すときはアクティブにしたいのでそうする。
                SetForegroundWindow(GetWindow(hWnd, GW_HWNDNEXT));
                SetActiveWindow(GetWindow(hWnd, GW_HWNDNEXT));
                // キャプチャしたとき本アプリが映っちゃうので、不可視にする （this.hide()とかthis.Visible=falseで消そうとすると、アニメーションを伴うのでうっすら残る）
                this.Opacity = 0;
                // 画像をキャプチャする
                Bitmap fsbmp = m_CaptureProcess.CaptureFullScreen();
                // 本アプリを可視にする
                this.Opacity = 1;
                System.Drawing.Point start = new System.Drawing.Point();
                System.Drawing.Point end = new System.Drawing.Point();
                GetRegion(m_sp, m_ep, ref start, ref end);
                Bitmap bmp = m_CaptureProcess.CaptureRectOnScreen(start, end, fsbmp);
                if (bmp == null)
                {
                    m_st = Status.RCANCEL;
                    return;
                }
                // 保存する
                m_FileManager.SaveImageFile(bmp, gazouName);
                // Excelファイル作成
                MakeExcelFile(m_FileManager.Count, sheetName, gazouName);
                // 状態をキャプチャ状態にする
                m_st = Status.RCAPTURE;
                SetButtonStatus(m_st);
                if (bmp != null)
                {
                    bmp.Dispose();
                }
            }
            // 一度、常時最前面した後に解除することで、最前面にする
            this.TopMost = true;
            this.Activate();
            this.TopMost = false;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        private string MakeSheetName()
        {
            string sheetName = sheetName_textBox.Text;
            if (isStringNull(sheetName_textBox.Text))
            {
                // シート名を空にした場合は"@"を突っ込む
                sheetName = "@";
            }
            /// シートIDとシート名の組み合わせを作る
            if (!m_SheetNo.ContainsKey(m_FileManager.Count.ToString()))
            {
                // シートIDがまだ使われていない場合は、シート名を追加するして以降つかう。
                if (m_SheetNo.ContainsValue(sheetName))
                {
                    // 同じシート名が存在していたら、名前のうしろに@を振る
                    // 同じ数の@見当たらなくなるまで続ける
                    do
                    {
                        sheetName += @"@";
                    } while (m_SheetNo.ContainsValue(sheetName));
                }
                m_SheetNo.Add(m_FileManager.Count.ToString(), sheetName);
            }
            else
            {
                // シートIDがもう使われていた場合は、追加を行わず、すでにあるシートIDのシート名を使う。
                sheetName = m_SheetNo[m_FileManager.Count.ToString()];
            }
            // シート名TextBoxに書き込む
            sheetName_textBox.Text = sheetName;
            return sheetName;
        }

        /// <summary>
        /// クリアボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clear_button_Click(object sender, System.EventArgs e)
        {
            bool isSelect = ((m_st == Status.FSELECT) || (m_st == Status.RSELECT)); // 選択開始状態か？
            bool isHold = ((m_st == Status.HOLD) || (m_st == Status.RHOLD)); // 選択中状態か？
            bool isCaptuer = ((m_st == Status.CAPTURE) || (m_st == Status.RCAPTURE)); // キャプチャ状態か？
            if (isSelect || isHold || isCaptuer)
            {
                if (form_radioButton.Checked)
                {
                    // フォーム選択解除状態にする
                    m_st = Status.FCANCEL;
                }
                else
                {
                    // 矩形選択解除状態にする
                    m_st = Status.RCANCEL;
                }
            }
            captuerTargetInfo.Text = "";
            SetButtonStatus(m_st);

            // 最前面に持ってくるための処理（最前面にしっぱなしにはしない）
            this.TopMost = true;
            this.Activate();
            this.TopMost = false;
        }

        /// <summary>
        /// 保存せず終了ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void close_button_Click(object sender, EventArgs e)
        {
            m_st = Status.CLOSE;
            Console.WriteLine("保存しないボタンが押されました。");
            /// COM解放
            m_ExcelFileProcess.CloseBook();
            /// EvidencdAppli解放
            this.Close();
        }

        /// <summary>
        /// 保存して終了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void save_button_Click(object sender, EventArgs e)
        {
            Status prest = m_st;
            m_st = Status.SCLOSE;
            Console.WriteLine("保存ボタンが押されました。");
            /// ダイアログ表示
            if (DispDialog("保存先のファイルを選択してください"))
            {
                // 保存先に画像フォルダをコピー
                m_FileManager.CopyFolder();
                /// Excel保存
                m_ExcelFileProcess.Save();
                /// COM解放
                m_ExcelFileProcess.CloseBook();
                /// EvidencdAppli解放
                this.Close();
            }
            else
            {
                m_st = prest;
            }
        }

        /// <summary>
        /// コメント残すチェックボックス
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                m_isNoClearComment = true;
            }
            else
            {
                m_isNoClearComment = false;
                // コメント欄クリア
                Comment.ResetText();
            }
        }

        /// <summary>
        /// Excelファイル作成
        /// </summary>
        /// <param name="no"></param>
        private void MakeExcelFile(int no, string sheetname, string gazouname)
        {
            // 文字列解析はここでやる予定でした・・・

            if (m_preno != no)
            {
                // Excel処理
                m_ExcelFileProcess.AddSheet(no, sheetname);
                // 同一シート内で挿入先のCellを初期化
                m_currentRow = 1;
            }
            m_preno = no;

            // コメント欄追加
            int i = 0;
            for (; i < Comment.Lines.Length; i++)
            {
                m_ExcelFileProcess.SetMojiretu(no, i + m_currentRow, 1, Comment.Lines[i]);
            }
            if (m_isNoClearComment)
            {
                // コメント欄クリア
                Comment.ResetText();
            }
            // キャプチャ画像追加
            int picRow = m_ExcelFileProcess.SetPictuer(no, sheetname, gazouname, i + m_currentRow, 1);
            m_currentRow = picRow + i + 1;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            // グローバルフック
            using (Process process = Process.GetCurrentProcess())
            using (ProcessModule module = process.MainModule)
            {
                // フックプロシージャを登録
                MouseHookInfo.hHook = MouseHookAPI.SetWindowsHookEx(
                MouseHookInfo.WH_MOUSE_LL,
                mouse_proc = new MouseHookAPI.HookProcedureDelegate(MouseHookProc),
                MouseHookAPI.GetModuleHandle(module.ModuleName),
                0);
            }
            if (MouseHookInfo.hHook == IntPtr.Zero)
                MessageBox.Show("SetWindowsHookEx Failed.");

            // EvidenceAppliを最前面に持ってくるための処理（最前面にしっぱなしにはしない）
            this.TopMost = true;
            this.Activate();
            this.TopMost = false;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        public IntPtr MouseHookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                // 選択状態のみ受け入れる
                MouseHookInfo.MouseHookStruct MyMouseHookStruct = (MouseHookInfo.MouseHookStruct)Marshal.PtrToStructure(lParam, typeof(MouseHookInfo.MouseHookStruct));
                switch ((int)wParam)
                {
                    case MouseHookInfo.WM_LBUTTONDOWN:
                        {
                            if (m_st == Status.FSELECT)
                            {
                                // フォームの場合
                                // EvidencdAppliを最前面にする
                                this.TopMost = true;
                            }
                            else if ((m_st == Status.RSELECT) || (m_st == Status.RCANCEL))
                            {
                                // 矩形の場合

                                // マウス座標の取得
                                System.Drawing.Point p = new System.Drawing.Point();
                                GetCursorPos(ref p);

                                // 撮影時は不可視にしたので廃止・・・
                                //System.Drawing.Rectangle rect = this.ClientRectangle;
                                //System.Drawing.Point mouseClientPos = this.PointToClient(p);
                                //if (rect.Contains(mouseClientPos))
                                //{
                                //    // EvidenceAppli内で押されていたら抜ける
                                //    m_isDragging = false;
                                //    break;
                                //}

                                // マウス座標をクライアント座標系へ変換
                                m_sp = m_FullScreen.PointToClient(p);
                                // ドラッグ開始
                                m_isDragging = true;
                            }
                            break;
                        }
                    case MouseHookInfo.WM_LBUTTONUP:
                        {
                            if (m_st == Status.FSELECT)
                            {
                                // フォーム選択

                                // マウス座標の取得
                                System.Drawing.Point p = new System.Drawing.Point();
                                GetCursorPos(ref p);

                                /*
                                 * キャプチャボタン上で反応しないようにする
                                 */
                                System.Drawing.Rectangle rect = this.ClientRectangle;
                                // マウス座標をクライアント座標系へ変換
                                System.Drawing.Point mouseClientPos = this.PointToClient(p);
                                if (rect.Contains(mouseClientPos))
                                {
                                    // EvidenceAppli内で押されたら、hWndを更新しない
                                    break;
                                }
                                else
                                {
                                    // EvidenceAppliの外で押されたら、hWndを更新する
                                    hWnd = WindowFromPoint(p);
                                    if (hWnd == IntPtr.Zero)
                                    {
                                        break;
                                    }
                                }

                                /*
                                 * ウィンドウ情報を取得してラベルに表示
                                 */
                                //ウィンドウのタイトルの長さを取得する
                                int textLen = GetWindowTextLength(hWnd);
                                if (0 < textLen)
                                {
                                    //ウィンドウのタイトルを取得する
                                    StringBuilder tsb = new StringBuilder(textLen + 1);
                                    GetWindowText(hWnd, tsb, tsb.Capacity);

                                    //ウィンドウのクラス名を取得する
                                    StringBuilder csb = new StringBuilder(256);
                                    GetClassName(hWnd, csb, csb.Capacity);

                                    //結果を表示する
                                    Console.WriteLine("クラス名:" + csb.ToString());
                                    Console.WriteLine("タイトル:" + tsb.ToString());
                                    captuerTargetInfo.Text = tsb.ToString();
                                    // 画像選択中にする
                                    m_st = Status.HOLD;
                                    SetButtonStatus(m_st);
                                }
                            }
                            else if ((m_st == Status.RCAPTURE) || (m_st == Status.RSELECT))
                            {
                                // 矩形

                                if (m_isDragging == false)
                                {
                                    // ドラッグ中じゃないなら抜ける
                                    break;
                                }
                                // マウス座標の取得
                                System.Drawing.Point p = new System.Drawing.Point();
                                GetCursorPos(ref p);
                                // マウス座標をクライアント座標系へ変換
                                m_ep = m_FullScreen.PointToClient(p);

                                /*
                                 * 矩形選択
                                 */
                                System.Drawing.Point start = new System.Drawing.Point();
                                System.Drawing.Point end = new System.Drawing.Point();
                                GetRegion(m_sp, m_ep, ref start, ref end);
                                captuerTargetInfo.Text = "左上座標:[" + start.X.ToString() + @"," + start.Y.ToString() + @"]"
                                    + " - 右下座標:[" + end.X.ToString() + @"," + end.Y.ToString() + @"]";

                                // 状態を画像選択中にする
                                m_st = Status.RHOLD;
                                SetButtonStatus(m_st);

                                // 矩形キャプチャが終わったのでm_FullScreenは捨てる
                                m_FullScreen.TopMost = false;
                                m_FullScreen.Close();

                                if (m_FullScreenbmp != null)
                                {
                                    m_FullScreenbmp.Dispose();
                                }
                                // EvidenceAppliを可視化して、つかえるようにする
                                this.Opacity = 1;
                                this.Activate();
                                this.clear_button.Focus();
                                this.Show();

                                /// m_FullScreenからフォーカスが戻らないので強制的にクリックイベントを発生させる
                                mouse_event(0x4, 0, 0, 0, 0);
                                // ドラッグ終了
                                m_isDragging = false;
                            }
                            break;
                        }
                    case MouseHookInfo.WM_LBUTTONDBLCLK:
                        {
                            break;
                        }
                    case MouseHookInfo.WM_RBUTTONDOWN:
                        {
                            break;
                        }
                    case MouseHookInfo.WM_RBUTTONUP:
                        {
                            break;
                        }
                    case MouseHookInfo.WM_RBUTTONDBLCLK:
                        {
                            break;
                        }
                    case MouseHookInfo.WM_MBUTTONDOWN:
                        {
                            break;
                        }
                    case MouseHookInfo.WM_MBUTTONUP:
                        {
                            break;
                        }
                    case MouseHookInfo.WM_MBUTTONDBLCLK:
                        {
                            break;
                        }
                    case MouseHookInfo.WM_MOUSEMOVE:
                        {
                            if (m_st == Status.RSELECT)
                            {
                                if (m_isDragging == false)
                                {
                                    // ドラッグ中じゃないなら抜ける
                                    break;
                                }

                                // 現在のマウスの座標を取得して枠線を描く
                                System.Drawing.Point p = new System.Drawing.Point();
                                GetCursorPos(ref p);
                                System.Drawing.Point start = new System.Drawing.Point();
                                System.Drawing.Point end = new System.Drawing.Point();
                                // 座標から(X,Y)座標を計算
                                GetRegion(m_sp, m_FullScreen.PointToClient(p), ref start, ref end);

                                // 前回書いた枠を残さないように画面更新
                                m_FullScreen.Refresh();

                                DrawRegion(start, end);
                            }
                            break;
                        }
                    case MouseHookInfo.WM_MOUSEWHEEL:
                        {
                            break;
                        }
                }
            }
            return MouseHookAPI.CallNextHookEx(MouseHookInfo.hHook, nCode, wParam, lParam);
        }

        /// <summary>
        /// ダイアログ表示 dobon.netのほぼそのまま
        /// </summary>
        private bool DispDialog(string message)
        {
            //SaveFileDialogクラスのインスタンスを作成
            SaveFileDialog sfd = new SaveFileDialog();

            //はじめのファイル名を指定する
            //はじめに「ファイル名」で表示される文字列を指定する
            sfd.FileName = "新しいファイル.xlsx";
            //はじめに表示されるフォルダを指定する
            sfd.InitialDirectory = @"C:\";
            //[ファイルの種類]に表示される選択肢を指定する
            //指定しない（空の文字列）の時は、現在のディレクトリが表示される
            sfd.Filter = "xlsxファイル(*.xlsx)|*.xlsx";
            //[ファイルの種類]ではじめに選択されるものを指定する
            //2番目の「すべてのファイル」が選択されているようにする
            sfd.FilterIndex = 2;
            //タイトルを設定する
            sfd.Title = "保存先のファイルを選択してください";
            //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            sfd.RestoreDirectory = true;
            //既に存在するファイル名を指定したとき警告する
            //デフォルトでTrueなので指定する必要はない
            sfd.OverwritePrompt = true;
            //存在しないパスが指定されたとき警告を表示する
            //デフォルトでTrueなので指定する必要はない
            sfd.CheckPathExists = true;

            //ダイアログを表示する
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                //OKボタンがクリックされたとき、選択されたファイル名を表示する
                m_ExcelFileProcess.FileName = sfd.FileName;
                m_FileManager.FileName = sfd.FileName;
                Console.WriteLine(sfd.FileName);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 枠描画
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        private void DrawRegion(System.Drawing.Point start, System.Drawing.Point end)
        {
            Pen blackPen = new Pen(Color.Red);
            // m_FullScreenのグラフィックオブジェクトを作成
            Graphics g = m_FullScreen.CreateGraphics();
            // 描画する線を点線に設定
            blackPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            // 領域を描画
            g.DrawRectangle(blackPen, start.X, start.Y, GetLength(start.X, end.X), GetLength(start.Y, end.Y));
            g.Dispose();
        }

        /// <summary>
        /// 長さを求める
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private int GetLength(int start, int end)
        {
            return Math.Abs(start - end);
        }

        /// <summary>
        /// 始点を左上、終点を右下にする
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        private void GetRegion(System.Drawing.Point p1, System.Drawing.Point p2, ref System.Drawing.Point start, ref System.Drawing.Point end)
        {
            start.X = Math.Min(p1.X, p2.X);
            start.Y = Math.Min(p1.Y, p2.Y);
            end.X = Math.Max(p1.X, p2.X);
            end.Y = Math.Max(p1.Y, p2.Y);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_FileManager.GazounoHozonKeisiki = comboBox1.Text;
            m_ExcelFileProcess.GazounoHozonKeisiki = comboBox1.Text;
        }
    }

    #endregion Method
}