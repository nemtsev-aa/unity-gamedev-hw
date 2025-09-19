namespace BehaviorTree.Brain {

    public enum BotStates {
        Idle = 0,
        DetectFarmingZone = 1,
        MoveToFarmingZone = 2,
        DetectResourceSource = 3,
        MoveToResourceSource = 4,
        Farming = 5,
        DetectResourceLoot = 6,
        MoveToResourceLoot = 7,
        LiftResourceLoot = 8,
        MoveToDeliveryTarget = 9,
        DropResourceLoot = 10,
        Patrol = 11
    }
}