math.randomseed(tostring(os.time()):reverse():sub(1, 7))

DataClass={}
DataClass.__index=DataClass
templateData=
{
    name=nil,
    number=nil,
    score=nil,
    time=nil,
}


function DataClass:new()
    local t={}
    self.__index = self
    setmetatable(t,self)
    return t
end


function AddObjToTable(targetVal)
    --if CheckObjValInTable(targetVal)==nil then
        table.insert( targetVal)
    --else
        --logError("已存在该玩家，没有插入到数据表中")
       -- return
    --end
end

function RemoveObjFromTable(targetVal)
    --if CheckObjValInTable(targetVal) ==nil then 
    --    return nil
    --else
        --local index=CheckObjValInTable(targetVal)
        table.remove(targetVal)
    --end
end

function CheckObjValInTable(targetVal)
    for k,v in pairs(DataClass) do
        if v.name==targetVal.name then
            return k
        end
    end
    return nil
end

function RandomObjDataGenerate(rangeStart,tangeEnd)
    local tempVal=math.random(rangeStart,tangeEnd)
    return tostring(tempVal)
end


function  DataGenAndInsert()
   local temp=templateData
   temp.playerID=
   string.char(
    RandomObjDataGenerate(97,122),
    RandomObjDataGenerate(97,122),
   RandomObjDataGenerate(97,122),
   RandomObjDataGenerate(97,122))

   temp.name=string.upper(string.char(RandomObjDataGenerate(97,122)))
            .."_"..string.char(RandomObjDataGenerate(97,122),RandomObjDataGenerate(97,122),RandomObjDataGenerate(97,122))
   temp.number=RandomObjDataGenerate(10,99)
   temp.score=RandomObjDataGenerate(100,999)
   temp.time=RandomObjDataGenerate(0,301)
   --logError("插入对象".."   name:"..temp.name.."   number:"..temp.number.."   score:"..temp.score.."   time:"..temp.time)
   return temp
end


function RandomRemove()
    local len=#DataClass
    local index=RandomObjDataGenerate(1,len)
    --logError("移除随机生成表中的第"..index.."个元素")
    return index
end