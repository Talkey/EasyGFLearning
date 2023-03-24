using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.AssetImporters;
using UnityEngine;


[ScriptedImporter(1, ".lua")]
public class LuaImporter : ScriptedImporter
{
    public override void OnImportAsset(AssetImportContext ctx)
    {
        var luaTxt = File.ReadAllText(ctx.assetPath);
        var assetText = new TextAsset(luaTxt);
        ctx.AddObjectToAsset("main obj", assetText);
        ctx.SetMainObject(assetText);
    }
}
