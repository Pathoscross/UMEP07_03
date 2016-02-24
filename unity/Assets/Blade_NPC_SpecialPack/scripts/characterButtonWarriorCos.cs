
	
using UnityEngine;
using System.Collections;

public class characterButtonWarriorCos : MonoBehaviour {

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
		  	frog.GetComponent<Animation>().CrossFade("BW_Idle");
	  }
	    //if (GUI.Button(new Rect(90, 20, 70, 40),"Greeting")){
		 // frog.animation.wrapMode= WrapMode.Loop;
		  //	frog.animation.CrossFade("Greeting");
	 // }
		   if (GUI.Button(new Rect(90, 20, 70, 40),"Walk")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("BW_Walk");
	  }
		   if (GUI.Button(new Rect(160, 20, 70, 40),"L_Walk")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("BW_L_Walk");
	  }
		   if (GUI.Button(new Rect(230, 20, 70, 40),"R_Walk")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("BW_R_Walk");
			
	  }
		   if (GUI.Button(new Rect(300, 20, 70, 40),"B_Walk")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("BW_B_Walk");
	  }
	     if (GUI.Button(new Rect(370, 20, 70, 40),"Talk")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("BW_Talk");
	  } 
		
	     if (GUI.Button(new Rect(440, 20, 70, 40),"Talk01")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("BW_Talk01");
	  } 
		if (GUI.Button(new Rect(510, 20, 70, 40),"Run")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("BW_Run");
	  } 
			if (GUI.Button(new Rect(580, 20, 70, 40),"L_Run")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("BW_L_Run");
	  }
			if (GUI.Button(new Rect(650, 20, 70, 40),"R_Run")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("BW_R_Run");
	  }
			if (GUI.Button(new Rect(720, 20, 70, 40),"B_Run")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("BW_B_Run");
	  }
			if (GUI.Button(new Rect(790, 20, 70, 40),"Jump")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("BW_Jump");
	  }
			if (GUI.Button(new Rect(860, 20, 70, 40),"DrawBlade")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Once;
		  	frog.GetComponent<Animation>().CrossFade("BW_DrawBlade");
	  } 
			if (GUI.Button(new Rect(20, 60, 70, 40),"PutBlade")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Once;
		  	frog.GetComponent<Animation>().CrossFade("BW_PutBlade");
	  } 
			if (GUI.Button(new Rect(90, 60, 70, 40),"ATK_standy")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("BW_AttackStandy");
	  }
			if (GUI.Button(new Rect(160, 60, 70, 40),"Attack00")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("BW_Attack00");
	  }
			if (GUI.Button(new Rect(230, 60, 70, 40),"Attack01")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("BW_Attack01");
	  }
		if (GUI.Button(new Rect(300, 60, 70, 40),"Block")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("BW_Block");
	  }
			if (GUI.Button(new Rect(370, 60, 70, 40),"BlockAttack")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("BW_BlockAttack");
	  }
			if (GUI.Button(new Rect(440, 60, 70, 40),"Combo1")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("BW_Combo1");
	  }
			if (GUI.Button(new Rect(510, 60, 70, 40),"Combo1_1")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("BW_Combo1_1");
	  }
			if (GUI.Button(new Rect(580, 60, 70, 40),"Combo1_2")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Once;
		  	frog.GetComponent<Animation>().CrossFade("BW_Combo1_2");
	  }
			if (GUI.Button(new Rect(650, 60, 70, 40),"Combo1_3")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Once;
		  	frog.GetComponent<Animation>().CrossFade("BW_Combo1_3");
	  }
			if (GUI.Button(new Rect(720, 60, 70, 40),"Kick")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Once;
		  	frog.GetComponent<Animation>().CrossFade("BW_Kick");
	  }
				if (GUI.Button(new Rect(790, 60, 70, 40),"Skill")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  frog.GetComponent<Animation>().CrossFade("BW_Skill");
		
	  }
				if (GUI.Button(new Rect(860, 60, 70, 40),"M_Avoid")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  frog.GetComponent<Animation>().CrossFade("BW_M_Avoid");
		
	  }
			if (GUI.Button(new Rect(20, 100, 70, 40),"L_Avoid")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("BW_L_Avoid");
	  }
				if (GUI.Button(new Rect(90, 100, 70, 40),"R_Avoid")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  frog.GetComponent<Animation>().CrossFade("BW_R_Avoid");
		
	  }
				if (GUI.Button(new Rect(160, 100, 70, 40),"Buff")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  frog.GetComponent<Animation>().CrossFade("BW_Buff");
		
	  }
			if (GUI.Button(new Rect(230, 100, 70, 40),"Run01")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("BW_Run01");
	  }
		if (GUI.Button(new Rect(300, 100, 70, 40),"RunAttack")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Once;
		  	frog.GetComponent<Animation>().CrossFade("BW_RunAttack");
	  }
		
		if (GUI.Button(new Rect(370, 100, 70, 40),"L_Run01")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("BW_L_Run01");
	  }
		if (GUI.Button(new Rect(440, 100, 70, 40),"R_Run01")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("BW_R_Run01");
	  }
		if (GUI.Button(new Rect(510, 100, 70, 40),"B_Run01")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("BW_B_Run01");
	  }
		if (GUI.Button(new Rect(580, 100, 70, 40),"Jump01")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("BW_Jump01");
	  }
		if (GUI.Button(new Rect(650, 100, 70, 40),"PickUp")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("BW_PickUp");
	  }
			if (GUI.Button(new Rect(720, 100, 70, 40),"Damage")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("BW_Damage");
	  }
			if (GUI.Button(new Rect(790, 100, 70, 40),"Death")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Once;
		  	frog.GetComponent<Animation>().CrossFade("BW_Death");
	  }
		if (GUI.Button(new Rect(860, 100, 70, 40),"Gentleman")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("BW_Gentleman");
	  }
		/////////////////////////////////////////////////////////////////////
		
	
		
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
		
		//////////////////////////////////////////////////////////////////
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
