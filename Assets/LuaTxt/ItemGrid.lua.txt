Object:subClass("ItemGrid")
ItemGrid.obj = nil
ItemGrid.imgIcon = nil
ItemGrid.Text = nil
function ItemGrid:Init(father,Posx,Posy)
   self.obj = ABManager:LoadRes("ui","ItenGrid",typeof(GameObject))
   self.obj.transform:SetParent(father,false)
   self.obj.transform.localPosition = Vector3(Posx,Posy,0)
   self.imgIcon = self.obj.transform:Find("ImgIcon"):GetComponent(typeof(Image))
   self.Text = self.obj.transform:Find("Text"):GetComponent(typeof(Text))
end
function ItemGrid:Initdata(Data)
	local data = ItemData[Data.id]
    local strs = string.split(data.icon,"_")
    local spriteAtlas = ABManager:LoadRes("ui",strs[1],typeof(SpriteAtlas))
    self.imgIcon.sprite = spriteAtlas:GetSprite(strs[2])
    self.Text.text = Data.num
end
function ItemGrid:Destroy()
	GameObject.Destroy(self.obj)
	self.obj = nil
end