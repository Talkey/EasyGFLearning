local UI=UnityEngine.UI
local Canvas=UnityEngine.Canvas
local Button=UI.Button
local Text=UI.Text
local Color=UnityEngine.Color
local Image=UnityEngine.UI.Image

--通过路径注册按钮(parentObj,targetBtn,btnPath,btnFunc)
function Button_Register_ByPath(parentObj,targetBtn,btnPath, btnFunc)
    local btnObj=Find_GameObject_ByPath(parentObj,btnPath)
    if btnObj~=nil then
        targetBtn=Get_ObjComponent(btnObj,Button)
        targetBtn.onClick:AddListener(btnFunc)
    end
end

--添加按钮监听事件(targetBtnObj,btnFunc)
function Button_AddListener(targetBtn,btnFunc)
    
    if targetBtn~=nil then
        local targetCompo=Get_ObjComponent(targetBtn,Button)
        targetCompo.onClick:AddListener(btnFunc)
    else
        logError("UITool按钮物体添加监听事件失败")
    end
end

--移除按钮监听事件(targetBtnObj,btnFunc)
function Button_RemoveListener(targetBtn,btnFunc)
    if targetBtn~=nil then
        targetBtn.onClick:RemoveListener(btnFunc)
    end
end

--更新文本Obj的内容(targetTexObj,context)
function UpdateText(targetTexObj,context)
    local texCompo=Get_ObjComponent(targetTexObj,Text)
    texCompo.text=context
end

--拼接文本字符串(targetTexObj,内容,解释,单位)
function Concat_TextStr(targetTexObj,context,explanation,unit)
    local texCompo=Get_ObjComponent(targetTexObj,Text)
    if texCompo~=nil then
        texCompo.text=explanation..context..unit
    end
end

--设置Canvas画布层级(targetCanvas,sortOrder)
function Set_Canvas_SortLayer(targetCanvas,sortOrder)
    if targetCanvas~=nil then
        local canvasCompo=Get_ObjComponent(targetCanvas,Canvas)
        canvasCompo.sortingOrder=sortOrder
    end
end




