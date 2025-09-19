namespace FarmingSystem {

    public class ResourceLoot : Loot {
        public Resource Resource { get; private set; }

        public void Init(Resource resource) {
            Resource = resource;
        }

        public override void Take() {
            base.Take();
        }
    }
}



