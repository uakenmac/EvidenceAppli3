using System.Drawing;
using System.Windows.Forms;

namespace EvidenceAppli
{
    public class ImageFile
    {
        #region Consturctor

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ImageFile()
        {
        }

        #endregion Consturctor

        #region Method

        /// <summary>
        /// 選択イメージをクリアする
        /// </summary>
        /// <param name="pictureBox"></param>
        public void ClearSelectImage(PictureBox pictureBox)
        {
            // pictuerBoxに画像が表示されていたら削除する
            // 現在のイメージを退避
            Image oldimg = pictureBox.Image;
            // デフォルトのビットマップオブジェクトを作成（BackColorを表示するのでGrahicオブジェクトの処理は不要）
            Bitmap defaultimg = new Bitmap(pictureBox.Width, pictureBox.Height);
            // 画像セット
            pictureBox.Image = defaultimg;
            // 退避したイメージオブジェクトを破壊
            if (oldimg != null)
            {
                oldimg.Dispose();
                oldimg = null;
            }
        }

        /// <summary>
        /// 選択画像を表示する
        /// </summary>
        public void DispImage(Bitmap bmp, PictureBox pictureBox)
        {
            // ピクチャボックスに表示する
            Image oldimg = null;
            if (pictureBox.Image != null)
            {
                oldimg = pictureBox.Image;
            }
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom; // 画像サイズは保持する
            pictureBox.Image = bmp;
            if (oldimg != null)
            {
                oldimg.Dispose();
                oldimg = null;
            }
        }

        #endregion Method
    }
}