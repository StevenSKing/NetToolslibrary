using System;

namespace PublicTools
{
    /// <summary>
    /// 根据身份证来获取基本信息
    /// </summary>
    public class GetPeopleInfo
    {
        /// <summary>
        /// 获取年龄，生日，性别
        /// </summary>
        /// <param name="identityCard"></param>
        /// <returns></returns>
        public static BirthdayAgeSex GetABS(string identityCard)
        {
            if (!AuthIDNumber(identityCard))
            {
                return null;
            }

            var entity = new BirthdayAgeSex();
            try
            {
                var strSex = string.Empty;
                if (identityCard.Length == 18)//处理18位的身份证号码从号码中得到生日和性别代码
                {
                    entity.Birthday = identityCard.Substring(6, 4) + "-" + identityCard.Substring(10, 2) + "-" + identityCard.Substring(12, 2);
                    strSex = identityCard.Substring(14, 3);
                }
                if (identityCard.Length == 15)
                {
                    entity.Birthday = "19" + identityCard.Substring(6, 2) + "-" + identityCard.Substring(8, 2) + "-" + identityCard.Substring(10, 2);
                    strSex = identityCard.Substring(12, 3);
                }

                entity.Age = CalculateAge(entity.Birthday);//根据生日计算年龄
                if (int.Parse(strSex) % 2 == 0)//性别代码为偶数是女性奇数为男性
                {
                    entity.Sex = "女";
                }
                else
                {
                    entity.Sex = "男";
                }
            }
            catch (Exception e) { }
            return entity;

        }
        /// <summary>
        /// 根据出生日期，计算精确的年龄
        /// </summary>
        /// <param name="birthDay">生日</param>
        /// <returns></returns>
        public static int CalculateAge(string birthDay)
        {
            var birthDate = DateTime.Parse(birthDay);
            var nowDateTime = DateTime.Now;
            var age = nowDateTime.Year - birthDate.Year;
            //再考虑月、天的因素
            if (nowDateTime.Month < birthDate.Month || (nowDateTime.Month == birthDate.Month && nowDateTime.Day < birthDate.Day))
            {
                age--;
            }
            return age;
        }

        /// <summary>
        /// 定义 生日年龄性别 实体
        /// </summary>
        public class BirthdayAgeSex
        {
            /// <summary>
            /// 生日
            /// </summary>
            public string Birthday { get; set; }
            /// <summary>
            /// 年龄
            /// </summary>
            public int Age { get; set; }
            /// <summary>
            /// 性别
            /// </summary>
            public string Sex { get; set; }
        }


        /// <summary>
        /// 身份证号码验证
        /// </summary>
        /// <param name="IDNumber"></param>
        /// <returns></returns>
        public static bool AuthIDNumber(string IDNumber)
        {
            if (string.IsNullOrEmpty(IDNumber) || (IDNumber.Length != 15 && IDNumber.Length != 18))
                return false;
            if (IDNumber.Length == 15)
                return CheckIDCard15(IDNumber);
            if (IDNumber.Length == 18)
                return CheckIDCard18(IDNumber);
            return true;
        }

        private static bool CheckIDCard15(string Id)

        {
            long n = 0;
            if (long.TryParse(Id, out n) == false || n < Math.Pow(10, 14))

            {
                return false;//数字验证

            }

            var address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";

            if (address.IndexOf(Id.Remove(2)) == -1)
            {
                return false;//省份验证
            }

            var birth = Id.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            var time = new DateTime();

            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证
            }

            return true;//符合15位身份证标准

        }

        /// <summary>
        /// 检查18位身份证号码
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        private static bool CheckIDCard18(string Id)

        {
            long n = 0;
            if (long.TryParse(Id.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(Id.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                return false;//数字验证
            }

            var address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";

            if (address.IndexOf(Id.Remove(2)) == -1)
            {
                return false;//省份验证
            }
            var birth = Id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            var time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证
            }
            var arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            var Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            var Ai = Id.Remove(17).ToCharArray();
            var sum = 0;
            for (var i = 0; i < 17; i++)
            {
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
            }
            var y = -1;
            Math.DivRem(sum, 11, out y);
            if (arrVarifyCode[y] != Id.Substring(17, 1).ToLower())
            {
                return false;//校验码验证
            }
            return true;//符合GB11643-1999标准

        }

    }
}
