public var MainMenu:MainMenu;
public var Anim:Animator;


function Start () {

}

function Update () {

}


public function UpOK()
{
 MainMenu.logonisok=true;
 Anim.SetBool("ReadyOK",true);
}