using System;
using System.Collections;
using ReferenceVariables;
using UnityEngine;

public class Mapper : MonoBehaviour
{
    [SerializeField] private GameObject userHand;
    [Space]
    [SerializeField] private FloatVariable input;
    [SerializeField] private float inputTolerance = 0.5f;
    [Space]
    [SerializeField] private string avatarChangeEventKey;
    [SerializeField] private GameObject avatarHand;
    [SerializeField] private Side side;

    private enum Side
    {
        Left,
        Right,
    }
    
    private enum State
    {
        Unmapped,
        Mapped,
        Measured,
    }
    private State state;

    private GameObject userShoulder;
    private GameObject avatarShoulder;

    private float userArmLength;
    private float avatarArmLength;
    
    private void Awake()
    {
        state = State.Unmapped;
        userShoulder = new GameObject("Shoulder")
        {
            transform =
            {
                parent = transform,
            },
        };
        
        input.ValueChanged += OnInputChanged;
        EventManager.AddListener(avatarChangeEventKey, OnAvatarChange);
    }

    private void OnAvatarChange(GameEvent obj)
    {
        if (obj is not AvatarSwappedEvent args)
            return;
        avatarArmLength = side switch
        {

            Side.Left => args.ArmLengths.Left.Value,
            Side.Right => args.ArmLengths.Right.Value,
            _ => throw new ArgumentOutOfRangeException(),
        };
        avatarShoulder = side switch
        {
            Side.Left => args.Shoulders.Left,
            Side.Right => args.Shoulders.Right,
            _ => throw new ArgumentOutOfRangeException(),
        };
    }

    private void OnInputChanged(object sender, EventArgs e)
    {
        if (input.Value < inputTolerance)
            return;
        switch (state)
        {
            case State.Unmapped:
                MapShoulder();
                state = State.Mapped;
                break;
            case State.Mapped:
                MeasureArmLength();
                StartCoroutine(Remapping());
                state = State.Measured;
                break;
            case State.Measured:
                input.ValueChanged -= OnInputChanged;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void MapShoulder()
    {
        userShoulder.transform.position = userHand.transform.position;
    }

    private void MeasureArmLength()
    {
        userArmLength = Vector3.Distance(userHand.transform.position, userShoulder.transform.position);
    }

    private IEnumerator Remapping()
    {
        float scale = avatarArmLength / userArmLength;
        while (true)
        {
            var position = (userHand.transform.position - userShoulder.transform.position) * scale;
            avatarHand.transform.position = position + avatarShoulder.transform.position;
            avatarHand.transform.rotation = userHand.transform.rotation;
            yield return new WaitForEndOfFrame();
        }
    }
}
