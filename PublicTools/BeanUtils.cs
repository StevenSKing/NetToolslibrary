using System;

namespace PublicTools;

public class BeanUtils
{
    /// <summary>
    /// 复制对象属性值
    /// </summary>
    /// <param name="tIn"></param>
    /// <param name="tOut"></param>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    public static void CopyProperty<TIn, TOut>(TIn tIn, TOut tOut)
    {
        // if (tOut == null) tOut = Activator.CreateInstance<TOut>();
        var tInType = tIn.GetType();
        foreach (var itemOut in tOut.GetType().GetProperties())
        {
            var itemIn = tInType.GetProperty(itemOut.Name);
            if (itemIn != null)
            {
                try
                {
                    itemOut.SetValue(tOut, itemIn.GetValue(tIn));
                }
                catch (Exception e)
                {
                    Console.WriteLine("写入属性失败(将继续下一个属性): " + itemIn.Name + "; " + e);
                }
            }
        }
        // return tOut;
    }
}