/*
 * @Author: edR && pkq3344520@gmail.com
 * @Date: 2023-04-15 11:10:46
 * @LastEditors: edR && pkq3344520@gmail.com
 * @LastEditTime: 2023-04-15 11:11:06
 * @Description: 状态不存在异常类
 */

using System;

public class StateNotExitException : Exception
{
    public StateNotExitException(string message) : base(message)
    {

    }
}