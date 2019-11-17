namespace Pretty.Core.Helpers
{
    public interface IFileHelper
    {
        bool CompressionImage(
            string filePath,
            string savePath,
            string fileName,
            long quality = 50);

        bool IsImg(string fileFullName);
    }
}
