﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TennisAgent : Agent
{
    [Header("Specific to Tennis")]
    public GameObject ball;
    public bool invertX;
    public float invertMult;
    public int score;
    public GameObject scoreText;
    public GameObject myArea;
    public GameObject opponent;

    public override List<float> CollectState()
    {
        state.Add(invertMult * (gameObject.transform.position.x - myArea.transform.position.x));
        state.Add(gameObject.transform.position.y - myArea.transform.position.y);
        state.Add(invertMult * gameObject.GetComponent<Rigidbody>().velocity.x);
        state.Add(gameObject.GetComponent<Rigidbody>().velocity.y);

        state.Add(invertMult * (ball.transform.position.x - myArea.transform.position.x));
        state.Add(ball.transform.position.y - myArea.transform.position.y);
        state.Add(invertMult * ball.GetComponent<Rigidbody>().velocity.x);
        state.Add(ball.GetComponent<Rigidbody>().velocity.y);
        return state;
    }

    // to be implemented by the developer
    public override void AgentStep(float[] act)
    {
        float moveX = 0.0f;
        float moveY = 0.0f;
        moveX = 0.25f * Mathf.Clamp(act[0], -1f, 1f) * invertMult;
        if (Mathf.Clamp(act[1], -1f, 1f) > 0f && gameObject.transform.position.y - transform.parent.transform.position.y < -1.5f)
        {
            moveY = 0.5f;
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, moveY * 12f, 0f);
        }

        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(moveX * 50f, GetComponent<Rigidbody>().velocity.y, 0f);

        if (invertX)
        {
            if (gameObject.transform.position.x - transform.parent.transform.position.x < -invertMult)
            {
                gameObject.transform.position = new Vector3(-invertMult + transform.parent.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
            }
        }
        else
        {
            if (gameObject.transform.position.x - transform.parent.transform.position.x > -invertMult)
            {
                gameObject.transform.position = new Vector3(-invertMult + transform.parent.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
            }
        }

        scoreText.GetComponent<Text>().text = score.ToString();
    }

    // to be implemented by the developer
    public override void AgentReset()
    {
        if (invertX)
        {
            invertMult = -1f;
        }
        else
        {
            invertMult = 1f;
        }

        gameObject.transform.position = new Vector3(-(invertMult) * Random.Range(6f, 8f), -1.5f, 0f) + transform.parent.transform.position;
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
    }
}
