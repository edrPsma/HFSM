/*
 * @Author: edR && pkq3344520@gmail.com
 * @Date: 2023-04-14 21:44:31
 * @LastEditors: edR && pkq3344520@gmail.com
 * @LastEditTime: 2023-04-15 15:32:44
 * @Description: 状态基类
 */


using UnityEngine;

public abstract class BaseState : IState
{
    string IState.StateName { get; set; }
    bool IState.CanExit { get; set; }
    bool IState.HasExitTime { get; set; }
    public BaseState(bool hasExitTime)
    {
        (this as IState).HasExitTime = hasExitTime;
    }

    void IState.Init()
    {
        OnInit();
    }

    void IState.Enter()
    {
        var self = (this as IState);
        self.CanExit = self.HasExitTime ? false : true;
        OnEnter();
    }

    void IState.Excute()
    {
        OnExcute();
    }

    void IState.Exit()
    {
        OnExit();
        var self = (this as IState);
        self.CanExit = self.HasExitTime ? false : true;
    }

    protected virtual void OnInit() { }
    protected virtual void OnEnter() { }
    protected virtual void OnExcute() { }
    protected virtual void OnExit() { }
    protected void ReadyToExit()
    {
        (this as IState).CanExit = true;
    }
}