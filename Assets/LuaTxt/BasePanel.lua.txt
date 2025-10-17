Object:subClass("BasePanel")
BasePanel.panelObj = nil
BasePanel.controls = {}
BasePanel.isInitEvent = false
function BasePanel:Init(name)
   if self.panelObj == nil then
      self.panelObj = ABManager:LoadRes("ui",name,typeof(GameObject))
      self.panelObj.transform:SetParent(Canvas,false)
      local AllControls = self.panelObj:GetComponentsInChildren(typeof(UIBehaviour))--获得的是整个游戏对象
      --为了避免找各种无用控件，定义一个规则，拼面板时，控件命名要按照一定规范来
      for i = 1,AllControls.Length-1 do
      	local typeName = AllControls[i]:GetType().Name--通过反射获得当前控件的类名
      	 if self.controls[AllControls[i].name] ~= nil then--防止一个对象上有多个Ui进行覆盖
      	 	self.controls[AllControls[i].name][typeName] = AllControls[i]--通过自定义索引存储控件
      	 else
            self.controls[AllControls[i].name] = {[typeName] = AllControls[i]}
      	 end
      end
   end
end
function BasePanel:GetControl(name,typeName)
	if self.controls[name] ~= nil then
       local sameNameControls =self.controls[name]
       if sameNameControls[typeName] ~= nil then
       	  return sameNameControls[typeName]
       end
	end
	return nil
end
function BasePanel:ShowMe(Name)
    self:Init(Name)
    self.panelObj:SetActive(true)
end
function BasePanel:HideMe()
	self.panelObj:SetActive(false)
end