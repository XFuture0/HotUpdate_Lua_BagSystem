--背包面板
BasePanel:subClass("BackPanel")
BackPanel.Content = nil
BackPanel.Items = {}--记录格子
BackPanel.Nowtype = -1
function BackPanel:Init(name)
	self.base.Init(self,name)
	if self.isInitEvent == false then
		self.Content = self:GetControl("SVBack","ScrollRect").transform:Find("Viewport"):Find("Content")
		self:GetControl("BKButton","Button").onClick:AddListener(function()
			self:HideMe()
	    end)
	    self:GetControl("Equip","Toggle").onValueChanged:AddListener(function(value)
	    	if value == true then
	    		self:ChangeType(1)
	    	end
	    end)
	    self:GetControl("Item","Toggle").onValueChanged:AddListener(function(value)
	    	if value == true then
	    		self:ChangeType(2)
	    	end
	    end)
	    self:GetControl("Gem","Toggle").onValueChanged:AddListener(function(value)
	    	if value == true then
	    		self:ChangeType(3)
	    	end
	    end)
	    isInitEvent = true
	end
end
function BackPanel:ShowMe(name)
	self.base.ShowMe(self,name)
	if self.Nowtype == -1 then
		self:ChangeType(1)
	end
end
function BackPanel:ChangeType(type)
	if(self.Nowtype == type) then
		return
	end
	self.Nowtype = type
	for i = 1,#self.Items do
		self.Items[i]:Destroy()
	end
	self.Items = {}
	local nowItems = nil
   if type == 1 then
       nowItems = PlayerData.equips
   elseif type == 2 then
       nowItems = PlayerData.items
   else
       nowItems = PlayerData.gems
   end
   for i = 1,#nowItems do
   	local grid = ItemGrid:new()
   	grid:Init(self.Content,(i-1)%3*160,math.floor((i-1)/4)*160)
   	grid:Initdata(nowItems[i])
   	table.insert(self.Items,grid)
   end
end