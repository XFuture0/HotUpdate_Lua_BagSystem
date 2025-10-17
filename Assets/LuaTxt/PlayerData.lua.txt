--保存玩家信息
PlayerData = {}
PlayerData.equips = {}
PlayerData.items = {}
PlayerData.gems = {}
function PlayerData:Init()--初始化
	table.insert(self.equips,{id = 1,num = 1})
	table.insert(self.equips,{id = 2,num = 1})
	table.insert(self.items,{id = 3,num = 5})
	table.insert(self.items,{id = 4,num = 2})
	table.insert(self.gems,{id = 5,num = 3})
	table.insert(self.gems,{id = 6,num = 4})
end