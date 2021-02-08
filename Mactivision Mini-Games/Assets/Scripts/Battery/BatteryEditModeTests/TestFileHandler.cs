using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestFileHandler
{
    // FileHandler is mostly just an abstraction over builtin C# functions.

    private FileHandler File;

    [SetUp]
    public void SetupFileHandler()
    {
        File = new FileHandler();
    }

}
