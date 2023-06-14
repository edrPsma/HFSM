/*
 * @Author: edR && pkq3344520@gmail.com
 * @Date: 2023-04-14 22:00:29
 * @LastEditors: edR && pkq3344520@gmail.com
 * @LastEditTime: 2023-06-14 21:16:27
 * @Description: 状态机基类
 */


using System;
using System.Collections.Generic;

public abstract class BaseFSM<TState> : IFSM<TState>
{
    bool IState.HasExitTime { get; set; }
    TState IFSM<TState>.InitialState { get; set; }
    string IState.StateName { get; set; }
    bool IState.CanExit { get; set; }
    IState runningState = null;
    TState runningStateType;
    public string CurStateName { get; private set; }
    public string PreStateName { get; private set; }

    Dictionary<TState, IState> stateDic = new Dictionary<TState, IState>();
    Dictionary<TState, List<ITransition<TState>>> transitionDic = new Dictionary<TState, List<ITransition<TState>>>();
    List<ITransition<TState>> anyStatetransitionList = new List<ITransition<TState>>();
    public BaseFSM(bool hasExitTime, TState initialState)
    {
        (this as IFSM<TState>).InitialState = initialState;
        (this as IState).HasExitTime = hasExitTime;
    }

    #region public
    public void AddState(TState stateType, IState state)
    {
        if (stateDic.ContainsKey(stateType))
        {
            throw new DuplicateStateException("状态重复添加:" + stateType.ToString());
        }
        state.StateName = stateType.ToString();
        state.Init();
        stateDic.Add(stateType, state);
    }

    public void AddTransition(TState from, TState to, Func<bool> condition, bool immediately)
    {
        BaseTransition<TState> t = new BaseTransition<TState>(from, to, condition, immediately);
        if (from == null)
        {
            anyStatetransitionList.Add(t);
            return;
        }
        if (!transitionDic.ContainsKey(from))
        {
            transitionDic.Add(from, new List<ITransition<TState>>());
        }
        transitionDic[from].Add(t);
    }

    public void AddAnyTransition(TState to, Func<bool> condition, bool immediately)
    {
        BaseTransition<TState> t = new BaseTransition<TState>(to, to, condition, immediately);
        anyStatetransitionList.Add(t);
    }
    #endregion

    public void Init()
    {
        OnInit();
    }
    public void Enter()
    {
        TState initialState = (this as IFSM<TState>).InitialState;
        runningState = GetState(initialState);
        runningStateType = initialState;
        var self = (this as IState);
        self.CanExit = self.HasExitTime ? false : true;
        CurStateName = runningState.StateName;
        PreStateName = "None";
        runningState.Enter();
        OnEnter();
    }

    public void Excute()
    {
        TransitionDetect();
        runningState?.Excute();
        OnExcute();
    }

    public void Exit()
    {
        OnExit();
        var self = (this as IState);
        self.CanExit = self.HasExitTime ? false : true;
        runningState = null;
        CurStateName = "None";
        PreStateName = "None";
    }

    #region virtual
    protected virtual void OnInit() { }
    protected virtual void OnEnter() { }
    protected virtual void OnExcute() { }
    protected virtual void OnExit() { }
    #endregion

    void TransitionDetect()
    {
        //检测任意状态切换
        foreach (var item in anyStatetransitionList)
        {
            if (!item.Immediately && !runningState.CanExit) continue;
            if (item.Condition != null && item.Condition.Invoke())
            {
                SetTransition(item.To);
                return;
            }
        }

        //检测普通状态切换
        if (!transitionDic.ContainsKey(runningStateType)) return;
        List<ITransition<TState>> transitionList = transitionDic[runningStateType];
        foreach (var item in transitionList)
        {
            if (!item.Immediately && !runningState.CanExit) continue;
            if (item.Condition != null && item.Condition.Invoke())
            {
                SetTransition(item.To);
                return;
            }
        }

        void SetTransition(TState to)
        {
            runningState?.Exit();
            runningStateType = to;
            runningState = GetState(runningStateType);
            runningState?.Enter();
            PreStateName = CurStateName;
            CurStateName = runningState.StateName;
        }
    }

    IState GetState(TState state)
    {
        if (!stateDic.ContainsKey(state))
        {
            throw new StateNotExitException("状态不存在:" + state.ToString());
        }
        return stateDic[state];
    }
}