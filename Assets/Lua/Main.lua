print("Start On")
require("Object")
require("InitClass")
require("PlayerData")
require("BasePanel")
PlayerData:Init()
require("LoadItemData")
--玩家信息
--1.从本地读取
--2.从服务器读取
require("MainPanel")
require("BackPanel")
require("ItemGrid")
MainPanel:ShowMe("Panel")