using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CSRedis;

using GlobalBase.DTO;
//using GlobalBase.RedisModel;

using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GlobalBase.RedisServer
{

    /// <summary>
    /// 定义 Redis 操作接口
    /// </summary>
    public interface IRedisClient
    {
        R<string> Get(string key);

        bool Set(string key, object t, int expiresSec = 0);

        T Get<T>(string key) where T : new();

        Task<string> GetAsync(string key);

        Task<StatusMsg> SetAsync(string key, object t, int expiresSec = 0);

        Task<T> GetAsync<T>(string key) where T : new();
    }


    /// <summary>
    /// 实现 redis 接口
    /// </summary>
    public class RedisMake : IRedisClient
    {
        public static List<CSRedisClient> clients;

        public static void GetClients(List<CSRedisClient> redisClient)
        {
            clients = redisClient;
        }

        /// <summary>
        /// 切换RedisDB
        /// </summary>
        /// <param name="i">0：验证码；1：账户密码；2：client信息</param>
        /// <returns></returns>
        public static CSRedisClient ChangeDB(int i)
        {

            RedisHelper.Initialization(clients[i]);

            return RedisHelper.Instance;
        }

        /// <summary>
        /// 根据键名获取Redis单个值
        /// </summary>
        /// <param name="dbNum">表序号</param>
        /// <param name="key">键</param>        
        /// <returns></returns>
        public R<string> Get(string key)
        {
            var msg = new R<string>();
            try
            {
                var redis = new RedisMake();

                var value = RedisHelper.Get(key);
                if (string.IsNullOrEmpty(value))
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
        /// 模糊查询key
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public async Task<string[]> GetKeys(string pattern)
        {
            return await RedisHelper.KeysAsync(pattern);
        }

        /// <summary>
        /// 根据键名获取值T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get<T>(string key) where T : new()
        {
            return RedisHelper.Get<T>(key);
        }

        /// <summary>
        /// 存储键值对
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="t">对象</param>
        /// <param name="expiresSec">有效时间/s</param>
        /// <returns></returns>
        public bool Set(string key, object t, int expiresSec = 0)
        {
            return RedisHelper.Set(key, t, expiresSec);
        }

        /// <summary>
        /// 写入Redis(带失效，单位：秒)
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expiresSec">有效时间，单位：秒 留空为永久</param>
        /// <returns></returns>
        public async Task<StatusMsg> SetAsync(string key, object value, int expiresSec = -1)
        {
            var msg = new StatusMsg();
            try
            {
                var x = await RedisHelper.SetAsync(key, value, expiresSec);
                msg.Status = x;
            }
            catch (Exception e)
            {
                msg.Msg = e.ToString();
                msg.Status = false;
                return msg;

            }
            return msg;
        }

        /// <summary>
        /// 异步根据键名获取值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<string> GetAsync(string key)
        {
            return await RedisHelper.GetAsync(key);
        }

        /// <summary>
        /// 异步 根据键名获取值T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(string key) where T : new()
        {
            return await RedisHelper.GetAsync<T>(key);
        }


        /// <summary>
        /// 异步存储多个键值对
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<bool> SetListAsync<T>(Dictionary<string, T> key)
        {
            var sortedDict = key.OrderBy(kv => kv.Key).ToDictionary(kv => kv.Key, kv => kv.Value);
            var keys = sortedDict.SelectMany(kv => new object[] { kv.Key, kv.Value }).ToArray();
            return await RedisHelper.MSetAsync(keys);
        }

        /// <summary>
        /// 判断key是否存储
        /// </summary>
        /// <param name="key">redis key</param>
        /// <returns></returns>
        public bool KeyExists(string key)
        {
            return RedisHelper.Exists(key);
        }

        /// <summary>
        /// 异步判断key是否存储
        /// </summary>
        /// <param name="key">redis key</param>
        /// <returns></returns>
        public async Task<bool> KeyExistsAsync(string key)
        {
            return await RedisHelper.ExistsAsync(key);
        }

        /// <summary>
        /// 异步删除key
        /// </summary>
        /// <param name="key">redis key</param>
        /// <returns></returns>
        public async Task<long> DelAsync(string key)
        {
            return await RedisHelper.DelAsync(key);
        }

        /// <summary>
        /// 异步批量删除key
        /// </summary>
        /// <param name="key">redis key</param>
        /// <returns></returns>
        public async Task<long> MDelAsync(List<string> keys)
        {
            return await RedisHelper.DelAsync(keys.ToArray());
        }

        /// <summary>
        /// 获取列表长度
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long GetKeyCount(string key)
        {
            return RedisHelper.LLen(key);
        }

        /// <summary>
        /// 批量获取
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<T> GetMore<T>(List<string> key)
        {
            var keys = key.ToArray();
            return RedisHelper.MGet<T>(keys).ToList();
        }

        /// <summary>
        /// 批量设置
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool SetMore<T>(Dictionary<string, T> key)
        {
            var sortedDict = key.OrderBy(kv => kv.Key).ToDictionary(kv => kv.Key, kv => kv.Value);
            var keys = sortedDict.SelectMany(kv => new object[] { kv.Key, kv.Value }).ToArray();
            return RedisHelper.MSet(keys);
        }

        /// <summary>
        /// 获取并删除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object[] GetAndDel(string key)
        {
            return RedisHelper.StartPipe(z => z.Get(key).Del(key));
        }

        /*public List<T> RPush(string key)
        {
            return RedisHelper.(key);
        }*/

        /// <summary>
        /// 在列表末尾添加一个或多个值,并返回列表长度
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public long RPush(string key, params object[] values)
        {
            return RedisHelper.RPush(key, values);
        }

        /// <summary>
        /// 移出并获取列表的第一个元素
        /// </summary>
        public string LPop(string key)
        {
            return RedisHelper.LPop(key);
        }

        /// <summary>
        /// 原子累加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="num">累加数额</param>
        /// <returns></returns>
        public string Rincr(string key, int num = 1)
        {
            long nums = ((long)num);
            return RedisHelper.IncrByAsync(key, nums).ToString();
        }

        /// <summary>
        /// 原子哈希累加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="filed">字段</param>
        /// <param name="num">累加额</param>
        /// <returns></returns>
        public string HRincr(string key, string filed, int num = 1)
        {
            long nums = ((long)num);
            return RedisHelper.HIncrByAsync(key, filed, nums).ToString();
        }

        /// <summary>
        /// 管道原子哈希累加,设置IP集合 批处理
        /// </summary>
        /// <param name="sumCount"></param>
        /// <param name="ips"></param>
        /// <returns></returns>
/*        public object[] MHIncrSetPipe(List<SumCountDTO> sumCount, List<SumCountIPsDTO> ips)
        {

            return RedisHelper.StartPipe(p =>
            {
                foreach (SumCountDTO item in sumCount)
                {
                    long nums = ((long)item.Sum);
                    p.HIncrBy(item.Key, item.Filed, nums);
                };
                foreach (SumCountIPsDTO ip in ips)
                {
                    p.SAdd(ip.ConfigID, ip.IPs.ToArray());
                };

            });
        }*/

        /// <summary>
        /// 获取哈希表中指定Key的所有字段和值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Dictionary<string, string> MHGetAsync(string key)
        {

            return RedisHelper.HGetAll(key);
        }

        /// <summary>
        /// 管道获取多个哈希表key的所有字段和值
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public List<KeyValue<Dictionary<string, string>>> MGetIncrByPipe(List<string> keys)
        {
            List<KeyValue<Dictionary<string, string>>> keyvalues = new List<KeyValue<Dictionary<string, string>>>();

            foreach (string key in keys)
            {
                var v = MHGetAsync(key);

                KeyValue<Dictionary<string, string>> keyValue = new KeyValue<Dictionary<string, string>>();
                keyValue.key = key;
                keyValue.value = v;

                keyvalues.Add(keyValue);
            };

            return keyvalues;
        }

        /// <summary>
        /// 存储集合
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<int> SetData(string key, List<string> ips)
        {
            int i = (int)await RedisHelper.SAddAsync(key, ips);
            return i;
        }

        /// <summary>
        /// 删除集合元素
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<int> SetDel(string key, List<string> ips)
        {
            int i = (int)await RedisHelper.SRemAsync(key, ips);
            return i;
        }

        /// <summary>
        /// 返回集合种所有的元素
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        public async Task<List<string>> GetMembers(string key)
        {
            var list = await RedisHelper.SMembersAsync(key);
            return list.ToList();
        }

        /// <summary>
        /// 获取集合成员数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<int> GetDataCount(string key)
        {

            int i = (int)await RedisHelper.SCardAsync(key);
            return i;
        }

        /// <summary>
        /// 管道获取多个集合key的成员数量
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public List<KeyValue<long>> MGetCardPipe(List<string> keys)
        {
            List<KeyValue<long>> keyvalues = new List<KeyValue<long>>();

            foreach (string key in keys)
            {
                KeyValue<long> keyValue = new KeyValue<long>();
                keyValue.key = key;
                keyValue.value = RedisHelper.SCard(key);
                keyvalues.Add(keyValue);
            };


            return keyvalues;
        }

        /// <summary>
        /// HyperLogLog基数 新增
        /// </summary>
        /// <param name="key"></param>
        /// <param name="ips"></param>
        /// <returns></returns>
        public async Task<bool> SetPFAdd(string key, List<string> ips)
        {
            bool i = await RedisHelper.PfAddAsync(key, ips);
            return i;
        }

        /// <summary>
        /// HyperLogLog基数 获取
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int GetPFAdd(List<string> keys)
        {
            int i = (int)RedisHelper.PfCount(keys.ToArray());
            return i;
        }

        /// <summary>
        /// 管道获取多个key基数统计
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public List<KeyValue<long>> MPfCountPipe(List<string> keys)
        {
            List<KeyValue<long>> keyvalues = new List<KeyValue<long>>();

            foreach (string key in keys)
            {
                KeyValue<long> keyValue = new()
                {
                    key = key,
                    value = RedisHelper.PfCount(key)
                };
                keyvalues.Add(keyValue);
            };

            return keyvalues;
        }



        public class KeyValue<T>
        {

            public string key { get; set; }

            public T value { get; set; }
        }




    }
}
