using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropDownVision : MonoBehaviour {

    

    List<string> names_vision  = new List<string>() {"Vision", "Control", "Impaired" };
    List<string> names_explore = new List<string>() { "Exploration mode", "Active", "Passive" };

    public Dropdown dropbown_vision;
    public Dropdown dropbown_exploration;
    public Text selectedName;

    public void DropDown_IndexChanged(int index)
    {
        selectedName.text = names_vision[index];
        selectedName.text = names_explore[index];
    }

    void Start()
    {
        PopulateList();
    }

    void PopulateList()
    {
        dropbown_vision.AddOptions(names_vision);
        dropbown_exploration.AddOptions(names_explore);
    }
}
