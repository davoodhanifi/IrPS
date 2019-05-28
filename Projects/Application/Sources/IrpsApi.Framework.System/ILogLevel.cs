namespace IrpsApi.Framework.System
{
    public interface ILogLevel : IRecord
    {
        string Title
        {
            get;
        }

        string TitleEn
        {
            get;
        }
    }
}
