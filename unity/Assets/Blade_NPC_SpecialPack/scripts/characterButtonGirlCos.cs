
	
using UnityEngine;
using System.Collections;

public class characterButtonGirlCos : MonoBehaviour {

	public GameObject frog;
	
	
	
	private Rect FpsRect ;
	private string frpString;
	
	private GameObject instanceObj;
	public GameObject[] gameObjArray=new GameObject[9];
	public AnimationClip[] AniList  = new AnimationClip[4];
	
	float minimum = 2.0f;
	float maximum = 50.0f;
	float touchNum = 0f;
	string touchDirection ="forward"; 
	private GameObject Villarger_A_Girl_prefab;
	
	// Use this for initialization
	void Start () {
		
		//frog.animation["dragon_03_ani01"].blendMode=AnimationBlendMode.Blend;
		//frog.animation["dragon_03_ani02"].blendMode=AnimationBlendMode.Blend;
		//Debug.Log(frog.GetComponent("dragon_03_ani01"));
		
		//Instantiate(gameObjArray[0], gameObjArray[0].transform.position, gameObjArray[0].transform.rotation);
	}
	
void OnGUI() {
	  if (GUI.Button(new Rect(20, 20, 70, 40),"Idle")){
		 frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("BG_Idle");
	  }
	    if (GUI.Button(new Rect(90, 20, 70, 40),"Walk")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("BG_Walk");
	  }
		  if (GUI.Button(new Rect(160, 20, 70, 40),"L_Walk")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("BG_L_Walk");
	  }
		  if (GUI.Button(new Rect(230, 20, 70, 40),"R_Walk")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("BG_R_Walk");
	  }
		  if (GUI.Button(new Rect(300, 20, 70, 40),"B_Walk")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("BG_B_Walk");
	  }
	     if (GUI.Button(new Rect(370, 20, 70, 40),"Talk")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("BG_Talk");
	  } 
		 if (GUI.Button(new Rect(440, 20, 70, 40),"Talk01")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("BG_Talk01");
	  } 
		if (GUI.Button(new Rect(510, 20, 70, 40),"Run")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("BG_Run");
	  }  
		if (GUI.Button(new Rect(580, 20, 70, 40),"L_Run")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("BG_L_Run");
	  }  
		if (GUI.Button(new Rect(650, 20, 70, 40),"R_Run")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("BG_R_Run");
	  }  
		if (GUI.Button(new Rect(720, 20, 70, 40),"B_Run")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("BG_B_Run");
	 }  
			if (GUI.Button(new Rect(790, 20, 70, 40),"Jump")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("BG_Jump");
	  } 
		if (GUI.Button(new Rect(860, 20, 70, 40),"Draw Blade")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Once;
		  	frog.GetComponent<Animation>().CrossFade("BG_DrawBlade");
	  } 
		if (GUI.Button(new Rect(20, 65, 70, 40),"Put Blade")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Once;
		  	frog.GetComponent<Animation>().CrossFade("BG_PutBlade");
	  } 
		if (GUI.Button(new Rect(90, 65, 70, 40),"AtkStandy")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("BG_AttackStandy");
	  } 
		if (GUI.Button(new Rect(160, 65, 70, 40),"Attack00")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("BG_Attack00");
	  } 
		if (GUI.Button(new Rect(230, 65, 70, 40),"Attack01")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("BG_Attack01");
			
		}	
		if (GUI.Button(new Rect(300, 65, 70, 40),"Block")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("BG_Block");
	  } 
		
			if (GUI.Button(new Rect(370, 65, 70, 40),"BlockAttack")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("BG_BlockAttack");
	  } 
			if (GUI.Button(new Rect(440, 65, 70, 40),"Combo1")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("BG_Combo1");
	  }
				if (GUI.Button(new Rect(510, 65, 70, 40),"Combo1_1")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Once;
		  	frog.GetComponent<Animation>().CrossFade("BG_Combo1_1");
	  }
				if (GUI.Button(new Rect(580, 65, 70, 40),"Combo1_2")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Once;
		  	frog.GetComponent<Animation>().Play("BG_Combo1_2");
	  }
				if (GUI.Button(new Rect(650, 65, 70, 40),"Combo1_3")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Once;
		  	frog.GetComponent<Animation>().CrossFade("BG_Combo1_3");
	  }
		if (GUI.Button(new Rect(720, 65, 70, 40),"kick")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("BG_Kick");
	  }
			if (GUI.Button(new Rect(790, 65, 70, 40),"Skill")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("BG_Skill");
	  }
			if (GUI.Button(new Rect(860, 65, 70, 40),"M_Avoid")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("BG_M_Avoid");
	  }
			if (GUI.Button(new Rect(20, 110, 70, 40),"L_Avoid")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("BG_L_Avoid");
	  }
			if (GUI.Button(new Rect(90, 110, 70, 40),"R_Avoid")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("BG_R_Avoid");
	  }
			if (GUI.Button(new Rect(160, 110, 70, 40),"Buff")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("BG_Buff");
	  }
		if (GUI.Button(new Rect(230, 110, 70, 40),"Run01")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("BG_Run01");
	  }
		if (GUI.Button(new Rect(300, 110, 70, 40),"RunAttack")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Once;
		  	frog.GetComponent<Animation>().CrossFade("BG_RunAttack");
	  }
		if (GUI.Button(new Rect(370, 110, 70, 40),"L_Run01")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("BG_L_Run01");
	  }
		if (GUI.Button(new Rect(440, 110, 70, 40),"R_Run01")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("BG_R_Run01");
	  }
		if (GUI.Button(new Rect(510, 110, 70, 40),"B_Run01")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("BG_B_Run01");
	  }
		
		if (GUI.Button(new Rect(580, 110, 70, 40),"Jump01")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("BG_Jump01");
	  }
		if (GUI.Button(new Rect(650, 110, 70, 40),"PickUp")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("BG_Pickup");
	  }
		
			if (GUI.Button(new Rect(720, 110, 70, 40),"Damage")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("BG_Damage");
	  }
			if (GUI.Button(new Rect(790, 110, 70, 40),"Death")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("BG_Death");
	  }
		if (GUI.Button(new Rect(860, 110, 70, 40),"GangnamStyle")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("BG_GangnamStyle");
	  }
	
		
		/////////////////////////////////////////////////////////////////////
		
			if (GUI.Button(new Rect(20, 700, 120, 40),"Blad_Warrior")){
	       Application.LoadLevel(16);
	 }
		if (GUI.Button(new Rect(150, 700, 70, 40),"S01")){
	       Application.LoadLevel(16);
	 }  
		if (GUI.Button(new Rect(220, 700, 70, 40),"S02")){
	       Application.LoadLevel(17);
	 } 
	
		
	 ///////////////////////////////////////////////////////////////////////////////
		
				if (GUI.Button(new Rect(20, 740, 120, 40),"Blad_Girl")){
	       Application.LoadLevel(18);
	 }
			if (GUI.Button(new Rect(150, 740, 70, 40),"S01")){
	       Application.LoadLevel(18);
	 }  
		if (GUI.Button(new Rect(220, 740, 70, 40),"S02")){
	       Application.LoadLevel(19);
	 } 
	
	//////////////////////////////////////////////////////////////////////////////
			if (GUI.Button(new Rect(20, 520, 120, 40),"V  1.4")){
	            frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		     	frog.GetComponent<Animation>().CrossFade("BG_Chibi_Idle");
	 } 	
		
		if (GUI.Button(new Rect(20, 560, 120, 40),"Equipment")){
	       Application.LoadLevel(0);
	 } 
	
		
		if (GUI.Button(new Rect(20, 600, 120, 40),"Costume")){
	       Application.LoadLevel(16);
	 } 
			if (GUI.Button(new Rect(20, 640, 120, 40),"Chibi")){
	       Application.LoadLevel(20);
	 } 
		
		
		//		if (GUI.Button(new Rect(580, 440, 140, 40),"V  1.2")){
		//  frog.animation.wrapMode= WrapMode.Loop;
		//  	frog.animation.CrossFade("Idle");
	 // } 
			//	if (GUI.Button(new Rect(640, 540, 140, 40),"Ver 2.0")){
		// frog.animation.wrapMode= WrapMode.Loop;
		 // 	frog.animation.CrossFade("BW_Idle");
	  //}
		
		
 }
	
	// Update is called once per frame
	void Update () {
		
		//if(Input.GetMouseButtonDown(0)){
		
			//touchNum++;
			//touchDirection="forward";
		 // transform.position = new Vector3(0, 0,Mathf.Lerp(minimum, maximum, Time.time));
			//Debug.Log("touchNum=="+touchNum);
		//}
		/*
		if(touchDirection=="forward"){
			if(Input.touchCount>){
				touchDirection="back";
			}
		}
	*/
		 
		//transform.position = Vector3(Mathf.Lerp(minimum, maximum, Time.time), 0, 0);
	if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
		//frog.transform.Rotate(Vector3.up * Time.deltaTime*30);
	}
	
}
