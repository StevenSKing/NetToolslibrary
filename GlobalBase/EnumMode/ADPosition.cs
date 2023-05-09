using System.Collections.Generic;

namespace GlobalBase.EnumMode
{
    /// <summary>
    /// 广告位置标识枚举
    /// </summary>
    public class ADPosition
    {
        public List<string> ADPositions = new List<string>
        {
            "TB","BB","TF","BF","W0"
        };

        /// <summary>
        /// 顶部横幅
        /// </summary>
        public string TopBanner { get; set; } = "TB";

        /// <summary>
        /// 底部横幅
        /// </summary>
        public string BottomBanner { get; set; } = "BB";

        /// <summary>
        /// 顶飘
        /// </summary>
        public string TopFloat { get; set; } = "TF";

        /// <summary>
        /// 底飘
        /// </summary>
        public string BottomFloat { get; set; } = "BF";

        /// <summary>
        /// 开屏
        /// </summary>
      //  public string OpenScreen { get; set; } = "OS";

        /// <summary>
        /// 透明遮罩点击
        /// </summary>
      //  public string ModalClick { get; set; } = "MC";

        /// <summary>
        /// 弹窗位置0
        /// </summary>
        public string Window0 { get; set; } = "W0";

        /*  /// <summary>
          /// 弹窗位置1
          /// </summary>
          public string Window1 { get; set; } = "W1";

          /// <summary>
          /// 弹窗位置2
          /// </summary>
          public string Window2 { get; set; } = "W2";

          /// <summary>
          /// 弹窗位置3
          /// </summary>
          public string Window3 { get; set; } = "W3";

          /// <summary>
          /// 弹窗位置4
          /// </summary>
          public string Window4 { get; set; } = "W4";

          /// <summary>
          /// 弹窗位置5
          /// </summary>
          public string Window5 { get; set; } = "W5";

          /// <summary>
          /// 弹窗位置6
          /// </summary>
          public string Window6 { get; set; } = "W6";

          /// <summary>
          /// 弹窗位置4
          /// </summary>
          public string Window7 { get; set; } = "W7";*/



    }
}
