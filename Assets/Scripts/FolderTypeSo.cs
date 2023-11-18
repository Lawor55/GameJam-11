using UnityEngine;

[CreateAssetMenu(fileName = "EmptyFolder", menuName = "FolderType")]
public class FolderTypeSo : ScriptableObject
{
    public float rageAmount;
    public string folderName;
    public Sprite folderSprite;
    public Sprite corruptedFolderSprite;

    private string[] folderNames;

    private void Awake()
    {
        if (!folderName.Equals("")) return;
        folderNames = new[] { "New Folder" };
        folderName = folderNames[Random.Range(0, folderNames.Length)];
    }
}