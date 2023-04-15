/*
 * @Author: edR && pkq3344520@gmail.com
 * @Date: 2023-04-14 21:36:45
 * @LastEditors: edR && pkq3344520@gmail.com
 * @LastEditTime: 2023-04-14 22:06:09
 * @Description: 状态机接口
 */


using System;

public interface IFSM<TState> : IState
{
    TState InitialState { get; set; }
    void AddState(TState stateType, IState state);
    void AddTransition(TState from, TState to, Func<bool> condition, bool immediately);
}