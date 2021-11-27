using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Tutorial : MonoBehaviour
{
    [SerializeField]
    private VideoPlayer player;
    [SerializeField]
    private Text tutorialText;

    private void Update() {
        switch (player.clip.name) {
            case "coinTuto":
                tutorialText.text = $"Collect all coins in a scene to unlock the exit and go to the next level!";
                break;
            case "guardAlarmTuto":
                tutorialText.text = $"Beware of guards, they can see and chase you if you step too close to them!";
                break;
            case "noiseTuto":
                tutorialText.text = $"If you make noise (e.g.: shoot) guards will go and check if they can find anything!";
                break;
            case "punchTuto":
                tutorialText.text = $"Punch guards to kill them, but be careful they can punch you back! (Spacebar)";
                break;
            case "securityCamTuto":
                tutorialText.text = $"Security Cameras will lock onto you if you step to close and alarm all guards in the level!";
                break;
            case "shootTuto":
                tutorialText.text = $"Collect weapons (by killings guards who have weapons) in order to shoot! (Left Mousebutton)";
                break;
            case "sprintTuto":
                tutorialText.text = $"You can sprint in order to escape guards. But you should manage your endurance! (Left Shift)";
                break;
            case "healthTuto":
                tutorialText.text = $"Manage your health or else you will die and the run will be ended!";
                break;
            case "exitTuto":
                tutorialText.text = $"After collecting all coins, go and find the exit to enter the next level!";
                break;
        }
    }


    public void ChangeVideoClip(VideoClip _clip) {
        player.clip = _clip;
    }
}
