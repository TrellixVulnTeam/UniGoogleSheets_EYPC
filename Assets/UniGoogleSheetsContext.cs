public class UniGoogleSheetsContext
{
    public UniGoogleSheetsContext(ParserContainer map)
    {
        this.ParserContainer = new ParserContainer();
        this.ParserContainer.Initialize();
        this.CSVReader = new CSVReader();
    }

    public readonly ParserContainer ParserContainer;
    public readonly CSVReader CSVReader;

}