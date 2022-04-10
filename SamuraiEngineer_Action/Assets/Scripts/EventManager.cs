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

    private void CsvLoad(int EventID)
    {
        csvDatas.Clear();
        string EventID_S = String.Format("{0:D2}", EventID);//D2ÇÕ2ÉPÉ^
        csvFile = Resources.Load("EventData/Event_" + EventID_S) as TextAsset;
        StringReader reader = new StringReader(csvFile.text);
        while (reader.Peek() != -1)
        {
            String line = reader.ReadLine();
            csvDatas.Add(line.Split(','));
        }
    }

    public List<String[]> subroutine(int EventID)
    {
        if (EventID == 0)
        {
            Debug.Log("IDÇ™ê›íËÇ≥ÇÍÇƒÇ¢Ç‹ÇπÇÒ");
            return null;
        }
        CsvLoad(EventID);
        Debug.Log(csvDatas[0][0]);

        return csvDatas;
    }

    /*private static EventManager eventManager = new EventManager();
    private EventManager() { }
    public static EventManager getInstance()
    {
        return eventManager;
    }*/
}