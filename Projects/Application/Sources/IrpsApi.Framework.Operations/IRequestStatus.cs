namespace IrpsApi.Framework.Operation
{
    public interface IRequestStatus : IRecord
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
