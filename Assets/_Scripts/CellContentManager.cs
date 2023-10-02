public class CellContentManager
{
    private ContentPooler _contentPooler;
    private ColorsManager _colorsManager;

    public CellContentManager(ContentPooler contentPooler, ColorsManager colorsManager)
    {
        _contentPooler = contentPooler;
        _colorsManager = colorsManager;
    }

    public CellContent GetContent(int value)
    {
        CellContent cellContent = _contentPooler.GiveCellContent();

        UpdateContent(cellContent, value);

        return cellContent;
    }

    public void UpdateContent(CellContent cellContent, int value)
    {
        cellContent.SetValue(value, _colorsManager.GetColorByValue(value));
    }
}
