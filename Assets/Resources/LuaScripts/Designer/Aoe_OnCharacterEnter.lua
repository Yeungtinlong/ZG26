--public delegate void AoeOnCharacterEnter(AoeState aoe, List<CharacterState> enterCharacters);

local GameLuaInterface = CS.TheGame.GM.GameLuaInterface;
local DamageInfoTag = CS.MBF.DamageInfoTag;
local CharacterState = CS.MBF.CharacterState;

local aoe_onCharacterEnter = {};

aoe_onCharacterEnter.TriggerMine = function(aoeState, enterCharacters)
    if (aoeState.parameters:ContainsKey("isTriggered")) then
        return ;
    end
    local isTriggered = false;
    for i, cs in pairs(enterCharacters) do
        if (_G.Utils.IsNil(cs) or cs.side == aoeState.side) then
            goto continue;
        end
        isTriggered = true;
        :: continue ::
    end
    if (isTriggered) then
        aoeState.duration = aoeState.model.onCharacterEnterParams[0];
        aoeState.parameters:Add("isTriggered", true);
    end
end

aoe_onCharacterEnter.TriggerSpike = function(aoeState, enterCharacters)
    if (aoeState.parameters:ContainsKey("isTriggered")) then
        return ;
    end
    local isTriggered = false;
    for i, cs in pairs(enterCharacters) do
        if (_G.Utils.IsNil(cs) or cs.side == aoeState.side) then
            goto continue;
        end
        isTriggered = true;
        :: continue ::
    end
    if (isTriggered) then
        aoeState.duration = aoeState.model.onCharacterEnterParams[0];
        aoeState.parameters:Add("isTriggered", true);
    end
end

return aoe_onCharacterEnter;