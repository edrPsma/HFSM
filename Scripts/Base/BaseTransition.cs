/*
 * @Author: edR && pkq3344520@gmail.com
 * @Date: 2023-04-14 21:55:12
 * @LastEditors: edR && pkq3344520@gmail.com
 * @LastEditTime: 2023-04-14 21:59:55
 * @Description: 状态转换基类
 */


using System;
using UnityEngine;

public class BaseTransition<TState> : ITransition<TState>
{
    public TState From { get; private set; }
    public TState To { get; private set; }
    public Func<bool> Condition { get; private set; }
    public bool Immediately { get; private set; }

    public BaseTransition(TState from, TState to, Func<bool> condition, bool immediately)
    {
        From = from;
        To = to;
        Condition = condition;
        Immediately = immediately;
    }
}