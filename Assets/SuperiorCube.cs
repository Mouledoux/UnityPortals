using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperiorCube : MonoBehaviour
{
    public bool isOnGround;
    SuperiorStateMachine<PlayerStates> ssm = new SuperiorStateMachine<PlayerStates>(PlayerStates.INIT);

    void Start()
    {
        ssm.AddTransition(PlayerStates.INIT, PlayerStates.IDLE, new System.Func<bool>[] {}, null);


        ssm.AddTransition(PlayerStates.IDLE, PlayerStates.FALLING,
        new System.Func<bool>[]
        {
            () => transform.position.y > 10,
        },
        () =>
        {
            transform.up = Vector3.down;
            transform.localScale = Vector3.one * 4;
        });


        ssm.AddTransition(PlayerStates.FALLING, PlayerStates.IDLE,
        new System.Func<bool>[]
        {
            () => isOnGround,
        },
        () =>
        {
            ResetPosition();
            transform.up = Vector3.up;
            transform.localScale = Vector3.one;
        });


        System.Func<bool>[] deadPre = {NearCamera};
        System.Action die = ResetPosition;
        ssm.AddTransition(PlayerStates.IDLE, PlayerStates.DEAD, deadPre, die);
    }

    void Update()
    {
        print(1/Time.deltaTime);
        ssm.ProcessTransitions();
    }

    void ResetPosition()
    {
        transform.position = Vector3.zero;
    }

    bool NearCamera()
    {
        return Vector3.Distance(transform.position, Camera.main.transform.position) < 5f;
    }

    private enum PlayerStates
    {
        INIT,
        IDLE,
        WALKING,
        RUNNING,
        JUMPING,
        FALLING,
        DEAD,
    }
}
