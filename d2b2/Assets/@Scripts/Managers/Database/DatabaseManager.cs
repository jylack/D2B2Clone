using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    private IDatabase database;



    public void Init()
    {
        database = new FirebaseRealtimeDatabase();
        database.Init();
    }

    public async UniTask<bool> CheckNickNameExist(string nickName)
    {
        return await database.CheckNickNameExist(nickName);
    }
    
    public async UniTask Save(string nickName, ScPlayerEntity playerEntity)
    {
        await database.Save(nickName, playerEntity);
    }

    public async UniTask<ScPlayerEntity> Load(string nickName)
    {
        return await database.Load(nickName);
    }

    public async UniTask<List<ScPlayerEntity>> LoadAll()
    {
        return await database.LoadAll();
    }

    public async UniTask Remove(string nickName)
    {
        await database.Remove(nickName);
    }
}
