using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    string m_scenseName = "test";

    Board m_board;
    Spawner m_spawner;

    Brick m_activeBrick;

    [Range(0.02f, 1f)]
    public float m_timeDropInterval = 0.7f;
    private float m_timeToDrop;

    float m_timeDropIntervalModded;

    float m_TimeToNextKeyLeftRight;
    [Range(0.02f, 1f)]
    public float m_keyRepeatRateLeftRight = 0.3f;

    float m_TimeToNextKeyDown;
    [Range(0.01f, 1f)]
    public float m_keyRepeatRateDown = 0.03f;

    float m_TimeToNextKeyRotate;
    [Range(0.01f, 1f)]
    public float m_keyRepeatRateRotate = 0.6f;

    bool m_isGamePlaying = true;

    GameObject m_gameOverPanel;

    SoundController m_soundController;

    ScoreController m_scoreController;

    public GameObject m_pausePanel;

    bool m_isPause = false;

    GhostBrick m_ghostBrick;

    enum Direction { none, left, right, up, down }

    Direction m_swipeDirection = Direction.none;
    Direction m_swipeEndDirection = Direction.none;

    bool hasPlayed = false;

    GameObject m_canvasOverlay;

    GameObject m_menu;

    public Background m_Background;

    public ThemeController m_themeController;
    
    // Use this for initialization
    void Start ()
	{
        m_gameOverPanel = GameObject.FindWithTag("gameover");

        if (m_gameOverPanel)
        {
            //Debug.Log("founded game over object");
            m_gameOverPanel.SetActive(false);
        }
        else
        {
            Debug.Log("WARNING! There is no gameover panel defined!");
        }

        if (m_pausePanel)
        {
            //Debug.Log("founded game over object");
            m_pausePanel.SetActive(false);
        }
        else
        {
            Debug.Log("WARNING! There is no pause panel defined!");
        }

        if (!m_themeController)
        {
            Debug.Log("WARNING! There is no themecontroller defined!");
        }

        m_TimeToNextKeyLeftRight = Time.time;
        m_TimeToNextKeyDown = Time.time;
        m_TimeToNextKeyRotate = Time.time;

        m_board = GameObject.FindWithTag("board").GetComponent<Board>();
	    m_spawner = GameObject.FindWithTag("spawner").GetComponent<Spawner>();
        m_soundController = GameObject.FindObjectOfType<SoundController>();
        m_scoreController = GameObject.FindObjectOfType<ScoreController>();
        m_ghostBrick = GameObject.FindObjectOfType<GhostBrick>();

        m_menu = GameObject.FindGameObjectWithTag("menu");

        m_canvasOverlay = GameObject.FindGameObjectWithTag("overlay");
        
        if (m_canvasOverlay)
        {
            m_canvasOverlay.SetActive(false);
        }
        else
        {
            Debug.Log("WARNING! There is no canvasOverlay defined!");
        }

        if (!m_menu)
        {
            Debug.Log("WARNING! There is no menu defined!");
        }

        if (!m_board)
	    {
	        Debug.Log("WARNING! There is no board defined!");
	    }

	    if (!m_spawner)
	    {
	        Debug.Log("WARNING! There is no spawner defined!");
        }
        else
        {
            //if (!m_activeBrick)
            //{
            //    m_activeBrick = m_spawner.SpawBrick();
            //}
            //m_spawner.transform.position = Vectorf.Round(m_spawner.transform.position);
        }

        if (!m_soundController)
        {
            Debug.Log("WARNING! There is no Sound controller defined!");
        }
        else
        {
            //
        }

        if (!m_scoreController)
        {
            Debug.Log("WARNING! There is no Score controller defined!");
        }

        if (!m_Background)
        {
            Debug.Log("WARNING! There is no background worldspace defined!");
        }
        
        m_timeDropIntervalModded = Mathf.Clamp(m_timeDropInterval - ((float)m_scoreController.m_level * 0.1f), 0.05f, 1f);

        hasPlayed = false;

        string theme = "Default";

        if (m_Background)
        {
            m_Background.SetBackground(theme);
        }

        m_spawner.SetTheme(theme);
    }

    void MoveRight()
    {
        m_activeBrick.MoveRight();
        m_TimeToNextKeyLeftRight = Time.time + m_keyRepeatRateLeftRight;
        if (!m_board.IsInvalidPosition(m_activeBrick))
        {
            m_activeBrick.MoveLeft();
            PlaySoundFx(m_soundController.m_errorSound, 3f);
        }
        else
        {
            PlaySoundFx(m_soundController.m_moveBrickSound, 1.5f);
        }
    }

    void MoveLeft()
    {
        m_activeBrick.MoveLeft();
        m_TimeToNextKeyLeftRight = Time.time + m_keyRepeatRateLeftRight;
        if (!m_board.IsInvalidPosition(m_activeBrick))
        {
            m_activeBrick.MoveRight();
            PlaySoundFx(m_soundController.m_errorSound, 3f);
        }
        else
        {
            PlaySoundFx(m_soundController.m_moveBrickSound, 1.5f);
        }
    }

    void Rotate()
    {
        m_activeBrick.RotateRight();
        m_TimeToNextKeyRotate = Time.time + m_keyRepeatRateRotate;
        if (!m_board.IsInvalidPosition(m_activeBrick))
        {
            m_activeBrick.RotateLeft();
            PlaySoundFx(m_soundController.m_errorSound, 3f);
        }
        else
        {
            PlaySoundFx(m_soundController.m_moveBrickSound, 1.5f);
        }
    }

    void MoveDown()
    {
        m_timeToDrop = Time.time + m_timeDropIntervalModded;
        m_TimeToNextKeyDown = Time.time + m_keyRepeatRateDown;

        m_activeBrick.MoveDown();

        if (!m_board.IsInvalidPosition(m_activeBrick))
        {
            if (m_board.IsOverLimit(m_activeBrick))
            {
                ShowGameOver();
            }
            else
            {
                LandBrick();
            }
        }
    }

    public void PlayerInput()
    {
        if (Input.GetButton("MoveRight") && Time.time > m_TimeToNextKeyLeftRight || Input.GetButtonDown("MoveRight"))
        {
            MoveRight();
        }
        else if (Input.GetButton("MoveLeft") && Time.time > m_TimeToNextKeyLeftRight || Input.GetButtonDown("MoveLeft"))
        {
            MoveLeft();
        }
        else if (Input.GetButtonDown("Rotate") && Time.time > m_TimeToNextKeyRotate)
        {
            Rotate();
        }
        else if ((Input.GetButton("MoveDown") && Time.time > m_TimeToNextKeyDown) || (Time.time > m_timeToDrop))
        {
            MoveDown();
        }else if (Input.GetButton("Pause"))
        {
            TogglePause();
        }else if((m_swipeDirection == Direction.right && Time.time > m_TimeToNextKeyLeftRight) || m_swipeEndDirection == Direction.right)
        {
            MoveRight();
            m_swipeDirection = Direction.none;
            m_swipeEndDirection = Direction.none;
        }
        else if ((m_swipeDirection == Direction.left && Time.time > m_TimeToNextKeyLeftRight) || m_swipeEndDirection == Direction.left)
        {
            MoveLeft();
            m_swipeDirection = Direction.none;
            m_swipeEndDirection = Direction.none;
        }
        else if (m_swipeDirection == Direction.up && m_swipeEndDirection == Direction.up)
        {
            Rotate();
            m_swipeDirection = Direction.none;
            m_swipeEndDirection = Direction.none;
        }
        else if (m_swipeDirection == Direction.down && Time.time > m_TimeToNextKeyDown)
        {
            MoveDown();
            m_swipeDirection = Direction.none;
        }
    }

    void ShowGameOver()
    {
        m_activeBrick.MoveUp();
        m_isGamePlaying = false;
        Debug.Log("game over");
        if (!m_isGamePlaying)
        {
            m_gameOverPanel.SetActive(true);
        }
        PlaySoundFx(m_soundController.m_gameOverSound, 5f);
        PlaySoundFx(m_soundController.m_vocalGameOver, 2f);
    }

    public void LandBrick()
    {
        m_activeBrick.MoveUp();
        m_board.StoreBrickInGrid(m_activeBrick);
        m_activeBrick.LandBrickFx();

        m_activeBrick = m_spawner.SpawBrick();

        if (m_ghostBrick)
        {
            m_ghostBrick.Reset();
        }

        m_TimeToNextKeyLeftRight = Time.time;
        m_TimeToNextKeyDown = Time.time;
        m_TimeToNextKeyRotate = Time.time;

        //m_board.RemoveAllRows();
        m_board.StartCoroutine("RemoveAllRows");
        
        PlaySoundFx(m_soundController.m_dropBrickSound, 1f);

        if (m_board.m_complatedRow > 0)
        {
            m_scoreController.ScoreLines(m_board.m_complatedRow);

            if (m_scoreController.m_isLevelUp)
            {
                m_timeDropIntervalModded = Mathf.Clamp(m_timeDropInterval - ((float)m_scoreController.m_level * 0.1f), 0.05f, 1f);
                PlaySoundFx(m_soundController.m_vocalLevelUp, 1.5f);
            }
            else
            {
                if (m_board.m_complatedRow > 1)
                {
                    AudioClip randomVocal = m_soundController.GetOneRandomAudioClip(m_soundController.m_vocalClips);
                    PlaySoundFx(randomVocal, 1.5f);
                }
            }
            
            PlaySoundFx(m_soundController.m_cleareRowSound, 1.5f);
        }
    }

    public void ReStart()
    {
        if (m_gameOverPanel)
        {
            m_gameOverPanel.SetActive(false);
        }
        Debug.Log("ReStarted");
        //Application.LoadLevel(Application.loadedLevel);
        SceneManager.LoadScene(m_scenseName);
        Time.timeScale = 1f;
    }
	
    public void SetTheme(string theme)
    {
        if(theme != null && theme != "")
        {
            if (m_Background)
            {
                m_Background.SetBackground(theme);
            }

            m_spawner.SetTheme(theme);

            if (m_themeController)
            {
                m_themeController.ToggleTheme();
            }
        }
    }

    public void PlayGame()
    {
        Debug.Log("playgame");
        m_spawner.InitBricksNext();
        hasPlayed = true;
        m_canvasOverlay.SetActive(true);
        m_menu.SetActive(false);
        m_soundController.PlayBackgorundMusic(m_soundController.GetOneRandomAudioClip(m_soundController.m_audioClips));

        if (!m_activeBrick)
        {
            m_activeBrick = m_spawner.SpawBrick();
        }
        m_spawner.transform.position = Vectorf.Round(m_spawner.transform.position);
    }

    public void StopGame()
    {
        hasPlayed = false;
        m_canvasOverlay.SetActive(false);
        m_menu.SetActive(true);
        m_soundController.m_musicSource.Stop();
    }

	// Update is called once per frame
	void Update () {
        if (hasPlayed)
        {
            if (!m_board || !m_spawner || !m_activeBrick || !m_isGamePlaying || !m_gameOverPanel || !m_soundController || !m_scoreController)
            {
                return;
            }
            PlayerInput();
        }
        else
        {
            if (Input.GetButton("Pause"))
            {
                Application.Quit();
            }
        }
	}

    void LateUpdate()
    {
        if (hasPlayed)
        {
            if (m_ghostBrick)
            {
                m_ghostBrick.DrawGhost(m_activeBrick, m_board);
            }
        }
    }

    void PlaySoundFx(AudioClip clip, float vol)
    {
        if(clip && m_soundController.m_fxEnable)
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, Mathf.Clamp(m_soundController.m_fxVolume * vol, 0.05f, 1f));
        }
    }

    public void TogglePause()
    {
        if (!m_isGamePlaying)
        {
            return;
        }

        m_isPause = !m_isPause;

        if (m_pausePanel)
        {
            m_pausePanel.SetActive(m_isPause);

            if (m_soundController)
            {
                if (m_isPause)
                {
                    m_soundController.m_musicSource.volume = 0f;
                    m_soundController.m_musicSource.Pause();
                }
                else
                {
                    m_soundController.m_musicSource.volume = m_soundController.m_musicVolume;
                    m_soundController.m_musicSource.Play();
                }
            }

            Time.timeScale = (m_isPause) ? 0 : 1;
        }
    }

    void OnEnable()
    {
        TouchController.SwipeEvent += SwipeHandler;
        TouchController.SwipeEndEvent += SwipeEndHandler;
    }

    void OnDisable()
    {
        TouchController.SwipeEvent -= SwipeHandler;
        TouchController.SwipeEndEvent -= SwipeEndHandler;
    }

    void SwipeHandler(Vector2 movement)
    {
        m_swipeDirection = GetDirection(movement);
    }

    void SwipeEndHandler(Vector2 movement)
    {
        m_swipeEndDirection = GetDirection(movement);
    }

    Direction GetDirection(Vector2 movement)
    {
        Direction swipDir = Direction.none;

        if(Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
        {
            swipDir = movement.x >= 0 ? Direction.right : Direction.left;
        }
        else
        {
            swipDir = movement.y >= 0 ? Direction.up : Direction.down;
        }

        return swipDir;
    }
}
