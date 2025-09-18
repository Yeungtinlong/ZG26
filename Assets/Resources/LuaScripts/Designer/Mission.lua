local GameRuntimeData = CS.TheGame.GM.GameRuntimeData;
local GameLuaInterface = CS.TheGame.GM.GameLuaInterface;
local TimelineNode = CS.MBF.TimelineNode;
local ItemStack = CS.TheGame.ItemStack;

local function GetItems(timelineObj, params)
    local itemsWillGet = params[0];
    for _, itemStack in pairs(itemsWillGet) do
        GameLuaInterface.GetItem(itemStack.id, itemStack.count);
    end
    return true;
end

local function GetRoles(timelineObj, params)
    local chaIds = params[0];
    for _, chaId in pairs(chaIds) do
        GameLuaInterface.UnlockCharacter(chaId);
    end
    return true;
end


local missions = {
    {
        id = "mission_first",
        name = "桃园结义",
        description = "击败黄巾残兵！",
        icon = "Roles/ui_cha_liubei",
        canComplete = function()
            return GameRuntimeData.Instance.SelectedLevel > 1;
        end,
        showRewards = { ItemStack("item_currency_coin", 100) },
        onClaim = TimelineNode(0, GetItems, { ItemStack("item_currency_coin", 100) })
    },
    {
        id = "mission_lvbu",
        name = "虎牢关之战",
        description = "吕布镇守虎牢关，务必将其击败！",
        icon = "Roles/ui_cha_lvbu",
        canComplete = function()
            return GameRuntimeData.Instance.SelectedLevel > 5;
        end,
        showRewards = { ItemStack("item_cha_lvbu", 1) },
        onClaim = TimelineNode(0, GetRoles, { "cha_lvbu" })
    },
    {
        id = "mission_bowangpo",
        name = "博望坡之战",
        description = "击败敌军先锋夏侯惇！",
        icon = "Roles/ui_cha_xiahoudun",
        canComplete = function()
            return GameRuntimeData.Instance.SelectedLevel > 8;
        end,
        showRewards = { ItemStack("item_currency_coin", 1000) },
        onClaim = TimelineNode(0, GetItems, { ItemStack("item_currency_coin", 1000) })
    },
    {
        id = "mission_sunquan",
        name = "击败孙权",
        description = "孙吴领兵来犯，教训他们！",
        icon = "Roles/ui_cha_sunquan",
        canComplete = function()
            return GameRuntimeData.Instance.SelectedLevel > 10;
        end,
        showRewards = { ItemStack("item_cha_sunquan", 1), ItemStack("item_cha_zhouyu", 1) },
        onClaim = TimelineNode(0, GetRoles, { "cha_sunquan", "cha_zhouyu" })
    },
    {
        id = "mission_20",
        name = "通过20关",
        description = "",
        icon = "Roles/ui_cha_liubei",
        canComplete = function()
            return GameRuntimeData.Instance.SelectedLevel > 20;
        end,
        showRewards = { ItemStack("item_currency_coin", 1000) },
        onClaim = TimelineNode(0, GetItems, { ItemStack("item_currency_coin", 1000) })
    },
    {
        id = "mission_30",
        name = "通过30关",
        description = "",
        icon = "Roles/ui_cha_liubei",
        canComplete = function()
            return GameRuntimeData.Instance.SelectedLevel > 30;
        end,
        showRewards = { ItemStack("item_currency_coin", 2000) },
        onClaim = TimelineNode(0, GetItems, { ItemStack("item_currency_coin", 2000) })
    },
    {
        id = "mission_40",
        name = "通过50关",
        description = "",
        icon = "Roles/ui_cha_liubei",
        canComplete = function()
            return GameRuntimeData.Instance.SelectedLevel > 50;
        end,
        showRewards = { ItemStack("item_currency_coin", 3000) },
        onClaim = TimelineNode(0, GetItems, { ItemStack("item_currency_coin", 3000) })
    },
    {
        id = "mission_99",
        name = "通过99关",
        description = "大业既成！",
        icon = "Roles/ui_cha_liubei",
        canComplete = function()
            return GameRuntimeData.Instance.SelectedLevel > 99;
        end,
        showRewards = { ItemStack("item_currency_coin", 10000) },
        onClaim = TimelineNode(0, GetItems, { ItemStack("item_currency_coin", 10000) })
    },
};

return missions;
