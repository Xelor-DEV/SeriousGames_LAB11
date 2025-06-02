using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

public class OculusInputHandler : MonoBehaviour
{
    public XRNode hand;

    private InputDevice handController;
    private Vector2 lastJoystick;
    private float lastTriggerValue;
    private float lastGripValue;

    // Estados privados para seguimiento
    private bool primaryButtonPressed;
    private bool secondaryButtonPressed;
    private bool menuButtonPressed;
    private bool gripButtonPressed;
    private bool triggerButtonPressed;
    private bool joystickButtonPressed; // Nuevo estado para el botón del joystick

    [Header("Primary Button (Y/B)")]
    public UnityEvent OnPrimaryButtonDown;
    public UnityEvent OnPrimaryButtonUp;
    public UnityEvent OnPrimaryButton; // Estado continuo
    public UnityEvent<bool> OnPrimaryButtonState; // Estado actual

    [Header("Secondary Button (X/A)")]
    public UnityEvent OnSecondaryButtonDown;
    public UnityEvent OnSecondaryButtonUp;
    public UnityEvent OnSecondaryButton; // Estado continuo
    public UnityEvent<bool> OnSecondaryButtonState; // Estado actual

    [Header("Menu Button")]
    public UnityEvent OnMenuButtonDown;
    public UnityEvent OnMenuButtonUp;
    public UnityEvent OnMenuButton; // Estado continuo
    public UnityEvent<bool> OnMenuButtonState; // Estado actual

    [Header("Grip Button")]
    public UnityEvent OnGripButtonDown;
    public UnityEvent OnGripButtonUp;
    public UnityEvent OnGripButton; // Estado continuo
    public UnityEvent<bool> OnGripButtonState; // Estado actual

    [Header("Trigger Button")]
    public UnityEvent OnTriggerButtonDown;
    public UnityEvent OnTriggerButtonUp;
    public UnityEvent OnTriggerButton; // Estado continuo
    public UnityEvent<bool> OnTriggerButtonState; // Estado actual

    [Header("Joystick Button")] // Nuevo header para el botón del joystick
    public UnityEvent OnJoystickButtonDown;
    public UnityEvent OnJoystickButtonUp;
    public UnityEvent OnJoystickButton; // Estado continuo
    public UnityEvent<bool> OnJoystickButtonState; // Estado actual

    [Header("Analog Controls")]
    public UnityEvent<Vector2> OnJoystickChanged;
    public UnityEvent<float> OnTriggerValueChanged;
    public UnityEvent<float> OnGripValueChanged;

    [Header("Thresholds")]
    [SerializeField] private float joystickThreshold = 0.01f;
    [SerializeField] private float analogThreshold = 0.01f;

    private void Start()
    {
        LoadDevices();
        InitializeLastValues();
    }

    private void LoadDevices()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDeviceCharacteristics characteristics = InputDeviceCharacteristics.HeldInHand;

        if (hand == XRNode.LeftHand)
        {
            characteristics |= InputDeviceCharacteristics.Left;
        }
        else if (hand == XRNode.RightHand)
        {
            characteristics |= InputDeviceCharacteristics.Right;
        }

        InputDevices.GetDevicesWithCharacteristics(characteristics, devices);

        if (devices.Count > 0)
        {
            handController = devices[0];
        }
    }

    private void InitializeLastValues()
    {
        lastJoystick = Vector2.zero;
        lastTriggerValue = 0f;
        lastGripValue = 0f;
    }

    void Update()
    {
        if (!handController.isValid)
        {
            LoadDevices();
            if (!handController.isValid) return;
        }

        UpdateButtonStates();
        UpdateJoystickState();
        UpdateTriggerValues();
    }

    private void UpdateButtonStates()
    {
        UpdateButton(CommonUsages.primaryButton,
            ref primaryButtonPressed,
            OnPrimaryButtonDown,
            OnPrimaryButtonUp,
            OnPrimaryButton,
            OnPrimaryButtonState);

        UpdateButton(CommonUsages.secondaryButton,
            ref secondaryButtonPressed,
            OnSecondaryButtonDown,
            OnSecondaryButtonUp,
            OnSecondaryButton,
            OnSecondaryButtonState);

        UpdateButton(CommonUsages.menuButton,
            ref menuButtonPressed,
            OnMenuButtonDown,
            OnMenuButtonUp,
            OnMenuButton,
            OnMenuButtonState);

        UpdateButton(CommonUsages.gripButton,
            ref gripButtonPressed,
            OnGripButtonDown,
            OnGripButtonUp,
            OnGripButton,
            OnGripButtonState);

        UpdateButton(CommonUsages.triggerButton,
            ref triggerButtonPressed,
            OnTriggerButtonDown,
            OnTriggerButtonUp,
            OnTriggerButton,
            OnTriggerButtonState);

        // Nuevo: Actualización del botón del joystick (L3/R3)
        UpdateButton(CommonUsages.primary2DAxisClick,
            ref joystickButtonPressed,
            OnJoystickButtonDown,
            OnJoystickButtonUp,
            OnJoystickButton,
            OnJoystickButtonState);
    }

    private void UpdateButton(InputFeatureUsage<bool> buttonFeature,
                             ref bool currentState,
                             UnityEvent downEvent,
                             UnityEvent upEvent,
                             UnityEvent continuousEvent,
                             UnityEvent<bool> stateEvent)
    {
        bool newState;
        if (handController.TryGetFeatureValue(buttonFeature, out newState))
        {
            // Estado actual (siempre se envía)
            stateEvent?.Invoke(newState);

            // Evento continuo mientras está presionado
            if (newState)
            {
                continuousEvent?.Invoke();
            }

            // Eventos Down/Up solo en cambio de estado
            if (newState != currentState)
            {
                if (newState)
                {
                    downEvent?.Invoke();
                }
                else
                {
                    upEvent?.Invoke();
                }
                currentState = newState;
            }
        }
    }

    private void UpdateJoystickState()
    {
        Vector2 newJoystick;
        if (handController.TryGetFeatureValue(CommonUsages.primary2DAxis, out newJoystick))
        {
            if (Vector2.Distance(newJoystick, lastJoystick) > joystickThreshold)
            {
                OnJoystickChanged?.Invoke(newJoystick);
                lastJoystick = newJoystick;
            }
        }
    }

    private void UpdateTriggerValues()
    {
        // Actualizar valor del trigger
        float newTriggerValue;
        if (handController.TryGetFeatureValue(CommonUsages.trigger, out newTriggerValue))
        {
            if (Mathf.Abs(newTriggerValue - lastTriggerValue) > analogThreshold)
            {
                OnTriggerValueChanged?.Invoke(newTriggerValue);
                lastTriggerValue = newTriggerValue;
            }
        }

        // Actualizar valor del grip
        float newGripValue;
        if (handController.TryGetFeatureValue(CommonUsages.grip, out newGripValue))
        {
            if (Mathf.Abs(newGripValue - lastGripValue) > analogThreshold)
            {
                OnGripValueChanged?.Invoke(newGripValue);
                lastGripValue = newGripValue;
            }
        }
    }
}