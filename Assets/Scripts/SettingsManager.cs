using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Turning;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
public class SettingsManager : MonoBehaviour
{
    SoundManager soundManager;
    PlayerState playerState;
    ControllerInputActionManager inputManagerLeft;
    ControllerInputActionManager inputManagerRight;
    DynamicMoveProvider moveProvider;
    ContinuousTurnProvider turnProvider;
    TeleportationProvider teleportationProvider;
    bool isLefter = false;
    private void Awake()
    {
        soundManager = GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>();
        playerState = GameObject.FindWithTag("PlayerState").GetComponent<PlayerState>();
        inputManagerLeft = GameObject.FindWithTag("LeftController").GetComponent<ControllerInputActionManager>();
        inputManagerRight = GameObject.FindWithTag("RightController").GetComponent<ControllerInputActionManager>();
        moveProvider = GameObject.FindWithTag("Player").GetComponentInChildren<DynamicMoveProvider>();
        turnProvider = GameObject.FindWithTag("Player").GetComponentInChildren<ContinuousTurnProvider>();
        teleportationProvider = GameObject.FindWithTag("Player").GetComponentInChildren<TeleportationProvider>();
    }
    public void SetVolume(float volume)
    {
        soundManager.SetVolume(volume);
    }
    public void LeftGame()
    {
        Application.Quit();
        Debug.Log("Quit game");
    }
    public void SetDifficulty(int level)
    {
        playerState.ChangeDiffuculty(level);
        Debug.Log(playerState.Difficulty);
    }
    public void ToggleLeft(bool value)
    {
        isLefter = value;
        inputManagerLeft.smoothMotionEnabled = !value;
        inputManagerLeft.smoothTurnEnabled = value;
        inputManagerRight.smoothTurnEnabled = !value;
        inputManagerRight.smoothMotionEnabled = value;
    }
    public void SetSmoothMovement(bool value)
    {
        if (!isLefter)  inputManagerLeft.smoothMotionEnabled = value;
        else inputManagerRight.smoothMotionEnabled = value;
    }
    public void SetSmoothTurn(bool value)
    {
        if (isLefter) inputManagerLeft.smoothTurnEnabled = value;
        else inputManagerRight.smoothTurnEnabled = value;
    }
    public void SetTeleportation(bool value)
    {
        if (isLefter) inputManagerLeft.smoothMotionEnabled = !value;
        else inputManagerRight.smoothMotionEnabled = !value;
    }
    public void SetTeleportDelay(float value)
    {
        teleportationProvider.delayTime = value;
    }
    public void SetMovementSpeed(float value)
    {
        moveProvider.moveSpeed = value;
    }
    public void SetTurnSpeed(float value)
    {
        turnProvider.turnSpeed = value;
    }
}
