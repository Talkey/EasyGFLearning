--gameobject extend tool

--通过类型查找目标物体挂载的组件,若无组件自动新增目标组件并返回(目标物体,目标组件类型)
function Get_ObjComponent( targetObj,compoType )
    if targetObj~=nil then
        local component=targetObj:GetComponent(typeof(compoType))
        
        if component~=nil then
            return component
        else
            logError("无法找到组件"..tostring(compoType)..",已为"..targetObj.name.."自动添加组件："..tostring(compoType))
            component=targetObj:AddComponent(typeof(compoType))
            if component~=nil then
                return component
            end
        end
    else
        logError("物体为空，无法查找组件")
    end
end

--通过路径查找子物体的组件(父物体,目标物体路径,目标组件类型)
function Get_ChildComponent( parentObj,childPath,compoType )
   local childObj= Find_GameObject_ByPath(parentObj,childPath)
   local component=Get_ObjComponent(childObj,compoType)
   return component
end

--通过路径查找该物体下的目标物体或目标子物体并返回(父物体,目标物体路径)
function Find_GameObject_ByPath(parentObj,targetObjPath)
    local tempTrans=parentObj.transform:Find(targetObjPath)
    if tempTrans==nil then
        logError(parentObj.gameObject.name.."脚本在路径找到不对对应物体"..targetObjPath)
        return nil
    end
    return tempTrans.gameObject
end

--返回该物体的所有子物体列表(父物体)
function Find_GObjs_ByPath(parentObj)
    local targetTable={}
    local list=parentObj:GetComponentsInChildren(typeof(UnityEngine.Transform))
    for i=1,list.Length-1 do
        table.insert(targetTable,list[i].gameObject)
    end
    return targetTable
end

--设置目标物体可见性(目标物体,可见性)
function Set_Obj_Active(targetObj,isVisable)
    if targetObj~=nil then
        targetObj:SetActive(isVisable)
    end
end

--设置目标物体的父物体(父物体,子物体)
function Set_Obj_Parent(parentObj,childObj)
    if parentObj~=nil and childObj~=nil then
        childObj.gameObject.transform=parentObj.transform
    end
end

--设置目标物体的缩放(缩放大小0~1)
function Set_Obj_Scale(targetObj,scaleVal)
    local targetTrans=targetObj.transform
    targetTrans.localScale=scaleVal
end

--通过预制体生成物体(预制体,父物体)
function Instantiate_ByPrefab(prefab,parentObj)
    if prefab==nil or parentObj==nil then
        logError("找不到物体")        
    return
    end
    local tempObj=UnityEngine.GameObject.Instantiate(prefab,parentObj.transform)
    return tempObj
end

---------待测试线


--通过路径生成物体(路径,父物体)**待测试
function Instantiate_ByPath(prefabPath,parentObj)
    local resourceObj=UnityEngine.Resources.Load(prefabPath)
    local tempObj=Instantiate_ByPrefab(resourceObj,parentObj)
    return tempObj
end

