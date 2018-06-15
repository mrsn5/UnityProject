using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (this.isActiveAndEnabled)
        {
            HeroRabbit rabbit = collider.GetComponent<HeroRabbit>();
            if (rabbit != null)
            {
                LevelController.current.SetLevelPassed();
                LevelController.current.Save();
                WinPopUp.Instance.Open();
            }
        }
    }

}
