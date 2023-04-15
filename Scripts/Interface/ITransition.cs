/*
 * @Author: edR && pkq3344520@gmail.com
 * @Date: 2023-04-14 21:38:52
 * @LastEditors: edR && pkq3344520@gmail.com
 * @LastEditTime: 2023-04-14 21:41:48
 * @Description: 状态转换接口
 */


using System;

public interface ITransition<TState>
{
    TState From { get; }
    TState To { get; }
    Func<bool> Condition { get; }
    bool Immediately { get; }
}