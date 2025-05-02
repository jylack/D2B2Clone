using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public class DatabaseManager
{
    private IDatabase database;



    public void Init()
    {
        database = new FirebaseRealtimeDb();
        database.Init();
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
