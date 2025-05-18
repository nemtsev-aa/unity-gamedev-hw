namespace ZombieShooter.UI {
    
    public sealed class InitializeGamePopup : UIPopup {

        public override void Show(bool status) {
            gameObject.SetActive(status);
        }
    }
}

