// Copyright (c) Jeffcky <see cref="https://jeffcky.ke.qq.com/"/> All rights reserved.
using System.Collections.Generic;
using System.Linq;

namespace IDS4Demo
{
    public static class Check
    {
        /// <summary>
        /// 检查值不为空
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static void NotNull<T>(T value, string message)
        {
            if (value == null)
            {
                throw new ApiException(message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static void NotEmpty<T>(IReadOnlyList<T> value, string message)
        {
            NotNull(value, message);

            if (value.Count == 0)
            {
                NotEmpty(value, message);

                throw new ApiException(message);
            }
        }

        /// <summary>
        /// 检查字符串既不能为空也不能为空字符串
        /// </summary>
        /// <param name="value"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static void NotEmpty(string value, string message)
        {
            if (value is null)
            {
                throw new ApiException(message);
            }
            else if (value.Trim().Length == 0)
            {
                throw new ApiException(message);
            }
        }


        /// <summary>
        /// 检查参数可为空，但参数值不为空字符串
        /// </summary>
        /// <param name="value"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static void NullButNotEmpty(string value, string message)
        {
            if (!(value is null)
                && value.Length == 0)
            {
                NotEmpty(value, message);

                throw new ApiException(message);
            }
        }

        /// <summary>
        /// 检查集合或集合中参数不能有空值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static void HasNoNulls<T>(IReadOnlyList<T> value, string message)
            where T : class
        {
            NotNull(value, message);

            if (value.Any(e => e == null))
            {
                throw new ApiException(message);
            }
        }

        /// <summary>
        /// 满足条件
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="message"></param>
        public static void Condition(bool condition, string message)
        {
            if (!condition)
            {
                throw new ApiException(message);
            }
        }
    }
}
