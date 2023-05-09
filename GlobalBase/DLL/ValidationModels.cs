using System.ComponentModel.DataAnnotations;
using ExtensionTools.DTO;
using PublicTools;

namespace ExtensionTools.DLL
{
    public class ValidationModels
    {
    }

    /// <summary>
    /// 验证查询时间
    /// </summary>
    public class PaginationPramsIsValidAttribute : ValidationAttribute
    {
        /// <summary>
        /// 验证是否可用
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var movie = (PaginationPrams)validationContext.ObjectInstance;
            if (!string.IsNullOrEmpty(movie.fist_time))
            {
                try
                {
                    var time1 = TimeTools.GetDateTime(movie.fist_time);
                }
                catch
                {
                    return new ValidationResult($"结束时间格式不正确");
                }
            }
            if (!string.IsNullOrEmpty(movie.last_time))
            {
                try
                {
                    var time1 = TimeTools.GetDateTime(movie.last_time);
                }
                catch
                {
                    return new ValidationResult($"结束时间格式不正确");
                }
            }
            if (!string.IsNullOrEmpty(movie.fist_time) && !string.IsNullOrEmpty(movie.last_time))
            {
                var time1 = TimeTools.GetDateTime(movie.fist_time);
                var time2 = TimeTools.GetDateTime(movie.last_time);
                if (time1 > time2)
                {
                    return new ValidationResult($"开始时间不能大于结束时间");
                }
            }
            return ValidationResult.Success;
        }

    }

}
