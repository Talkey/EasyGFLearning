local Vector3=UnityEngine.Vector3
local Transform=UnityEngine.Transform

--计算返回两个物体之间的距离(obj0,obj1)
function Distance_Between(obj0,obj1)
    if obj0==nil or obj1==nil then
        logError("DistanceBetween调用：物体为空")
        return nil
    end
    return Vector3.Distance(obj0.transform.position,obj1.transform.position)
end

------------------------------------------待测试线-----------------------------------------------------

--计算返回物体0到物体1的方向(物体0,物体1)
function Direction_Between(obj0,obj1)
    if obj0==nil or obj1==nil then
        logError("Direction_Between调用：物体为空")
        return 
    end
    local direction=Vector3(obj1.transform.position.x-obj0.transform.position.x,
                            obj1.transform.position.y-obj0.transform.position.y,
                            obj1.transform.position.z-obj0.transform.position.z)
    return direction
end

--向目标移动并返回是否到达目的地(移动物体,目标位置,移动速度)
function MoveToTarget(moveObj,moveTargtPos,moveSpeed)
    if moveObj==nil or moveDestination==nil then
        logError("MoveTowards调用：物体为空")
        return 
    end
    moveObj.transform.position=Vector3.MoveTowards(
        moveObj.transform.position,
        moveTargtPos,
        moveSpeed*UnityEngine.Time.deltaTime
    )

end


--判断是否到达目标点
function ReachedTargetPos(moveObj,moveTargtPos)
    if Vector3.Distance(moveObj.transform.position,moveTargtPos) < 0.01 then
        return true
    else
        return false
    end
end


--案例:循环移动
function CicleMove(moveObj,startPos,endPos,moveSpeed)
    local currentMoveTarget=startPos
    if moveObj.activeInHierarchy ~=true then
        return
    end
    if ReachedTargetPos(moveObj,currentMoveTarget)==false then
        MoveToTarget(moveObj,currentMoveTarget,moveSpeed)
    else
        if currentMoveTarget==startPos then
            currentMoveTarget=endPos
        else
            currentMoveTarget=startPos
            
        end
        MoveToTarget(moveObj,currentMoveTarget,moveSpeed)
    end
end
