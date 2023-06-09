using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public class SerializedGame : CSSerializedObject
{
    public int progress = 0;
    public SerializedVector playerPosition;
    public bool isPlayerUnderground;
    public SerializedDictionary<string, bool> plantUnlocked;
    public SerializedDictionary<string, bool> plantHinted;
    public SerializedDictionary<string, bool> isTriggered;
    public bool isTutorialHidden;
    public float musicParam;
    public bool isGameFinished;
}
public class SaveLoadManager : Singleton<SaveLoadManager>
{
    int currentVersion = 0;
    PlayerMovement player;
    bool isGameStarted = false;
    public void saveGame()
    {

        // 1
        // create save game
        SerializedGame save = new SerializedGame();
        save.version = currentVersion;
        player.Save(save);
        PlantManager.Instance.Save(save);
        TriggersManager.Instance.Save(save);


        var previousSave = getCurrentSave();
        if (save.progress < previousSave.progress)
        {
            Debug.LogError("this should not happen as progress would not decrease");
            return;
        }

        FileStream file = null;
        try
        {
            // 2
            BinaryFormatter bf = new BinaryFormatter();
            file = File.Create(Application.persistentDataPath + "/gamesave.save");
            bf.Serialize(file, save);



            string json = JsonUtility.ToJson(save, true);
            File.WriteAllText(Application.persistentDataPath + "/saveload.json", json);

            Debug.Log("Game Saved");
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);

        }
        finally
        {

            file.Close();
        }
    }


    SerializedGame getCurrentSave()
    {

        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            FileStream file = null;
            try
            {

                BinaryFormatter bf = new BinaryFormatter();
                file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
                SerializedGame save = (SerializedGame)bf.Deserialize(file);
                return save;
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);

            }
            finally
            {

                file.Close();
            }
        }
        return new SerializedGame();
    }

    public void LoadGame()
    {
        // 1
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            // ClearBullets();
            // ClearRobots();
            // RefreshRobots();

            // 2
            FileStream file = null;
            try
            {

                BinaryFormatter bf = new BinaryFormatter();
                file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
                SerializedGame save = (SerializedGame)bf.Deserialize(file);

                player.Load(save);
                PlantManager.Instance.Load(save);
                TriggersManager.Instance.Load(save);
                //Debug.Log(save.position);
            }
            catch (System.Exception e) {
                Debug.LogError(e);

            }
            finally
            {

                file.Close();
            }

            // 3
            //for (int i = 0; i < save.livingTargetPositions.Count; i++)
            //{
            //    int position = save.livingTargetPositions[i];
            //    Target target = targets[position].GetComponent<Target>();
            //    target.ActivateRobot((RobotTypes)save.livingTargetsTypes[i]);
            //    target.GetComponent<Target>().ResetDeathTimer();
            //}

            //// 4
            //shotsText.text = "Shots: " + save.shots;
            //hitsText.text = "Hits: " + save.hits;
            //shots = save.shots;
            //hits = save.hits;

            Debug.Log("Game Loaded");

           // Unpause();
        }
        else
        {
            Debug.Log("No game saved!");
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)){
            saveGame();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadGame();
        }
    }

    private void Start()
    {
        player = GameObject.FindObjectOfType<PlayerMovement>();
        LoadGame();
        isGameStarted = true;
    }

    //public void StartGame()
    //{
    //    if (!isGameStarted)
    //    {
    //        LoadGame();
    //    }

    //}

    public static void clearSavedData()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            File.Delete(Application.persistentDataPath + "/gamesave.save");
        }
    }

    public static bool hasSavedData()
    {
        return File.Exists(Application.persistentDataPath + "/gamesave.save");
    }


    bool savingbeforequit = false;

    //private void OnDestroy()
    //{
    //    if (isGameStarted)
    //    // if (!savingbeforequit)if (!isGameStarted)
    //    {
    //        savingbeforequit = true;

    //        saveGame();

    //        Debug.Log("OnDestroy executed");
    //    }
    //}

    private void OnApplicationQuit()
    {
        if (isGameStarted)
        //if (!savingbeforequit)
        {
            savingbeforequit = true;

            saveGame();

            Debug.Log("OnApplicationQuit executed");
        }
    }

    private void OnApplicationPause(bool pause)
    {
        if (isGameStarted)
        //if (!savingbeforequit)
        {
            savingbeforequit = true;

            saveGame();

            Debug.Log("OnApplicationPause executed");
        }
    }

    void exitApp()
    {
        if (isGameStarted)
        // if (!savingbeforequit)
        {
            savingbeforequit = true;

            saveGame();

            Debug.Log("exit button");
        }
        Application.Quit();
    }
}
