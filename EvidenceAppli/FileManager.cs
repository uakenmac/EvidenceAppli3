using System;
using System.Drawing;

namespace EvidenceAppli
{
    public class FileManager
    {
        #region field

        private string tmpFolder = null;

        /// <summary>
        /// ファイル数
        /// </summary>
        private static int m_count;

        public string FileName { set; get; }

        #endregion field

        #region constructor

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FileManager()
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="tmpFolder"></param>
        public FileManager(string tmpFolder)
        {
            this.Count = 1;
            if (System.IO.Directory.Exists(tmpFolder))
            {
                try
                {
                    System.IO.Directory.Delete(tmpFolder, true);
                }
                catch (Exception)
                {
                    System.Console.WriteLine(tmpFolder + ":フォルダの初期化に失敗しました。");
                }
            }
            try
            {
                // tmpフォルダを作る
                System.IO.DirectoryInfo di = System.IO.Directory.CreateDirectory(tmpFolder);
            }
            catch (Exception)
            {
                System.Console.WriteLine(tmpFolder + ":フォルダの作成に失敗しました。");
            }
            this.tmpFolder = tmpFolder;
        }

        #endregion constructor

        #region property

        /// <summary>
        /// ファイル数の取得と設定
        /// </summary>
        public int Count
        {
            get
            {
                // m_countは1以上にする
                if (m_count < 1)
                {
                    return 1;
                }
                else
                {
                    return m_count;
                }
            }
            set
            {
                m_count = value;
            }
        }

        public string GazounoHozonKeisiki
        {
            get;

            set;
        }

        /// <summary>
        /// CapturedImageへの関連の設定と取得
        /// </summary>
        public ImageFile CapturedImage
        {
            get;

            set;
        }

        #endregion property

        #region method

        /// <summary>
        /// カウンタ1増加
        /// </summary>
        public void IncCount()
        {
            ++m_count;
        }

        /// <summary>
        /// カウンタ1減少
        /// </summary>
        public void DecCount()
        {
            --m_count;
        }

        /// <summary>
        /// フォルダ間コピー
        /// </summary>
        public void CopyFolder()
        {
            string destDirName = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(FileName), System.IO.Path.GetFileNameWithoutExtension(FileName));

            //コピー先のディレクトリがあるときは削除して、同名フォルダを作成する
            if (System.IO.Directory.Exists(destDirName))
            {
                System.IO.Directory.Delete(destDirName, true);
            }
            System.IO.Directory.CreateDirectory(destDirName);
            //属性もコピー
            System.IO.File.SetAttributes(destDirName,
                System.IO.File.GetAttributes(tmpFolder));

            //コピー先のディレクトリ名の末尾に"\"をつける
            if (destDirName[destDirName.Length - 1] !=
                    System.IO.Path.DirectorySeparatorChar)
                destDirName = destDirName + System.IO.Path.DirectorySeparatorChar;

            //コピー元のディレクトリにあるファイルをコピー
            string[] files = System.IO.Directory.GetFiles(tmpFolder);
            foreach (string file in files)
                System.IO.File.Copy(file,
                    destDirName + System.IO.Path.GetFileName(file), true);
        }

        /// <summary>
        /// 画像を保存する
        /// </summary>
        public void SaveImageFile(Bitmap bmp, string noumber)
        {
            // Ping形式でPathで指定した保存
            if (bmp != null)
            {
                switch (GazounoHozonKeisiki)
                {
                    case "png":
                        bmp.Save(this.tmpFolder + @"\" + noumber + ".png", System.Drawing.Imaging.ImageFormat.Png);
                        break;

                    case "jpeg":
                        bmp.Save(this.tmpFolder + @"\" + noumber + ".jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;

                    case "bmp":
                        bmp.Save(this.tmpFolder + @"\" + noumber + ".bmp", System.Drawing.Imaging.ImageFormat.Bmp);
                        break;

                    case "gif":
                        bmp.Save(this.tmpFolder + @"\" + noumber + ".gif", System.Drawing.Imaging.ImageFormat.Gif);
                        break;

                    case "emf":
                        bmp.Save(this.tmpFolder + @"\" + noumber + ".emf", System.Drawing.Imaging.ImageFormat.Emf);
                        break;

                    case "tiff":
                        bmp.Save(this.tmpFolder + @"\" + noumber + ".tiff", System.Drawing.Imaging.ImageFormat.Tiff);
                        break;

                    case "wmf":
                        bmp.Save(this.tmpFolder + @"\" + noumber + ".wmf", System.Drawing.Imaging.ImageFormat.Wmf);
                        break;
                }
                bmp.Dispose();
                bmp = null;
            }
        }

        #endregion method
    }
}