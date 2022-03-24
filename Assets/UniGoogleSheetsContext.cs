public class UniGoogleSheetsContext
{
    public UniGoogleSheetsContext(ParserContainer map)
    {
        this.ParserContainer = new ParserContainer();
        this.ParserContainer.Initialize();
    }

    public readonly ParserContainer ParserContainer;
}