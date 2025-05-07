using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public interface IDatabase
{
    void Init();
    UniTask<bool> CheckNickNameExist(string nickName);
    UniTask Save(string nickName, ScPlayerEntity playerEntity);
    UniTask<ScPlayerEntity> Load(string nickName);
    UniTask<List<ScPlayerEntity>> LoadAll();
    UniTask Remove(string nickName);
}