using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Coldairarrow.Util;
using GlobalBase.DTO;
using Microsoft.EntityFrameworkCore;

namespace GlobalBase.DLL
{
    public interface ILinqDao<T> where T : class
    {
        /*Task<bool> Add(T Entity);*/

        Task<bool> Delete(T Entity);

        Task<bool> Update(T Entity);

        IEnumerable<T> GetEntities(Expression<Func<T, bool>> exp);
    }


    public class result
    {
        public string id { get; set; }
        public int count { get; set; }
    }

    /// <summary>
    /// EFCore Linq 操作封装
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EFCoretoLinq<T> : ILinqDao<T> where T : class
    {
        private DbContext db;

        /// <summary>
        /// 构造函数传入DB
        /// </summary>
        /// <param name="_db"></param>
        public EFCoretoLinq(DbContext _db)
        {
            _db.Database.SetCommandTimeout(10);

            db = _db;
        }


        private static bool _initIdWork;

        private static void initIdWord()
        {
            if (_initIdWork) return;
            new IdHelperBootstrapper().SetWorkderId(1).Boot();
            _initIdWork = true;
        }

        /// <summary>
        /// 异步执行增加操作以及返回带有输出的参数
        /// </summary>
        /// <param name="Entity"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<result> Add(T Entity, bool key = true)
        {
            initIdWord();
            var id = IdHelper.GetId();
            using (var dbs = db)
            {
                // 反射给 [Key] 属性生成雪花ID
                if (key)
                {
                    var Properties = Entity.GetType().GetProperties();
                    for (var i = 0; i < Properties.Length; i++)
                    {
                        var pro = Properties[i];
                        var a = pro.CustomAttributes.FirstOrDefault(x => x.AttributeType == typeof(KeyAttribute));
                        if (a == null) continue;
                        if (!pro.CanWrite) continue;
                        pro.SetValue(Entity, id);
                        //ids = id;
                        break;
                    }
                }

                await dbs.Set<T>().AddAsync(Entity);

                return new result { id = id, count = await dbs.SaveChangesAsync() };
            }
        }

        /// <summary>
        /// 通步执行增加操作以及返回带有输出的参数
        /// </summary>
        /// <param name="Entity"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public result AddSync(T Entity, bool key = true)
        {
            initIdWord();
            var id = IdHelper.GetId();
            using (var dbs = db)
            {
                // 反射给 [Key] 属性生成雪花ID
                if (key)
                {
                    var Properties = Entity.GetType().GetProperties();
                    for (var i = 0; i < Properties.Length; i++)
                    {
                        var pro = Properties[i];
                        var a = pro.CustomAttributes.FirstOrDefault(x => x.AttributeType == typeof(KeyAttribute));
                        if (a == null) continue;
                        if (!pro.CanWrite) continue;
                        pro.SetValue(Entity, id);
                        //ids = id;
                        break;
                    }
                }
                dbs.Set<T>().Add(Entity);
                return new result { id = id, count = dbs.SaveChanges() };
            }
        }



        /// <summary>
        /// 异步执行批量增加操作以及返回带有输出的参数
        /// </summary>
        /// <param name="Entity"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<bool> Adds(T[] Entity, bool key = true)
        {

            if (key)
            {
                foreach (var item in Entity)
                {
                    initIdWord();
                    var Properties = item.GetType().GetProperties();
                    for (var i = 0; i < Properties.Length; i++)
                    {
                        var id = IdHelper.GetId();
                        var pro = Properties[i];
                        var a = pro.CustomAttributes.FirstOrDefault(x => x.AttributeType == typeof(KeyAttribute));
                        if (a == null) continue;
                        if (!pro.CanWrite) continue;
                        pro.SetValue(item, id);
                        //ids = id;
                        break;
                    }
                }
            }
            using (var dbs = db)
            {
                await dbs.Set<T>().AddRangeAsync(Entity);
                return await dbs.SaveChangesAsync() > 0;
            }
        }

        /// <summary>
        ///  异步执行删除操作以及返回带有输出的参数
        /// </summary>
        /// <param name="Entity"></param>
        /// <returns></returns>
        public async Task<bool> Delete(T Entity)
        {
            using (var dbs = db)
            {
                dbs.Set<T>().Remove(Entity);
                return await dbs.SaveChangesAsync() > 0;
            }
        }

        /// <summary>
        /// 异步执行条件删除操作以及返回带有输出的参数
        /// </summary>
        /// <param name="qure">筛选表达式</param>
        /// <returns></returns>
        public async Task<bool> DeleteQure(Expression<Func<T, bool>> qure)
        {
            using (var dbs = db)
            {
                dbs.Set<T>().Remove(CompileQuerySingle(qure));
                return await dbs.SaveChangesAsync() > 0;
            }
        }

        /// <summary>
        ///  异步执行批量删除操作以及返回带有输出的参数
        /// </summary>
        /// <param name="Entity"></param>
        /// <returns></returns>
        public async Task<bool> DeleteRange(IEnumerable<T> Entity)
        {
            using (var dbs = db)
            {
                dbs.Set<T>().RemoveRange(Entity);
                return await dbs.SaveChangesAsync() > 0;
            }
        }

        /// <summary>
        ///  异步执行修改操作以及返回带有输出的参数
        /// </summary>
        /// <param name="Entity"></param>
        /// <returns></returns>
        public async Task<bool> Update(T Entity)
        {
            using (var dbs = db)
            {
                db.Set<T>().Update(Entity);

                return await dbs.SaveChangesAsync() > 0;
            }
        }

        /// <summary>
        /// 更新部分字段(尽量使用)
        /// </summary>
        public async Task<int> UpdateEntity(T entity, bool IsCommit = true)
        {
            var result = 0;
            var userm = db.Entry<T>(entity);
            //把user对象加入上下文,但是没有改变
            using (var dbs = db)
            {
                if (entity.GetType().GetProperties().Any())
                {
                    foreach (var property in entity.GetType().GetProperties())
                    {
                        // var type = property.GetType();
                        // var defV = new(type);
                        var obj = property.GetValue(entity);
                        if (obj != null)
                        {
                            var keys = userm.Property(property.Name).Metadata.GetType().GetProperty("Keys");
                            var value = keys.GetValue(userm.Property(property.Name).Metadata);

                            if (value == null)
                            {
                                dbs.Entry<T>(entity).Property(property.Name).IsModified = true;
                            }
                        }
                        else
                        {
                            dbs.Entry<T>(entity).Property(property.Name).IsModified = false;
                        }

                    }
                }
                if (IsCommit)
                {
                    result = await dbs.SaveChangesAsync();
                }

            }

            return result;
        }


        /// <summary>
        ///  执行Linq查询操作以及返回实体集合
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public IEnumerable<T> GetEntities(Expression<Func<T, bool>> exp)
        {
            return CompileQuery(exp);
        }

        /// <summary>
        ///  执行Linq查询操作以及返回实体集合数量
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public int GetEntitiesCount(Expression<Func<T, bool>> exp)
        {
            var func = EF.CompileQuery((DbContext context, Expression<Func<T, bool>> exps) => context.Set<T>().Where(exp).Count());
            // return CompileQuery(exp).Count();
            return func(db, exp);

        }

        /// <summary>
        ///  执行Linq查询操作以及按照查询条件返回对应的实体集合(带翻页)
        /// </summary>
        /// <param name="Page">页码</param>
        /// <param name="pageSize">每页行数</param>
        /// <param name="exp">表达式</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="by">true 正序，false倒序</param>
        /// <returns></returns>
        public PaginationDto<T> GetEntitiesForPaging(int? Page, int? pageSize, Expression<Func<T, bool>> exp, string orderby, bool by)
        {
            int Pages = (int)(Page == null || Page == 0 ? 1 : Page);
            int pageSizes = (int)(pageSize == null || pageSize == 0 ? 10 : pageSize);
            Expression<Func<T, bool>> exps = exp ?? (x => true);
            PaginationDto<T> pagination = new PaginationDto<T>();

            if (by)
            {
                var func = EF.CompileQuery((DbContext context, Expression<Func<T, bool>> expss) =>
                context.Set<T>().Where(exps).OrderByDescending(e => EF.Property<object>(e, orderby)).Skip((Pages - 1) * pageSizes).Take(pageSizes));
                var funcres = func(db, exps);
                if (funcres.Count() > 0)
                {
                    pagination.data = func(db, exps).ToList();
                }
                else
                {
                    pagination.data = new List<T>();
                }

            }
            else
            {
                var func = EF.CompileQuery((DbContext context, Expression<Func<T, bool>> expss) =>
                context.Set<T>().Where(exps).OrderBy(e => EF.Property<object>(e, orderby)).Skip((Pages - 1) * pageSizes).Take(pageSizes));
                var funcres = func(db, exps);
                if (funcres.Count() > 0)
                {
                    pagination.data = funcres.ToList();
                }
                else
                {
                    pagination.data = new List<T>();
                }
            }
            //return CompileQuery(exp).Skip((Page - 1) * pageSize).Take(pageSize);
            pagination.total = GetEntitiesCount(exps);

            return pagination;
        }

        /// <summary>
        ///  执行Linq查询与排序操作以及返回单个实体
        /// </summary>
        /// <typeparam name="IKey"></typeparam>
        /// <param name="exp"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public T GetEntityOrderBy<IKey>(Expression<Func<T, bool>> exp, Expression<Func<T, IKey>> orderBy)
        {
            return CompileQuerySingle(exp, orderBy);
        }


        /// <summary>
        ///  执行Linq查询操作以及返回单个实体
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public T GetEntity(Expression<Func<T, bool>> exp)
        {
            return CompileQuerySingle(exp);
        }


        /// <summary>
        ///  执行Linq查询操作以及按照组合表达式返回对应的实体集合
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        private IEnumerable<T> CompileQuery(Expression<Func<T, bool>> exp)
        {
            var func = EF.CompileQuery((DbContext context, Expression<Func<T, bool>> exps) => context.Set<T>().Where(exp));
            return func(db, exp);
        }

        /// <summary>
        /// 执行查询与排序操作以及按照组合表达式返回对应的单个实体
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        private T CompileQuerySingle<IKey>(Expression<Func<T, bool>> exp, Expression<Func<T, IKey>> orderBy)
        {

            var func = EF.CompileQuery((DbContext context, Expression<Func<T, bool>> exps) => context.Set<T>().AsNoTracking().OrderByDescending(orderBy).FirstOrDefault(exp));
            return func(db, exp);
        }


        /// <summary>
        /// 执行查询操作以及按照组合表达式返回对应的单个实体
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        private T CompileQuerySingle(Expression<Func<T, bool>> exp)
        {

            var func = EF.CompileQuery((DbContext context, Expression<Func<T, bool>> exps) => context.Set<T>().FirstOrDefault(exp));
            return func(db, exp);
        }

    }


}
