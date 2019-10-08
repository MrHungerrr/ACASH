using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Scholar : MonoBehaviour
{
    //Тип ученика
    public enum list_scholarType
    {
        Dumb,
        Asshole,
        Underdog
    }

    public list_scholarType scholarType;


    //Базовое
    public bool isLiving;
    [HideInInspector]
    public int number;
    [HideInInspector]
    public string type;
    private string keyWord;
    [HideInInspector]
    public bool executed;
    [HideInInspector]
    public bool greeneyes;
    private const float peripheral_vision_angle = 140f;
    private const float central_vision_angle = 30f;
    private const float vision_distance = 5f;



    [HideInInspector]
    public bool talking;
    [HideInInspector]
    public bool walking;
    [HideInInspector]
    public bool cheating;
    [HideInInspector]
    public bool cheatNeed;
    [HideInInspector]
    public bool writing;

    //Вопросы
    private bool walkingAnswer;
    [HideInInspector]
    public bool asking;
    [HideInInspector]
    public string questionKey;
    [HideInInspector]
    public bool teacher_answer;


    //T - это Teacher
    private float T_angle_x;
    private float T_angle_y;
    private Vector3 T_direction;
    private float T_distance;
    private float T_look_time;
    private bool T_behind_wall;
    private float T_look_near_time;
    [HideInInspector]
    public bool T_look_at_us;
    [HideInInspector]
    public bool T_look_near_at_us;
    private int T_look_coef;
    private float angle_to_teacher;
    [HideInInspector]
    public bool T_here;
    private bool T_in_sight;
    private float T_vanish_time;
    private const float T_vanish_time_const = 4f;




    //Доп инструмент ы
    [HideInInspector]
    public TextBoxScholar TextBox;
    [HideInInspector]
    public ActionsScholar Action;
    [HideInInspector]
    public Emotions Emotions;
    private GameManager GameMan;
    private ScriptManager ScriptMan;
    private PlayerScript Player;
    private Transform Camera;
    [HideInInspector]
    public ScholarAgent Agent;
    [HideInInspector]
    public ScholarManager ScholarMan;



    private bool selectable = true;

    //Стресс и настроение
    [HideInInspector]
    public int stress;
    [HideInInspector]
    public int threshold_1 = 33;
    [HideInInspector]
    public int threshold_2 = 66;
    private byte moodType;
    [HideInInspector]
    public float[] moodType_time = new float[3];
    private string[] moodType_string = new string[]
    {
        "chill",
        "normal",
        "panic"
    };


    //Написание теста
    [HideInInspector]
    public int IQ_start;
    [HideInInspector]
    public int test;
    [HideInInspector]
    public double test_buf;
    [HideInInspector]
    public float test_bufTime;


    //Список замечаний, которые уже были сделаны.
    [HideInInspector]
    public Dictionary<string, bool> remarks = new Dictionary<string, bool>()
    {
        { "Talking_", false },
        { "Cheating_", false },
        { "Walking_", false },
    };


    //Список причин, по которым можно удалить ученика
    [HideInInspector]
    public Dictionary<string, bool> reason = new Dictionary<string, bool>()
    {
        { "Walking_", false },
        { "Talking_", false },
        { "Cheating_", false },
    };









    private void Awake()
    {
        this.tag = "Scholar";
 
        TextBox = transform.parent.transform.parent.GetComponentInChildren<TextBoxScholar>();
        Emotions = transform.parent.transform.parent.GetComponentInChildren<Emotions>();
        Action = transform.parent.transform.GetComponentInParent<ActionsScholar>();
        ScholarMan = GameObject.FindObjectOfType<ScholarManager>();
        ScriptMan = GameObject.FindObjectOfType<ScriptManager>();
        GameMan = GameObject.FindObjectOfType<GameManager>();
        Player = GameObject.FindObjectOfType<PlayerScript>();
        Camera = GameObject.FindGameObjectWithTag("PlayerCamera").transform;
        cheatNeed = false;

        ChangeType(scholarType.ToString());
        Selectable(true);
        IQ_start = 0;
    }


    private void Start()
    {
    }



    void Update()
    {
        if (!executed)
        {
            if (writing)
                Agent.Writing();

            TeacherCalculate();
            WhereTeacher();
        }
    }

    private void FixedUpdate()
    {
        if(!executed)
            MoodTypeTime();
    }

    public void Continue()
    {
        Action.Continue();
        Debug.Log("Продолжаем");
    }

    public void Stop()
    {
            StopAllCoroutines();
            Action.Stop();
            TextBox.Clear();
    }

    public void StartWrite()
    {
        Action.StartWriting();
    }

    public void Do(string key)
    {
        Action.Doing(key);
    }

    public void SimpleDo(string key)
    {
        Action.SimpleDoing(key);
    }



    //========================================================================================================
    //Поднятие стресса

    public void Stress(int value)
    {
        stress += value;
        if (stress > 100)
            stress = 100;
        if (stress < 0)
            stress = 0;

        ChangeMoodType();
    }

    private void ChangeMoodType()
    {
        if (stress < threshold_1)
            moodType = 0;
        else if (stress < threshold_2)
            moodType = 1;
        else
            moodType = 2;
    }



    public string GetMoodType()
    {
        return moodType_string[moodType];
    }


    private void MoodTypeTime()
    {
         moodType_time[moodType] += Time.fixedDeltaTime;
    }

    public void ZeroingMoodTypeTime()
    {
        for(int i = 0; i < 3; i++)
        {
            moodType_time[i] = 0;
        }
    }

    public int GetMoodTypeTime()
    {
        float buf_time = 0;

        for (int i = 0; i < 3; i++)
        {
            buf_time += moodType_time[i];
        }

        buf_time *= UnityEngine.Random.value;
        moodType_time[1] += moodType_time[0];


        if(buf_time <= moodType_time[0])
        {
            return 0;
        }
        else if(buf_time < moodType_time[1])
        {
            return 1;
        }
        else
        {
            return 2;
        }
    }



    public void WritingTest(float value)
    {
        //Debug.Log("Пишу тест");
        if (test_bufTime > 0)
        {
            test_buf += value * Time.deltaTime;
            test_bufTime -= Time.deltaTime;
        }
        else
        {
            test += Convert.ToInt32(test_buf);
            test_bufTime = 1f;
            test_buf = 0;
        }
    }



    //========================================================================================================
    //Ученик говорит

    public void Say(string key, double probability_of_continue)
    {
        Stop();
        StartCoroutine(Saying(key, probability_of_continue));
    }

    public void Say(string key)
    {
        Stop();
        StartCoroutine(Saying(key, 0));
    }

    public void SayWithoutContinue(string key)
    {
        StartCoroutine(SayingWithoutContinue(key));
    }

    public void SayThoughts(string key)
    {
        StartCoroutine(SayingThoughts(key));
    }

    private IEnumerator Saying(string key, double probability_of_continue)
    {
        talking = true;
        Selectable(false);
        TextBox.Say(key);


        while (TextBox.IsTalking())
        {
            Action.SightTo(Player.transform.position);
            yield return new WaitForEndOfFrame();
        }

        Selectable(true);
        talking = false;

        if (ScholarMan.Probability(probability_of_continue))
            Continue();
        else
            StartWrite();
    }

    private IEnumerator SayingWithoutContinue(string key)
    {
        talking = true;
        Selectable(false);
        TextBox.Say(key);

        while (TextBox.IsTalking())
        {
            Action.SightTo(Player.transform.position);
            yield return new WaitForEndOfFrame();
        }

        Selectable(true);
        talking = false;
    }

    private IEnumerator SayingThoughts(string key)
    {
        talking = true;
        TextBox.Say(key);

        while (TextBox.IsTalking())
        {
            yield return new WaitForEndOfFrame();
        }

        talking = false;
    }



    //========================================================================================================
    //Ответ учителю

    public void Answer(string key, double prob_cont_right, double prob_cont_false)
    {
        if (IsTeacherBullingRight())
        {
            Say(key + "_Yes", prob_cont_right);
        }
        else
        {
            Say(key + "_No", prob_cont_false);
        }
    }

    public void Answer(string key, string obj, double prob_cont_right, double prob_cont_false)
    {
        if (IsTeacherBullingRight(obj))
        {
            Say(key + "_Yes", prob_cont_right);
        }
        else
        {
            Say(key + "_No", prob_cont_false);
        }
    }



    //========================================================================================================
    //Наезд на ученика

    public void HearBulling(bool strong)
    {
        Agent.HearBulling(strong);
        StartCoroutine(WatchingTeacher());
    }

    public void Bulling(string bullKey, bool strong)
    {
        Agent.Bulling(keyWord + bullKey, strong);
    }

    public void BullingForSubjects(string bullKey, string obj)
    {
        Agent.BullingForSubjects(keyWord + bullKey, obj);
    }

    private IEnumerator WatchingTeacher()
    {
        while (!talking)
        {
            Action.SightTo(Player.transform.position);
            yield return new WaitForEndOfFrame();
        }
    }



    //========================================================================================================
    //Прав ли учитель?

    public bool IsTeacherBullingRight()
    {
        switch (GetView())
        {
            case "Cheating_":
                {
                    if (cheating)
                        return true;
                    else
                        return false;
                }
            case "Talking_":
                {
                    if (talking)
                        return true;
                    else
                        return false;
                }
            case "Walking_":
                {
                    if (walkingAnswer)
                        return true;
                    else
                        return false;
                }
        }
        return false;
    }

    public bool IsTeacherBullingRight(string obj)
    {
        Debug.Log(obj);
        if (GameMan.banned[obj])
            return true;
        else
            return false;
    }



    //========================================================================================================
    //Вопросы и ответы

    public void Question(string q)
    {
        questionKey = keyWord + q;
        teacher_answer = false;
        asking = true;
        StartCoroutine(Asking(q));
    }

    private IEnumerator Asking(string key)
    {
        talking = true;
        Selectable(false);
        TextBox.Question(key);

        yield return new WaitForSeconds(1f);

        while (TextBox.IsTalking())
        {
            Action.SightTo(Player.transform.position);
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("Мы задали вопрос");
        Selectable(true);
        talking = false;
    }

    public void TeacherAnswer(bool answer)
    {
        //string buf = "Answer_";
        //buf += UnityEngine.Random.Range(0, ScriptMan.linesQuantity[buf]);
        string key = questionKey;

        if (answer)
        {
            key += "_Yes";
            teacher_answer = true;
        }
        else
        {
            key += "_No";
        }

        Agent.TeacherAnswer(key, answer);
        asking = false;
    }



    //=================================================================================================================================================
    //Учитель кричит на ученика

    public void Shout()
    {

    }





    //=================================================================================================================================================
    //Как выглядит то что делает ученик

    public string GetView()
    {
        if (talking)
        {
            return "Talking_";
        }
        else if (walking)
        {
            return "Walking_";
        }
        else
        {
            return "Cheating_";
        }
    }



    //=================================================================================================================================================
    //Исключение

    public void Execute(string key)
    {
        Stop();
        TextBox.Say(keyWord + key);
        Do("Execute");
    }





    //========================================================================================================
    //Вероятность





    //========================================================================================================
    //Присвоить номер ученику

    public void SetNumber(int i)
    {
        number = i;
        Action.home = ScholarMan.desks[0, i].position;
        Action.desk = ScholarMan.desks[1, i].position;
        TextBox.Number(i);
    }



    //========================================================================================================
    //Возможность выбрать объект

    public void Selectable(bool u)
    {

        if (u)
        {
            selectable = true;
            StartCoroutine(SetSelectable());
        }
        else
        {
            this.gameObject.layer = 10;
            selectable = false;
        }
    }



    private IEnumerator SetSelectable()
    {
        yield return new WaitForSeconds(0.1f);
        if (selectable)
        {
            this.gameObject.layer = 9;
        }
    }



    //========================================================================================================
    //Изменение типа ученика

    public void ChangeType(string t)
    {
        type = t;
        Agent = new ScholarAgent(type, this);
        keyWord = type + "_";
    }



    //========================================================================================================
    //Вычесления связанные с учителем

    private void TeacherCalculate()
    {
        if (Player.look_closer)
            T_look_coef = 2;
        else
            T_look_coef = 1;

        T_behind_wall = true;

        T_angle_y = LookingAngle(Action.transform.position, Player.transform);
        T_angle_x = (Camera.transform.rotation.eulerAngles.x+30) % 360;

        T_direction = new Vector3(Player.transform.position.x - Action.transform.position.x, Action.transform.position.y, Player.transform.position.z - Action.transform.position.z);
        T_distance = T_direction.magnitude;

        RaycastHit hit;
        Debug.DrawRay(Action.transform.position + transform.up.normalized * 0.3f, T_direction.normalized, Color.red);
        if (Physics.Raycast(Action.transform.position + transform.up.normalized*0.3f, T_direction.normalized, out hit, vision_distance))
        {
            if(hit.collider.tag == "Player")
            {
                T_behind_wall = false;
            }
        }



        angle_to_teacher = LookingAngle(Player.transform.position, Action.transform);

        // Debug.Log("X: " + teacher_angle_x + ";   Y: " + teacher_angle_y);
        // Debug.Log("Magnitude: " + teacher_distance);


        if ((T_angle_y < (48/(T_look_coef*T_look_coef)) && T_angle_x < 80) || (T_distance<=0.5))
        {
            T_look_near_at_us = true;
        }
        else
        {
            T_look_near_at_us = false;
        }


        if (T_look_at_us)
        {
            T_look_time += Time.deltaTime * T_look_coef;
        }
        else
        {
            T_look_time = 0;
        }

        if (T_look_near_at_us)
        {
            T_look_near_time += Time.deltaTime * T_look_coef;
        }
        else
        {
            T_look_near_time = 0;
        }

        T_look_at_us = false;
    }

    private float LookingAngle(Vector3 lookingTo, Transform Who)
    {
        lookingTo.y = Who.transform.position.y;
        lookingTo = lookingTo - Who.transform.position;
        return Vector3.Angle(lookingTo, Who.forward);
    }

    public void Hear(float distance)
    {
        if(T_distance <= distance)
        {
            T_here = true;
            T_vanish_time = T_vanish_time_const;
        }
    }

    public void SpecialHear(Vector3 pos)
    {
        //Вероятность нужна тут
        Debug.Log("Я услышал");
        Action.SpecialWatch(pos);
    }

    private void WhereTeacher()
    {

        if (!T_behind_wall)
        {
            if (T_distance > 0.5)
            {
                if ((angle_to_teacher <= peripheral_vision_angle*0.5f && T_here) || (angle_to_teacher <= central_vision_angle * 0.5f && !T_here))
                {
                    T_in_sight = true;
                }
                else
                {
                    T_in_sight = false;
                }
            }
            else
            {
                T_in_sight = true;
            }
        }
        else
        {
            T_in_sight = false;
        }


        if(T_in_sight)
        {
            T_here = true;

            T_vanish_time = T_vanish_time_const;
        }
        else
        {
            if (T_vanish_time > 0)
            {
                T_vanish_time -= Time.deltaTime;
            }
            else
            {
                T_here = false;
            }
        }
        

        /*
        if(T_here)
        {
            //Debug.Log("Ты туууут!");
        }
        else
        {
            //Debug.Log("Неееет");
        }
        */
    }

}
