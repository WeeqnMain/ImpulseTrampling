using UnityEngine;

[System.Serializable]
public class Sound
{
    [SerializeField] private string _name;
    public string name => _name;

    [SerializeField] private AudioClip _clip;
    public AudioClip clip => _clip;
}
