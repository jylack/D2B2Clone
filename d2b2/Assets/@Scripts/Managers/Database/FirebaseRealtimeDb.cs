using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Firebase;
using Firebase.Database;
using Newtonsoft.Json;
using UnityEngine;

public class FirebaseRealtimeDb : IDatabase
{
    private const string RootPath = "users";
    
    private DatabaseReference dbRef;
    
    
    
    public void Init()
    {
        try
        {
            DependencyStatus result = FirebaseApp.CheckAndFixDependenciesAsync().GetAwaiter().GetResult();
            if (result == DependencyStatus.Available)
            {
                dbRef = FirebaseDatabase.DefaultInstance.RootReference;
                Debug.Log("FirebaseDatabase initialized!");
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + result);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
            throw;
        }
    }

    public async UniTask<bool> CheckNickNameExist(string nickName)
    {
        try
        {
            DataSnapshot rootSnapshot = await dbRef.Child(RootPath).GetValueAsync();

            foreach (DataSnapshot snapshot in rootSnapshot.Children)
            {
                if (snapshot.Key == nickName)
                    return true;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
            throw;
        }

        return false;
    }
    
    public async UniTask Save(string nickName, ScPlayerEntity playerEntity)
    {
        try
        {
            if (string.IsNullOrEmpty(nickName))
                throw new Exception("nickname is empty.");
            
            string json = JsonConvert.SerializeObject(playerEntity);
            await dbRef.Child(RootPath).Child(nickName).SetRawJsonValueAsync(json);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
            throw;
        }
    }

    public async UniTask<ScPlayerEntity> Load(string nickName)
    {
        try
        {
            DataSnapshot snapshot = await dbRef.Child(RootPath).Child(nickName).GetValueAsync();
            if (snapshot.Exists)
            {
                string json = snapshot.GetRawJsonValue();
                return JsonConvert.DeserializeObject<ScPlayerEntity>(json);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
            throw;
        }

        return null;
    }

    public async UniTask<List<ScPlayerEntity>> LoadAll()
    {
        try
        {
            List<ScPlayerEntity> entities = new();
            DataSnapshot rootSnapshot = await dbRef.Child(RootPath).GetValueAsync();
            
            foreach (DataSnapshot snapshot in rootSnapshot.Children)
            {
                string json = snapshot.GetRawJsonValue();
                ScPlayerEntity entity = JsonConvert.DeserializeObject<ScPlayerEntity>(json);
                entities.Add(entity);
            }

            return entities;
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
            throw;
        }
    }
    
    public async UniTask Remove(string nickName)
    {
        try
        {
            await dbRef.Child(RootPath).Child(nickName).RemoveValueAsync();
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
            throw;
        }
    }
}
