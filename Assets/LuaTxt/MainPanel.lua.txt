--主面板
BasePanel:subClass("MainPanel")
function MainPanel:Init(name)--实例化对象，找到控件，为控件加上监听
   self.base.Init(self,name)
   if self.isInitEvent == false then--为了只添加一次事件
	   self:GetControl("BtnRole","Button").onClick:AddListener(function()
	   	   self:btnRoleClick()
	   end)--内设方法传入self
	   isInitEvent = true
   end
end
function MainPanel:btnRoleClick()
	BackPanel:ShowMe("BackPanel")
end