using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VerticalScene : MonoBehaviour
{
    public enum State
    {
        Null,
        Ready,
        Rolling,
        FadeOut,
        Stop,
    }
    public List<GameObject> m_NearBackground_Start = new List<GameObject>();
    public List<GameObject> m_NearBackground_Body = new List<GameObject>();
    public List<GameObject> m_NearBackground_End = new List<GameObject>();

    public List<GameObject> m_MiddleBackground_Start = new List<GameObject>();
    public List<GameObject> m_MiddleBackground_Body = new List<GameObject>();
    public List<GameObject> m_MiddleBackground_End = new List<GameObject>();

    public float m_Speed_Near = 0;
    public float m_Speed_Middle = 0;

    public Vector2 m_BackagegroundSize;

    private List<GameObject> m_CurNearBackground = new List<GameObject>();
    private List<GameObject> m_CurMiddleBackground = new List<GameObject>();
    private int m_CurNearEndIndex = 0;
    private int m_CurMiddleEndIndex = 0;
    private Vector2 m_BackgroundStartPos;
    private Vector2 m_BackgroundEndPos;

    State m_State = State.Null;

    State SceneST
    {
        set
        {
            switch (m_State)
            {
                case State.Rolling:
                    break;
            }

            m_State = value;

            switch(m_State)
            {
                case State.Ready:
                    {
                        m_CurNearBackground = new List<GameObject>(2);
                        m_CurNearBackground.Add(null);
                        m_CurNearBackground.Add(null);
                        m_CurNearBackground[0] = m_NearBackground_Start[0];
                        m_CurNearBackground[0].transform.position = this.transform.position;
                        m_CurNearBackground[0].SetActive(true);

                        m_CurMiddleBackground = new List<GameObject>(2);
                        m_CurMiddleBackground.Add(null);
                        m_CurMiddleBackground.Add(null);
                        m_CurMiddleBackground[0] = m_MiddleBackground_Start[0];
                        m_CurMiddleBackground[0].transform.position = this.transform.position;
                        m_CurMiddleBackground[0].SetActive(true);

                    }
                    break;
                case State.Rolling:
                    {
                        for (int i = 0; i < m_CurNearBackground.Count; ++i)
                        {
                            if(m_CurNearBackground[i] == null)
                            {
                                m_CurNearBackground[i] = GetRandNearBackground();
                                m_CurNearBackground[i].transform.position = m_BackgroundStartPos;
                                m_CurNearBackground[i].SetActive(true);
                            }
                        }

                        for (int i = 0; i < m_CurMiddleBackground.Count; ++i)
                        {
                            if (m_CurMiddleBackground[i] == null)
                            {
                                m_CurMiddleBackground[i] = GetRandMiddleBackground();
                                m_CurMiddleBackground[i].transform.position = m_BackgroundStartPos;
                                m_CurMiddleBackground[i].SetActive(true);
                            }
                        }
                    }
                    break;
                case State.FadeOut:
                    {
                        for (int i = 0; i < m_CurNearBackground.Count; ++i)
                        {
                            if (m_CurNearBackground[i] == null)
                            {
                                m_CurNearBackground[i] = m_NearBackground_End[0];
                                m_CurNearBackground[i].transform.position = m_BackgroundStartPos;
                                m_CurNearBackground[i].SetActive(true);
                                m_CurNearEndIndex = 0;
                            }
                        }

                        for (int i = 0; i < m_CurMiddleBackground.Count; ++i)
                        {
                            if (m_CurMiddleBackground[i] == null)
                            {
                                m_CurMiddleBackground[i] = m_MiddleBackground_End[0];
                                m_CurMiddleBackground[i].transform.position = m_BackgroundStartPos;
                                m_CurMiddleBackground[i].SetActive(true);
                                m_CurMiddleEndIndex = 0;
                            }
                        }
                    }
                    break;
                case State.Stop:
                    {
                        for (int i = 0; i < m_CurNearBackground.Count; ++i)
                        {
                            m_CurNearBackground[i].SetActive(false);
                            m_CurNearBackground[i] = null;
                        }

                        for (int i = 0; i < m_CurMiddleBackground.Count; ++i)
                        {
                            m_CurMiddleBackground[i].SetActive(false);
                            m_CurMiddleBackground[i] = null;
                        }
                    }
                    break;
            }


        }
    }

	// Use this for initialization
	void Start()
	{
        InitScene();
	}

    void InitScene()
    {
        m_BackgroundStartPos = new Vector2(
            this.transform.position.x,
            this.transform.position.y - m_BackagegroundSize.y
        );

        m_BackgroundEndPos = new Vector2(
            this.transform.position.x,
            this.transform.position.y + m_BackagegroundSize.y
        );
    }

	// Update is called once per frame
	void Update()
	{
        //test
        if(Input.GetKeyDown(KeyCode.T))
        {
            SceneST = State.Ready;
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            SceneST = State.Rolling;
        }
        //----------------------
        switch(m_State)
        {
            case State.Ready:
                break;
            case State.Rolling:
                {
                    RollingNear();
                    RollingMiddle();
                }
                break;
            case State.FadeOut:
                {
                    ;
                }
                break;
            case State.Stop:
                {
                    ;
                }
                break;
        }
	}

    void RollingNear()
    {
        //是否需要滚动新的
        for (int i = 0; i < m_CurNearBackground.Count; ++i)
        {
            float rollingnewpos = m_CurNearBackground[i].transform.position.y + Time.deltaTime * m_Speed_Near;

            m_CurNearBackground[i].transform.position = new Vector2(m_CurNearBackground[i].transform.position.x,rollingnewpos);

            if (rollingnewpos > m_BackgroundEndPos.y)
            {
                m_CurNearBackground[i].SetActive(false);

                m_CurNearBackground[i] = GetRandNearBackground();

                m_CurNearBackground[i].SetActive(true);

                m_CurNearBackground[i].transform.position = m_BackgroundStartPos;
            }
        }
    }

    GameObject GetRandNearBackground()
    {
        int index = Random.Range(0, m_NearBackground_Body.Count);

        return m_NearBackground_Body[index];
    }

    void RollingMiddle()
    {
        //是否需要滚动新的
        for (int i = 0; i < m_CurMiddleBackground.Count; ++i)
        {
            float rollingnewpos = m_CurMiddleBackground[i].transform.position.y + Time.deltaTime * m_Speed_Near;

            m_CurMiddleBackground[i].transform.position = new Vector2(m_CurMiddleBackground[i].transform.position.x, rollingnewpos);

            if (rollingnewpos > m_BackgroundEndPos.y)
            {
                m_CurMiddleBackground[i].SetActive(false);

                m_CurMiddleBackground[i] = GetRandMiddleBackground();

                m_CurMiddleBackground[i].SetActive(true);

                m_CurMiddleBackground[i].transform.position = m_BackgroundStartPos;
            }
        }
    }

    GameObject GetRandMiddleBackground()
    {
        int index = Random.Range(0, m_MiddleBackground_Body.Count);

        return m_MiddleBackground_Body[index];
    }
}
