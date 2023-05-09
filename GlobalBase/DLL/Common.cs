using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using PublicTools;

namespace ExtensionTools.DLL;

public class DBCommon
{
    private static readonly string Sqlcon = GetConfig.GetConfigs("MSSQLConnection:con0");
    private static readonly InitiDataBase InitiDataBase = new(Sqlcon);

    public static EFCoretoLinq<T> Linq<T>() where T : class => new EFCoretoLinq<T>(new InitiDataBase(Sqlcon));

    public static object GetPropertyValue(object obj, string property)
    {
        PropertyInfo propertyInfo = obj.GetType().GetProperty(property);
        return propertyInfo.GetValue(obj, null);
    }

    /// <summary>
    /// 按实体生成查询参数==
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="Entity"></param>
    /// <returns></returns>
    public static List<QueryEntity> queryEntities<T>(T Entity)
    {
        List<QueryEntity> entities = new List<QueryEntity>();
        if (!Entity.GetType().GetProperties().Any()) return entities;

        foreach (var property in Entity.GetType().GetProperties())
        {
            var value = property.GetValue(Entity, null);
            if (value == null) continue;

            // 获限注解上的比较操作符
            // 例如: [Operator(key: "Time", Operator: "EndWith")]
            IEnumerable values = null;
            if (value.GetType().IsArray)
                values = (IEnumerable)value;

            var arr = property.GetCustomAttributes(typeof(OperatorAttribute), false).ToList();
            int i = 0;
            foreach (var b in arr)
            {
                if (values != null)
                {
                    var arr2 = values.Cast<object>().ToArray();
                    if (arr2.Length >= i + 1) value = arr2[i];
                }


                var query1 = new QueryEntity();
                if (entities.Count > 0) query1.LogicalOperator = "AND";
                var c = (OperatorAttribute)b;
                query1.Operator = c.Operator;
                query1.Value = value;
                query1.Key = c.Key;
                entities.Add(query1);
                i++;
            }

            if (arr.Count > 0) continue;

            var query2 = new QueryEntity();
            if (entities.Count > 0) query2.LogicalOperator = "AND";
            query2.Operator = "Equals";
            query2.Value = value;
            query2.Key = property.Name;
            entities.Add(query2);
        }

        return entities;
    }

}

/// <summary>
///  操作比较符
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
public class OperatorAttribute : Attribute
{
    public OperatorAttribute(string key, string Operator)
    {
        Key = key;
        this.Operator = Operator;
    }

    public string Key { get; set; }
    public string Operator { get; set; }
}