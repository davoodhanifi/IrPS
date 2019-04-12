namespace IrpsApi.Framework.Operation
{
    public interface IRequestType : IRecord
    {
        string Title
        {
            get;
            set;
        }

        string TitleEn
        {
            get;
            set;
        }
    }
}
