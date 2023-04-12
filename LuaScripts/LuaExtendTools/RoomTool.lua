
---room tool
function IsMaster()
    local localPlayer=Get_LocalPlayer()
    
    if localPlayer==nil then
        logError("玩家为空")
        return
    end
    
    print(localPlayer.gameObject.name.."ID号："..localPlayer.playerID.."是房主："..tostring(localPlayer.isMaster))
    if localPlayer.isMaster==true then
        return true
    end
    return false
end


function Get_LocalPlayer()
    local localPlayer = ParaPlayerService.GetLocalPlayer()
    if localPlayer ~= nil then
      print("玩家ID"..localPlayer.playerID)
    end
    return localPlayer
end


function IsOwner(obj)
    local boolV=obj:HasOwnership()
    logError("本地玩家是否拥有主权"..tostring(boolV))
    return boolV
end
