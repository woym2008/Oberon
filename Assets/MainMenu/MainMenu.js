public var ring:Animator;
public var core:Animator;
public var HintText:Animator;
public var PlayerPoints:Animator;
public var Anim:Animator;



public var Dholding:boolean;
public var Fholding:boolean;


public var logonisok:boolean;
public var gotorelease:boolean;


function Start () {

}

function Update () 
{

   if (Input.GetButton("Ready"))
 {
   Dholding=true;
   

 } 
  if(Input.GetButtonUp ("Ready"))
 {
   Dholding=false;
 
 }
 

    if (Input.GetButton("Go"))
    {     
     core.SetBool("boost",true);
     ring.SetBool("SpeedUp",true);
    }
    
    if (Input.GetButtonUp("Go"))
    {     
     core.SetBool("boost",false);
     ring.SetBool("SpeedUp",false);
    }
    
   if(gotorelease==true)
   {  //change hint text 
     HintText.SetTrigger("Next2");
   }
     
   if (gotorelease==true&&Input.GetButtonUp("Go"))
    {     
      //ready to release ,then the level will jump
       this.Anim.SetTrigger("MaskRun");  // mask  go  and then level jump~
    }

    
    
    

   ///color change random
  
 ///////////level3


///////////
   if(Dholding==false)
   {
   PlayerPoints.SetBool("Up",false);
   
   }
   
   if(Dholding==true)
   {
   PlayerPoints.SetBool("Up",true);
   
   }
   
   if(logonisok==true)
    {
      HintText.SetTrigger("Next1");     //set the ring  go to zoom and rotate
      ring.SetTrigger("Up");
          // set the hint text to change  
      
    
    }








}









/////////////////////////////////////////////////

public function NextLevel()
{
  Application.LoadLevel(1);

}

