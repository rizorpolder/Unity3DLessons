//using System.IO;
//using UnityEngine;

//public class SaveData
//{
//    private IData<SerializableObject> _data = new JsonData<SerializableObject>();
//    private string _path;

//    public SaveData()
//    {
//        _path = Path.Combine(Application.dataPath,"Player.bat");
//    }

//    public void Save()
//    {
//        var player = new SerializableObject
//        {
//            Pos = Main.Instance.Player.position,
//            Name = "Roman",
//            IsEnable = true
//        };
//        _data.Save(player,_path);
//    }

//    public void Load()
//    {
//        var newPlayer = _data.Load(_path);
//        Main.Instance.Player.position = newPlayer.Pos;
//        Main.Instance.Player.name = newPlayer.Name;
//        Main.Instance.Player.gameObject.SetActive(newPlayer.IsEnable);


//    }
//}