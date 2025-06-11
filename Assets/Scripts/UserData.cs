using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class UserDataBase
{
    public List<UserData> users = new List<UserData>();
}
[System.Serializable]
public class UserData
{
    [Header("기본 정보")]
    public string userName;
    public int curCash = 600000;
    public int balance = 60000;

    [Header("로그인 정보")]
    [HideInInspector] public string logID;
    [HideInInspector] public string logPW;

    [Header("회원가입 정보")]
    [HideInInspector] public string signName;
    [HideInInspector] public string signID;
    [HideInInspector] public string signPW;
    [HideInInspector] public string signPWCheck;

    public UserData(string _signName, string _singID, string _signPW)
    {
        userName = _signName;
        signID = _singID;
        signPW = _signPW;
        signPWCheck = UIManager.Instance.signPwChack.text;
    }
}
