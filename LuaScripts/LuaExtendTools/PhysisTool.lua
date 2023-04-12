function UnityRaycast(origin, direction, checkNum, checkDistance, checkLayers)
    if checkNum == nil or checkNum < 0 then
        checkNum = 1
    end
    if checkDistance == nil or checkDistance < 0 then
        checkDistance = math.maxinteger
    end
    if checkLayers == nil or checkLayers < 0 then
        checkLayers = -1
    end

    local results = CS.System.Array.CreateInstance(typeof(UnityEngine.RaycastHit), checkNum)
    local resultsNum = UnityEngine.Physics.RaycastNonAlloc(origin, direction, results, checkDistance, checkLayers, UnityEngine.QueryTriggerInteraction.Ignore)

    if resultsNum < 1 then
        return false, nil
    end

    if resultsNum == 1 then
        return true, results[0]
    end

    local minDistance = math.maxinteger
    local nearest = nil
    for i = 0, resultsNum - 1 do
        local distance = UnityEngine.Vector3.Distance(origin, results[i].transform.position)
        if minDistance > distance then
            minDistance = distance
            nearest = results[i]
        end
    end
    return true, nearest
end

function RayCast(originTrans,direction)
    local castHit={}
    



    
end