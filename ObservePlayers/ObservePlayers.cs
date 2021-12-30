using CitizenFX.Core;
using CitizenFX.Core.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObservePlayers
{
    public class ObservePlayers : BaseScript
    {
        private Vector3 oldPosition;
        private int currentlyObserving = 0;
        private bool inVehicleNotiShown = false;
        private bool observingInVehicle = false;

        public ObservePlayers()
        {
            EventHandlers["ObservePlayers:Observe"] += new Action<int>((int playerToObserve) => {
                initiateObservations(playerToObserve);
            });
            EventHandlers["ObservePlayers:StopObserve"] += new Action<dynamic>((dynamic) => {
                initiateStopObservations();
            });
            
            main();
        }

        private async void initiateObservations(int playerToObserve)
        {
            if (currentlyObserving == 0)
            {
                oldPosition = Game.PlayerPed.Position;
            }
            Game.PlayerPed.Task.ClearAllImmediately();
            inVehicleNotiShown = false;
            observingInVehicle = false;
            SetObservationProperties(true);
            await Delay(1000);
            currentlyObserving = playerToObserve;
        }

        private async void initiateStopObservations()
        {
            if (currentlyObserving > 0)
            {
                currentlyObserving = 0;
                Game.PlayerPed.Task.ClearAllImmediately();
                Game.PlayerPed.Position = oldPosition;
                Game.PlayerPed.IsCollisionEnabled = true;
                await Delay(1000);
                SetObservationProperties(false);
            }
        }

        private async void main()
        {
            while (true)
            {
                await Delay(1);
                if (currentlyObserving > 0)
                {
                    SetObservationProperties(true);

                    if (Players[currentlyObserving] != null && Players[currentlyObserving].Character != null && Players[currentlyObserving].Character.Exists())
                    {
                        if (Game.IsControlJustReleased(0, Control.Jump))
                        {
                            observingInVehicle = !observingInVehicle;
                        }

                        Ped target = Players[currentlyObserving].Character;
                        Vector3 observepos = target.GetOffsetPosition(new Vector3(0, -3f, 0));
                        if (target.IsInVehicle())
                        {
                            if (observingInVehicle)
                            {
                                if (!Game.PlayerPed.IsInVehicle(target.CurrentVehicle))
                                {
                                    SetInVehicle(target.CurrentVehicle);
                                }
                                Screen.ShowSubtitle("Spacebar/X to exit vehicle after waypoint checked (less desync).", 100);
                            }
                            else
                            {
                                observepos = target.CurrentVehicle.GetOffsetPosition(new Vector3(0, -4f, 0));
                                if (!inVehicleNotiShown)
                                {
                                    Screen.ShowNotification("Press Spacebar/X to enter vehicle as passenger (to check waypoint).");
                                    inVehicleNotiShown = true;
                                }
                            }
                        }
                        else
                        {
                            inVehicleNotiShown = false;
                        }
                        if (!observingInVehicle)
                        {
                            Game.PlayerPed.Position = observepos;
                        }
                        
                    }
                }
            }
        }

        private void SetInVehicle(Vehicle veh)
        {
            if (veh.IsSeatFree(VehicleSeat.Passenger))
            {
                Game.PlayerPed.SetIntoVehicle(veh, VehicleSeat.Passenger);
            }
            else if (veh.IsSeatFree(VehicleSeat.LeftRear))
            {
                Game.PlayerPed.SetIntoVehicle(veh, VehicleSeat.LeftRear);
            }
            else if (veh.IsSeatFree(VehicleSeat.RightRear))
            {
                Game.PlayerPed.SetIntoVehicle(veh, VehicleSeat.RightRear);
            }
            else
            {
                Screen.ShowNotification("No free passenger seat found in vehicle.");
            }
        }

        private void SetObservationProperties (bool observing)
        {
            Game.PlayerPed.IsVisible = !observing;
            Game.PlayerPed.IsCollisionEnabled = !observing;
            Game.PlayerPed.IsInvincible = observing;
        }

    }
}
