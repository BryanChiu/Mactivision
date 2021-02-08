using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class FileHandler
{
    string ConfigPath = "./Assets/Resources/";
    string GeneratedFile = "GeneratedTemplate.json";
    string ConfigFile = "BatteryConfig.json";
    string MetaFile = "GeneratedTemplate.meta";

    public FileHandler()
    {

    }

    public string[] ListConfigFiles()
    {
        return Directory.GetFiles(ConfigPath, "*.json").Select(filename => Path.GetFileNameWithoutExtension(filename)).ToArray();
    }

    public void DeleteGeneratedConfig()
    {
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
    }

    public void WriteConfig(string path, string json)
    {
        WriteFile(path, ConfigFile, json); 
    }

    private void WriteFile(string path, string filename, string text)
    {
        if (DirectoryExists(path))
        {
            File.WriteAllText(path + filename, text);
        }
    }
}
