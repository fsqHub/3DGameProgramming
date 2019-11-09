using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PHalo : MonoBehaviour
{
    
	public ParticleSystem particleSystem; 
	private ParticleSystem.Particle[] particles; //粒子数组
    private ParticleData[] pDatas;   //粒子属性数组

    //public Camera camera;
    public int pCount = 5000; // 粒子数目
    public float maxRadius = 3.5f; // 原始最大半径
    public float minRadius = 3f;  // 原始最小半径
    public float speed = 0.5f;    //粒子运动速度
    public float speedDiff = 3; //速度差分值

    public int flag;   //用以辨别粒子光环是否需要改变大小
    					//0则表示需要回到初始大小，1则表示将光环变小，2表示变大
    public float tranSpeed = 1f;    //光环大小变化时粒子的变化速度

    // Start is called before the first frame update
    void Start()
    {
    	//数据初始化
        particleSystem = this.GetComponent<ParticleSystem>();
        particles = new ParticleSystem.Particle[pCount];
        pDatas = new ParticleData[pCount]; 
                
        particleSystem.maxParticles = pCount;
    	particleSystem.Emit(pCount);
        particleSystem.GetParticles(particles);

        flag = 0;

        ///for (int i = 0; i < 4  ; i++)
       // {
            //将光环分为3部分，第一部分宽度占1/4，第二部分也占1/4，第三部分占1/2
            float radius,angle,midRadius1,midRadius2;
            midRadius1 = minRadius + (maxRadius - minRadius)/4;
            midRadius2 = maxRadius + (maxRadius - minRadius)/2;
            
            //给三部分的粒子赋予初始值，第一部分的离子数占1/10
            for (int j = 0 ; j < pCount/10 ; j ++){
            	angle = Random.Range(0.0f, 360.0f);
            	radius = Random.Range(minRadius, midRadius1);
            	pDatas[j] = new ParticleData(angle,radius);
            }
            //第二部分的离子数占4/5
            for (int j = 0 ; j < 4 * pCount/5 ; j ++){
            	angle = Random.Range(0.0f, 360.0f);
            	radius = Random.Range(midRadius1, midRadius2);
            	pDatas[pCount/10 + j] = new ParticleData(angle,radius);
            }
            //第三部分的离子数占1/10
            for (int j = 0 ; j < pCount/10 ; j ++){
            	angle = Random.Range(0.0f, 360.0f);
            	radius = Random.Range(midRadius2, maxRadius);
            	pDatas[9 * pCount/10 + j] = new ParticleData(angle,radius);
            }
           
        }
    //}

    // Update is called once per frame
    void Update()
    {
        
        for (int i = 0 ; i < pCount ; i++){
        	
        	if (flag == 1){ //光环半径变为初始值的0.6，是一个渐变过程
        		if (pDatas[i].CurRadius > (0.6f * pDatas[i].iniRadius)){
        			pDatas[i].CurRadius -= tranSpeed * Time.deltaTime;
        		}
        	}else if (flag == 2){ //光环半径变为原来的1.6，，是一个渐变过程
        		if (pDatas[i].CurRadius < (1.6f * pDatas[i].iniRadius)){
        			pDatas[i].CurRadius += tranSpeed * Time.deltaTime;
        		}
        	}
        	else if (flag == 0){ //光环半径渐变为初始大小
        		if (pDatas[i].CurRadius > pDatas[i].iniRadius){
        			pDatas[i].CurRadius -= tranSpeed * Time.deltaTime;
        		}
        		if (pDatas[i].CurRadius < pDatas[i].iniRadius){
        			pDatas[i].CurRadius += tranSpeed * Time.deltaTime;
        		}
        	}
        	
        	float angle_ = pDatas[i].angle;
        	// 一半的粒子顺时针旋转，一半的粒子逆时针旋转 
            if (i % 2 == 0){   
                angle_  += ((i % speedDiff + 1) * speed) % 360;  
            } else{  
                angle_  -= ((i % speedDiff + 1) * speed) % 360;  
            }  
            pDatas[i].angle = angle_;

            // 更新坐标
            float rad = pDatas[i].angle / 180 * Mathf.PI; 
            particles[i].position = new Vector3(pDatas[i].CurRadius * Mathf.Cos(rad), pDatas[i].CurRadius * Mathf.Sin(rad), 0f);  
        }

        particleSystem.SetParticles(particles, pCount);       
    }
    public void SetFlag(int flag_){
    	flag = flag_;
    }
   /* public void HaloChange(float minRadius_, float maxRadius_,bool larger){
    	maxRadius = maxRadius_;
    	minRadius = minRadius_;

    }*/
}
