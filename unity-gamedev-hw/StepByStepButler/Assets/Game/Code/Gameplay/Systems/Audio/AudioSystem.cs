using EventBusService;
using StepByStepButler.Gameplay.Heroes;
using Random = UnityEngine.Random;

namespace StepByStepButler.Gameplay.Systems.Audio {

    public sealed class AudioSystem : IGameSystem {
        private readonly IEventBus _eventBus;
        private readonly HeroesProvider _heroes;
        private readonly HeroAudioConfiguration _audioConfig;
        private readonly AudioPlayer _audioPlayer;

        public AudioSystem(IEventBus eventBus,
                           HeroesProvider heroes,
                           HeroAudioConfiguration audioConfig,
                           AudioPlayer audioPlayer) {

            _eventBus = eventBus;
            _heroes = heroes;
            _audioConfig = audioConfig;
            _audioPlayer = audioPlayer;
        }
        public void OnInitializeGame() {
            _eventBus.Subscribe<TurnStartedEvent>(OnTurnStarted);
            _eventBus.Subscribe<HealthChangedEvent>(OnHealthChanged);
            _eventBus.Subscribe<AbilityUsedEvent>(OnAbilityUsed);
            _eventBus.Subscribe<HeroDeathEvent>(OnHeroDeath);
        }

        public void OnFinishGame() {
            _eventBus.Unsubscribe<TurnStartedEvent>(OnTurnStarted);
            _eventBus.Unsubscribe<HealthChangedEvent>(OnHealthChanged);
            _eventBus.Unsubscribe<AbilityUsedEvent>(OnAbilityUsed);
            _eventBus.Unsubscribe<HeroDeathEvent>(OnHeroDeath);
        }

        public void OnRestartGame() { }

        private void OnTurnStarted(TurnStartedEvent turnEvent) {

            if (TryGetHeroAudioClipsTypeById(turnEvent.HeroId, out HeroAudioClips audioClips) == false)
                return;

            if (audioClips?.startTurnClips != null && audioClips.startTurnClips.Length > 0) {
                var randomClip = audioClips.startTurnClips[Random.Range(0, audioClips.startTurnClips.Length)];
                _audioPlayer.PlaySound(randomClip);
            }
        }

        private void OnHealthChanged(HealthChangedEvent evt) {

            if (_heroes.TryGetEntityById(evt.HeroId, out var hero) == false)
                return;

            // Check if health is below 20%
            float healthPercentage = (float)evt.NewHealth / evt.MaxHealth;

            if (healthPercentage < 0.2f && healthPercentage > 0f) {
                var heroType = hero.GetComponent<HeroTypeComponent>().Type;
                var audioClips = _audioConfig.GetClips(heroType);

                if (audioClips?.lowHealthClip != null)
                    _audioPlayer.PlaySound(audioClips.lowHealthClip);
            }
        }

        private void OnAbilityUsed(AbilityUsedEvent abilityEvent) {

            if (TryGetHeroAudioClipsTypeById(abilityEvent.HeroId, out HeroAudioClips audioClips) == false)
                return;

            if (audioClips?.abilityClip != null)
                _audioPlayer.PlaySound(audioClips.abilityClip);
        }

        private void OnHeroDeath(HeroDeathEvent deathEvent) {

            if (TryGetHeroAudioClipsTypeById(deathEvent.HeroId, out HeroAudioClips audioClips) == false)
                return;

            if (audioClips?.deathClip != null)
                _audioPlayer.PlaySound(audioClips.deathClip);
        }

        private bool TryGetHeroAudioClipsTypeById(int heroId, out HeroAudioClips clips) {

            if (_heroes.TryGetEntityById(heroId, out var hero) == false) {
                clips = default;
                return false;
            }

            var type = hero.GetComponent<HeroTypeComponent>().Type;
            clips = _audioConfig.GetClips(type);
            return true;
        }
    }
}