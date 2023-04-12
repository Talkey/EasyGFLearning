function InsertSort(tableList,sortOrder)
    local list=tableList
    if sortOrder==1 then
        for i=2,#list do
            local tmp=list[i]
            for j=i-1,0,-1 do
                if list[j]>tmp then
                    list[j+1]=list[j]
                    list[j]=tmp
                end
            end
        end
    else
        for i=2,#list do
            local tmp=list[i]
            for j=i-1,0,-1 do
                if list[j]<tmp then
                    list[j+1]=list[j]
                    list[j]=tmp
                end
            end
        end
    end
    return list
end


function Shell_Sort(playerDataList)
    local step=math.floor(#playerDataList/2)
    while step>0 do
        for i=step,#playerDataList,1 do
            local j=i;
            local temp=playerDataList[i]

            while j-step>0 and playerDataList[j-step]>temp do
                playerDataList[j]=playerDataList[j-step]
                j=j-step
            end
            playerDataList[j]=temp
        end
       
        step=math.floor(step/2)
    end
    return playerDataList
end


