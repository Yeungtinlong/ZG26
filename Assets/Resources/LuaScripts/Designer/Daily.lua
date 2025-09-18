---
--- Created by Yeung.
--- DateTime: 2025/9/12 15:21
---

local TimelineNode = CS.MBF.TimelineNode;
local GameLuaInterface = CS.TheGame.GM.GameLuaInterface;
local ItemStack = CS.TheGame.ItemStack;

local function GetItem(timelineObj, params)
    local itemWillGet = params[0];
    GameLuaInterface.GetItem(itemWillGet.id, itemWillGet.count);
    return true;
end

local function GetRole(timelineObj, params)
    local chaId = params[0];
    GameLuaInterface.UnlockCharacter(chaId);
    return true;
end

local daily = {
    { day = 1, icon = "Roles/ui_cha_xiahoudun", description = "武将·夏侯惇", effect = TimelineNode(0, GetRole, { "cha_xiahoudun" }) },
    { day = 2, icon = "Items/ui_head_item_currency_coin", description = "金币x100", effect = TimelineNode(0, GetItem, { ItemStack("item_currency_coin", 100) }) },
    { day = 3, icon = "Items/ui_head_item_currency_coin", description = "金币x200", effect = TimelineNode(0, GetItem, { ItemStack("item_currency_coin", 200) }) },
    { day = 4, icon = "Items/ui_head_item_currency_coin", description = "金币x300", effect = TimelineNode(0, GetItem, { ItemStack("item_currency_coin", 300) }) },
    { day = 5, icon = "Items/ui_head_item_currency_coin", description = "金币x400", effect = TimelineNode(0, GetItem, { ItemStack("item_currency_coin", 400) }) },
    { day = 6, icon = "Items/ui_head_item_currency_coin", description = "金币x500", effect = TimelineNode(0, GetItem, { ItemStack("item_currency_coin", 500) }) },
    { day = 7, icon = "Roles/ui_cha_zhugeliang", description = "武将·诸葛亮", effect = TimelineNode(0, GetRole, { "cha_zhugeliang" }) },
};

return daily;
