public class UniGoogleSheetsContext
{
    public UniGoogleSheetsContext(TypeMap map)
    {
        this.TypeMap = new TypeMap();
        this.TypeMap.Initialize();
    }

    public readonly TypeMap TypeMap;
}