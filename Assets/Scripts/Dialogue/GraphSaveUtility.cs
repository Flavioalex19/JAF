using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphSaveUtility
{
    DialogueGraphView _targetGraphView;

    public static GraphSaveUtility GetInstance(DialogueGraphView targetGraphView)
    {
        return new GraphSaveUtility
        {
            _targetGraphView = targetGraphView
        };
    }

    void SaveGraph(string fileName)
    {

    }
    void LoadGraph(string fileName)
    {

    }
}
