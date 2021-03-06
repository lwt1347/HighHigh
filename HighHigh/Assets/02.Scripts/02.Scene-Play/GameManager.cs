﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {


    //플레이어
    [SerializeField]
    private Player player = null;

    //물리적용 백그라운드
    [SerializeField]
    private MoveBackGround_Physics[] moveBackGround_Physics = null;


    private void Awake()
    {
        //해상도 600:1024 고정
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.SetResolution(1024, 600, true);
    }


    void Update () {


        //플레이어 게임 업데이트
        player.GameUpdate();
        for (int i=0; i< moveBackGround_Physics.Length; i++) {
            moveBackGround_Physics[i].GameUpdate();
        }

    }
}
