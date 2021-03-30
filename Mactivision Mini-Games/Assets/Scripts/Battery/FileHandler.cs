using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileHandler
{
    string ConfigPath = "./Assets/Configs/";
    string GeneratedFile = "GeneratedTemplate.json";
    string MetaFile = "GeneratedTemplate.meta";

    public void DeleteGeneratedConfig()
    {
        // Mostly used for testing.
        File.Delete(ConfigPath + GeneratedFile);

        // Delete the meta file otherwise Unity will complain.
        File.Delete(ConfigPath + MetaFile);
    }

    public bool DirectoryExists(string path)
    {
        return Directory.Exists(path);
    }

    public bool GeneratedConfigExists()
    {
        return File.Exists(ConfigPath + GeneratedFile);
    }
   
    public void CreateDirectory(string path)
    {
        Directory.CreateDirectory(path);
    }

    public void DeleteDirectory(string path, bool and_contents)
    {
        if (DirectoryExists(path))
        {
            Directory.Delete(path, and_contents);
        }
    }

    public void WriteGenerated(string json)
    {
        WriteFile(ConfigPath, GeneratedFile, json);
        Debug.Log("Wrote Generated Config.");
    }

    private void WriteFile(string path, string filename, string text)
    {
        if (DirectoryExists(path))
        {
            File.WriteAllText(path + filename, text);
        }
    }
}
