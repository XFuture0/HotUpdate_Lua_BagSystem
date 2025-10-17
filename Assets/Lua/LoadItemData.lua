--读取Json表数据
local txt = ABManager:LoadRes("json","ItemData",typeof(TextAsset))--加载json文件
local itemList = Json.decode(txt.text)--json解码(数组结构索引)
ItemData = {}
for index,value in pairs(itemList) do
	ItemData[value.id] = value
end--json表转存