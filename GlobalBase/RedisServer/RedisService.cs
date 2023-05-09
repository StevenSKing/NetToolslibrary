using System;
using System.Threading;
using System.Threading.Tasks;

using GlobalBase.DTO;

namespace GlobalBase.RedisServer
{
    public class RedisService
    {

        /// <summary>
        /// 根据键名获取Redis值
        /// </summary>
        /// <param name="dbNum">表序号</param>
        /// <param name="key">键</param>        
        /// <returns></returns>
        public R<object> GetReidsList(string key)
        {
            var msg = new R<object>();
            try
            {
                var redis = new RedisMake();

                var value = redis.Get<object>(key);
                if (value == null)
                {
                    msg.success = false;
                    msg.msg = "未获取到值";
                }
                else
                {
                    msg.success = true;
                    msg.data = value;
                }
            }
            catch (Exception e)
            {
                msg.msg = e.ToString();
                msg.success = false;
                return msg;

            }
            return msg;
        }

        /// <summary>
        /// 根据键名删除Redis值集合
        /// </summary>
        /// <param name="dbNum">表序号</param>
        /// <param name="key">键</param>        
        /// <returns></returns>
        public async Task<R<bool>> DelReids(string key)
        {
            var msg = new R<bool>();
            try
            {
                var redis = new RedisMake();

                await redis.DelAsync(key);
                msg.success = true;
                msg.data = true;

            }
            catch (Exception e)
            {
                msg.msg = e.ToString();
                msg.success = false;
                return msg;
            }
            return msg;
        }


        /// <summary>
        /// 移出并获取列表的第一个元素
        /// </summary>
        /// <param name="dbNum">表序号</param>
        /// <param name="key">键值对</param>        
        /// <returns></returns>
        public string LPop(string key)
        {
            lock (this)
            {
                var redis = new RedisMake();

                return redis.LPop(key);
            }
        }
        private static RedisService rs = new RedisService();
        private static string orderIdKeyName = "orderIdKeyName";

        /// <summary>
        /// 获取订单ID，此方法应当放入try中，在超时后会报错
        /// </summary>       
        public static string GetOrderId()
        {
            var sec = 0;
            var id = rs.LPop(orderIdKeyName);
            while (string.IsNullOrEmpty(id))
            {
                //10秒未获取到订单号，本次提交退出并抛出异常
                if (sec >= 10)
                {
                    throw new Exception("10秒未获取到订单号");
                }
                //如果获取结果为空，则表明订单池已被掏空，这里等待1秒后再拿
                Thread.Sleep(1000);
                sec++;
                rs.LPop(orderIdKeyName);
            }
            return id;
        }
    }
}
