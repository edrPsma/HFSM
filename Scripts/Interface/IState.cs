/*
 * @Author: edR && pkq3344520@gmail.com
 * @Date: 2023-04-14 21:31:17
 * @LastEditors: edR && pkq3344520@gmail.com
 * @LastEditTime: 2023-04-15 11:19:44
 * @Description: 状态接口
 */


using UnityEngine;

public interface IState
{
    string StateName { get; set; }
    bool CanExit { get; set; }
    bool HasExitTime { get; set; }
    void Init();
    void Enter();
    void Excute();
    void Exit();
}