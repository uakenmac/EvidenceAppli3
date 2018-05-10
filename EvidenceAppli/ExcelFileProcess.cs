using Microsoft.Office.Interop.Excel;
using System;
using System.Runtime.InteropServices;

namespace EvidenceAppli
{
    public class ExcelFileProcess
    {
        #region field

        /// <summary>
        /// 終了フラグ
        /// </summary>
        private bool isExcelFileColse = false;

        // Excel操作用オブジェクト
        private static Application m_xlApp = null;

        private static Workbooks m_xlBooks = null;
        private static Workbook m_xlBook = null;
        private string tmpFolder;

        private string gazouhozonkeisiki = null;

        #endregion field

        #region property

        public string FileName
        {
            get;

            set;
        }

        public string GazounoHozonKeisiki
        {
            get { return @"." + gazouhozonkeisiki; }

            set { gazouhozonkeisiki = value; }
        }

        #endregion property

        #region Consturctor

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ExcelFileProcess()
        {
        }

        public ExcelFileProcess(string tmpFolder)
        {
            this.tmpFolder = tmpFolder;
            // Excelアプリインスタンス生成
            m_xlApp = new Application();
            m_xlApp.Visible = true; // ウィンドウ表示化
            m_xlApp.DisplayAlerts = false;// アラートを出さない
        }

        #endregion Consturctor

        #region Method

        /// <summary>
        /// ブックオープン
        /// </summary>
        public void OpenBook()
        {
            m_xlBook = m_xlApp.Workbooks.Add();
            //不要シート削除
            for (int ws_cnt = m_xlBook.Worksheets.Count; ws_cnt > 1; ws_cnt--)
            {
                m_xlBook.Worksheets[ws_cnt].Delete();
            }
        }

        /// <summary>
        /// シート追加
        /// </summary>
        /// <param name="no">シート番号</param>
        public void AddSheet(int no, string sheetname)
        {
            Sheets m_xlSheets = m_xlBook.Worksheets;
            try
            {
                // シート追加
                m_xlSheets.Add(Type.Missing, m_xlSheets[m_xlSheets.Count], 1);
                // シート変数
                Worksheet xlSheet = m_xlSheets[no];
                try
                {
                    // シート名変更
                    xlSheet.Name = sheetname;
                    //シートを選択状態にする。
                    xlSheet.Select();
                }
                finally
                {
                    // xlSheet解放
                    if (xlSheet != null)
                    {
                        Marshal.ReleaseComObject(xlSheet);
                        xlSheet = null;
                    }
                }
            }
            finally
            {
                if (m_xlSheets != null)
                {
                    Marshal.ReleaseComObject(m_xlSheets);
                    m_xlSheets = null;
                }
            }
        }

        /// <summary>
        /// 画像セット
        /// </summary>
        /// <param name="no">シート番号</param>
        public int SetPictuer(int no, string sheetname, string gazouname, int row, int col)
        {
            if (no < 1)
            {
                return -1;
            }
            int returnValue = 0;
            Sheets m_xlSheets = m_xlBook.Worksheets;
            try
            {
                Worksheet sh = m_xlSheets[no];
                try
                {
                    if (sheetname == sh.Name)
                    {
                        Range range1 = sh.Cells[row, col];

                        string pfile = tmpFolder + @"\" + gazouname + GazounoHozonKeisiki;

                        Shape shp = sh.Shapes.AddPicture(pfile,
                            Microsoft.Office.Core.MsoTriState.msoFalse,
                            Microsoft.Office.Core.MsoTriState.msoTrue,
                            range1.Left,
                            range1.Top,
                            0,
                            0);
                        shp.ScaleHeight(1, Microsoft.Office.Core.MsoTriState.msoTrue);
                        shp.ScaleWidth(1, Microsoft.Office.Core.MsoTriState.msoTrue);
                        returnValue = shp.BottomRightCell.Row;
                    }
                }
                finally
                {
                    if (sh != null)
                    {
                        Marshal.ReleaseComObject(sh);
                        sh = null;
                    }
                }
            }
            finally
            {
                if (m_xlSheets != null)
                {
                    Marshal.ReleaseComObject(m_xlSheets);
                    m_xlSheets = null;
                }
            }
            return returnValue;
        }

        /// <summary>
        /// セルに文字列を挿入する
        /// </summary>
        /// <param name="no">シート番号</param>
        /// <param name="row">行</param>
        /// <param name="col">列</param>
        /// <param name="str">文字列</param>
        public void SetMojiretu(int no, int row, int col, string str)
        {
            if (row < 1 || col < 1)
            {
                return;
            }

            if (no < 1)
            {
                return;
            }

            Sheets m_xlSheets = m_xlBook.Worksheets;
            try
            {
                Worksheet sh = m_xlSheets[no];
                try
                {
                    if (no.ToString() == sh.Name)
                    {
                        sh.Cells[row, col] = str;
                    }
                }
                finally
                {
                    if (sh != null)
                    {
                        Marshal.ReleaseComObject(sh);
                    }
                }
            }
            finally
            {
                if (m_xlSheets != null)
                {
                    Marshal.ReleaseComObject(m_xlSheets);
                    m_xlSheets = null;
                }
            }
        }

        /// <summary>
        /// 終了
        /// </summary>
        public void CloseBook()
        {
            if (!isExcelFileColse)
            {
                isExcelFileColse = true;
                ReleaseExcelComObject();
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        public void Save()
        {
            try
            {
                m_xlBook.SaveAs(FileName);
            }
            catch
            {
                Marshal.ReleaseComObject(m_xlBook);
                m_xlBook = null;
            }
        }

        /// <summary>
        /// COM解放
        /// </summary>
        /// <param name="ReleaseMode"></param>
        private void ReleaseExcelComObject()
        {
            // m_xlBook解放
            if (m_xlBook != null)
            {
                try
                {
                    m_xlBook.Close();
                }
                finally
                {
                    Marshal.ReleaseComObject(m_xlBook);
                    m_xlBook = null;
                }
            }

            // m_xlBooks解放
            if (m_xlBooks != null)
            {
                Marshal.ReleaseComObject(m_xlBooks);
                m_xlBooks = null;
            }

            // m_xlApp解放
            if (m_xlApp != null)
            {
                try
                {
                    // アラートを戻して終了
                    m_xlApp.DisplayAlerts = true;
                    m_xlApp.Quit();
                }
                finally
                {
                    Marshal.ReleaseComObject(m_xlApp);
                }
            }
        }

        #endregion Method
    }
}