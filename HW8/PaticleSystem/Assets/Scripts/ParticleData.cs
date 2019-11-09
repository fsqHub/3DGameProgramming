using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleData 
{
    public float angle,   //粒子角度
    			 iniRadius, // 初始半径
    			 CurRadius;  // 当前半径

    public ParticleData(float angle_, float radius_) { //初始化
	    angle = angle_;
	    iniRadius = CurRadius = radius_;
  	}

  	public void setRadius(float radius_){
  		CurRadius = radius_;
  	}

  	public void setAngle(float angle_){
  		angle_ = angle_;
  	}
}
