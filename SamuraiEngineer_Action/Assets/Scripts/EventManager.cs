using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class EventManager: MonoBehaviour
{
    public static EventManager instance;
    public static TextAsset csvFile;
    List<string[]> csvDatas = new List<string[]>();

    private void Start()
    {
        csvFile = Resources.Load("EventData/Event_01") as TextAsset;
        StringReader reader = new StringReader(csvFile.text);
        while (reader.Peek() != -1)
        {
            String line = reader.ReadLine();
            csvDatas.Add(line.Split(','));
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void subroutine()
    {
        Debug.Log(csvDatas[0][0]);
    }

    /*private static EventManager eventManager = new EventManager();
    private EventManager() { }
    public static EventManager getInstance()
    {
        return eventManager;
    }*/
}