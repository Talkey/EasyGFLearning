local Canvas=UnityEngine.Canvas
local UI=UnityEngine.UI
local Button=UI.Button
local Text=UI.Text
local Color=UnityEngine.Color
local Image=UI.Image

--七色
ColorTable=
{
    SkyBlue    =Color(0,1,1,1),
    Green      =Color(0,1,0,1),
    Yellow     =Color(1,1,0,1),
    Red        =Color(1,0,0,1),
    Purple     =Color(1,0,1,1),
    DeepBlue   =Color(0,0,1,1),
}


--设置组件底色(目标UI,最终颜色,目标组件类型) 用于Text和Img
function SetColor(targetUI,targetColor,uiCompo)
    if targetUI~=nil and targetColor~=nil then
        local compo=Get_ObjComponent(targetUI,uiCompo)
        compo.color=targetColor
    else
        logError("颜色错误或无法找到对应UI")
    end
end

--图片底色过渡(目标图片,最终底色,过渡速度)
function Trans_ImgColor(targetImage,endColor,transSpeed)
    if targetImage==nil then
        logError("没有找到目标渐变需要的Color组件")
        return 
    end
    local tempCompo=Get_ObjComponent(targetImage,Image)
    tempCompo.color=Color.Lerp(endColor,tempCompo.color,transSpeed)
end

--图片透明度过渡(目标图片,最终透明度,过渡速度)
function Trans_ImgAlpha(targetImage,endAlpha,transSpeed)
    local tempCompo=Get_ObjComponent(targetImage,Image)

end


--案例：四色转换
function FourColorTrans(timeCountImage,fillAmount)
    if fillAmount<0.25 then
        ImgColorTrans(timeCountImage,UnityEngine.Color(1,0,0,1),fillAmount*4)
    elseif fillAmount<0.5 then
        ImgColorTrans(timeCountImage,UnityEngine.Color(1,1,0,1),(fillAmount-0.25)*4)
    elseif fillAmount<0.75 then
        ImgColorTrans(timeCountImage,UnityEngine.Color(0,1,0,1),(fillAmount- 0.5)*4)
    elseif fillAmount<1 then
        ImgColorTrans(timeCountImage,UnityEngine.Color(0,1,1,1),(fillAmount-0.75)*4)
    end
end