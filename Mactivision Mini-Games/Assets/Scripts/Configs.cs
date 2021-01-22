using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Converters;

public class BatteryConfig
{
    public string PlayerName {get; set;}
    public string StartTime {get; set;}
    public string EndTime {get; set;}

    public IList<GameConfig> Games {get; set;}
}

public interface GameConfig 
{
    string Name {get; set;}
    string Scene {get; set;}
}

// public class GameConfigConverter : CustomCreationConverter<GameConfig>
// {
//     public override GameConfig Create(Type objectType)
//     {
//         return new DiggerConfig();
//     }
// }

public class DiggerConfig : GameConfig
{
    public string Name {get; set;}
    public string Scene {get; set;}
    public int DigAmount {get; set;}
    public string DigKey {get; set;}
}

public class FeederConfig : GameConfig
{
    public string Name {get; set;}
    public string Scene {get; set;}
    public string Seed {get; set;}
    public float MaxGameTime {get; set;}
    public int MaxFoodDispensed {get; set;}
    public int TotalFoods {get; set;}
    public float AverageUpdateFrequency {get; set;}
    public float StandardDeviationUpdateFreq {get; set;}
}
