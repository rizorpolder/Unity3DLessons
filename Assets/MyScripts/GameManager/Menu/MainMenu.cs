using UnityEngine;


public class MainMenu:MonoBehaviour
    {
        //Track Animation Component -done;
        // Track the Animation clips for fade in/out-done;
        //Function that can recieve animation events
        //Functions t play fade in/out animations

        [SerializeField] private Animation _mainMenuAnimator;
        [SerializeField] private AnimationClip _fadeInAnimation;
        [SerializeField] private AnimationClip _fadeOutAnimation;
        

        public void OnfadeOutComplete()
        {
            Debug.LogWarning("FadeOut Complete");
        }

        public void OnFadeInComplete()
        {
            Debug.LogWarning("FadeIn Complete");
            //UIManager.Instance.SetDummyCameraActive(true);

    }

    public void FadeIn()
        {
            _mainMenuAnimator.Stop();
            _mainMenuAnimator.clip = _fadeInAnimation;
            _mainMenuAnimator.Play();
        }

        public void FadeOut()
        {
            //UIManager.Instance.SetDummyCameraActive(false);

            _mainMenuAnimator.Stop();
            _mainMenuAnimator.clip = _fadeOutAnimation;
            _mainMenuAnimator.Play();
    }
    //GameManager 08

}

