using System;
using UnityEngine;
using UnityEngine.UI;

public class MobileInput : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private Button jumpButton;

    public Vector3 direction { get; private set; }

    public Action JumpButtonPressed;

    private void Awake()
    {
        jumpButton.GetComponent<Button>().onClick.AddListener(OnJumpButtonPressed);
    }

    private void Update()
    {
        direction = new Vector3(joystick.Horizontal, 0f, joystick.Vertical).normalized;
    }

    private void OnJumpButtonPressed()
    {
        JumpButtonPressed?.Invoke();
    }
}
