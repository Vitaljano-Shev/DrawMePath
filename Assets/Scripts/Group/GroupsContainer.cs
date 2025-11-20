using System.Collections.Generic;
using UnityEngine;

public class GroupsContainer : MonoBehaviour
{
    [SerializeField] private List<Group> _groups = new List<Group>();

    private int _correctGroupCount = 0, _finishedGroupCount;
    private void Start()
    {
        LevelController.Instance.GroupsAmount = _groups.Count;

        if (_groups.Count > 0)
        {
            foreach (Group group in _groups)
            {
                group.GroupCorrect = false;
            }
        }
    }
    
    public void AddGroup(bool isCorrect)
    {
        _finishedGroupCount++;
        if (isCorrect) _correctGroupCount++;

        if(_finishedGroupCount == _groups.Count)
        {
            if (_correctGroupCount == _finishedGroupCount)
            {
                Debug.Log("Level Completed");
                LevelController.Instance.NextLevel();
            }
            else
            {
                Debug.Log("Invalid Groups. Level incompled");
                LevelController.Instance.RestartLevel();
            }
        }
    }
}
