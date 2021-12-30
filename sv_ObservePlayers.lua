function observefunc(source, args, rawCommand)
	if isAuthorized(source) and tablelength(args) == 1 then
		local target = tonumber(args[1])
		-- Set any blips/player names for the observer to be invisible to others.
		TriggerClientEvent("ObservePlayers:Observe", source, target)
	end
end

RegisterCommand('observe', observefunc, false)

function stopobservefunc(source, args, rawCommand)
	if isAuthorized(source) then
		local target = tonumber(args[1])	
		TriggerClientEvent("ObservePlayers:StopObserve", source)
		SetTimeout(1000, function()
			-- Restore the visibility of any blips/player names for the observer to others.
		end)
	end
end

RegisterCommand('stopobserve', stopobservefunc, false)

function tablelength(T)
  local count = 0
  for _ in pairs(T) do count = count + 1 end
  return count
end