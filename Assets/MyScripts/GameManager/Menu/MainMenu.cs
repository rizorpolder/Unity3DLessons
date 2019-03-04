using UnityEngine;


public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Animation _mainMenuAnimator;
        [SerializeField] private AnimationClip _fadeInAnimation;
        [SerializeField] private AnimationClip _fadeOutAnimation;

        public Events.EventFadeComplete OnMainMenuFadeComplete;

        private void Start()
        {
            GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
        }
        

        public void OnfadeOutComplete()
        {
            
            OnMainMenuFadeComplete.Invoke(true);

        }

        public void OnFadeInComplete()
        {
            OnMainMenuFadeComplete.Invoke(false);
            UIManager.Instance.SetDummyCameraActive(true); 

        }

         public void HandleGameStateChanged(GameManager.GameState currentSate, GameManager.GameState previousState)
            {
                if (previousState == GameManager.GameState.PREGAME && currentSate == GameManager.GameState.RUNNING)
                {
                    FadeOut();
                }

                if (previousState != GameManager.GameState.PREGAME && currentSate == GameManager.GameState.PREGAME)
                {
                    FadeIn();
                }
            }

        public void FadeIn()
            {
                _mainMenuAnimator.Stop();
                _mainMenuAnimator.clip = _fadeInAnimation;
                _mainMenuAnimator.Play();
            }

        public void FadeOut()
            {
                UIManager.Instance.SetDummyCameraActive(false);
               

                _mainMenuAnimator.Stop();
                _mainMenuAnimator.clip = _fadeOutAnimation;
                _mainMenuAnimator.Play();
        }
    

}

