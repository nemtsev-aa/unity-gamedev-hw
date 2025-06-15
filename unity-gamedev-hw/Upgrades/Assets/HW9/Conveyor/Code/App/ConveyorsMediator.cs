using JetBrains.Annotations;
using Game.GameEngine.Mechanics;
using Conveyors.Entity.Components;

namespace Conveyors.App {

    [UsedImplicitly]
    public sealed class ConveyorsMediator {

        public void SetupFromData(ConveyorsService service, ConveyorData[] dataSet) {

            for (int i = 0, count = dataSet.Length; i < count; i++) {
                var data = dataSet[i];
                var conveyor = service.FindConveyor(data.ID);

                conveyor
                    .Get<IComponent_LoadZone>()
                    .SetupAmount(data.InputAmount);
                conveyor
                    .Get<IComponent_UnloadZone>()
                    .SetupAmount(data.OutputAmount);
            }
        }

        public void SetupByDefault(ConveyorsService service) {
            //Do nothing...
        }

        public ConveyorData[] ConvertToData(ConveyorsService service) {
            var conveyors = service.GetAllConveyors();
            var count = conveyors.Length;
            var dataArray = new ConveyorData[count];

            for (var i = 0; i < count; i++) {
                var conveyor = conveyors[i];
                var data = new ConveyorData {
                    ID = conveyor.Get<IComponent_GetId>().Id,
                    InputAmount = conveyor.Get<IComponent_LoadZone>().CurrentAmount,
                    OutputAmount = conveyor.Get<IComponent_UnloadZone>().CurrentAmount
                };
                dataArray[i] = data;
            }

            return dataArray;
        }
    }
}