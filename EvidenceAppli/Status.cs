namespace EvidenceAppli
{
    /// <remarks>メインフォームの状態</remarks>
    public enum Status
    {
        /// <summary>
        /// フォーム選択開始
        /// </summary>
        FSELECT,

        /// <summary>
        /// フォーム選択解除
        /// </summary>
        FCANCEL,

        /// <summary>
        ///  矩形選択開始
        /// </summary>
        RSELECT,

        /// <summary>
        /// 矩形選択解除
        /// </summary>
        RCANCEL,

        /// <summary>
        /// フォームキャプチャ
        /// </summary>
        CAPTURE,

        /// <summary>
        /// 矩形キャプチャ
        /// </summary>
        RCAPTURE,

        /// <summary>
        /// フォーム画像選択中
        /// </summary>
        HOLD,

        /// <summary>
        /// 矩形画像選択中
        /// </summary>
        RHOLD,

        /// <summary>
        /// 初期状態
        /// </summary>
        INIT,

        /// <summary>
        /// 保存して終了
        /// </summary>
        SCLOSE,

        /// <summary>
        /// 保存せず終了
        /// </summary>
        CLOSE,

        /// <summary>
        /// ターミネーター
        /// </summary>
        NUM,
    }
}