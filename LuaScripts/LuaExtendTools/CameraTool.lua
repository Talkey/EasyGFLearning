--相机旋转的物体结构
--rootObj
    --rotateObj
        --Camera

rotateSpeed=40
rotateAngle=70

local Camera=UnityEngine.Camera
local Vector3=UnityEngine.Vecotor3
local Time=UnityEngine.Time

local rootObj=nil
local rotateObj=nil
local cameraObj=nil

local isRotating=false
local originalTransAngles=nil

function Awake()
    if _G["CameraTool"] ~= nil then
        UnityEngine.Object.Destroy(self.gameObject)
        return
    end
    _G["CameraTool"] = self.script
end


function Start()
    rootObj=self.gameObject
    rotateObj=Find_ChildGameObject_ByPath(rootObj,"CameraRotateRoot")
    originalTransAngles=rotateObj.transform.localEulerAngles
    cameraObj=Find_ChildGameObject_ByPath(rotateObj,"Camera")
    Obj_SetActive(cameraObj,false)
    SetCameraEnabled(false)
end


---开始旋转
function StartRotate()
    rotateObj.transform.localEulerAngles=originalTransAngles
    isRotating=true
end


function Update()
    if isRotating==true then
        RotateAround_Target(rotateSpeed,rotateAngle)
    end
end


function RotateAround_Target(tempRotateSpeed,targetAngle)
    local localPlayer=ParaPlayerService.GetLocalPlayer()
    local neck=localPlayer:GetBoneTransform(UnityEngine.HumanBodyBones.Neck)
    SetCameraEnabled(true)
    rotateObj.transform.position=neck.transform.position
    rotateObj.transform:Rotate(0,tempRotateSpeed*Time.deltaTime,0)

    if targetAngle<=rotateObj.transform.localEulerAngles.y then
        SetCameraEnabled(false)
        rotateObj.transform.localEulerAngles=originalTransAngles
        isRotating=false
    end
end


function SetCameraEnabled(boolVal)
    Obj_SetActive(cameraObj,boolVal)
    local cameraCompo=Get_ObjComponent(cameraObj,Camera)
    cameraCompo.enabled=boolVal
end



function SetPosAndDirection(targetDirection)
    rootObj.transform.forward=targetDirection
end