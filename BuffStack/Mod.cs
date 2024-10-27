using GDWeave;

namespace BuffStack;

public class Mod : IMod
{
    public Mod(IModInterface modInterface)
    {
        modInterface.RegisterScriptMod(new PlayerPatch());
    }

    public void Dispose()
    {
    }
}
