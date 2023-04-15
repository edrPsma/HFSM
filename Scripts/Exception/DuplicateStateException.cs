/*
 * @Author: edR && pkq3344520@gmail.com
 * @Date: 2023-04-15 10:55:10
 * @LastEditors: edR && pkq3344520@gmail.com
 * @LastEditTime: 2023-04-15 11:11:25
 * @Description: 状态重复添加异常类
 */

using System;

public class DuplicateStateException : Exception
{
    public DuplicateStateException(string message) : base(message)
    {

    }
}